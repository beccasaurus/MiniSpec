using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing;
using MiniSpec.Testing.Utilities;

namespace MiniSpec.Private.Testing.Discovery {
  internal class TestDiscoverer : ITestDiscoverer {
    public void DiscoverTests(ITestSuite suite) {
      suite.Config.TestReporter.BeforeDiscovery(suite);
      DiscoverTestsInAllAssemblies(suite);
      suite.Config.TestReporter.AfterDiscovery(suite);
    }

    void DiscoverTestsInAllAssemblies(ITestSuite suite) {
      foreach (var assemblyPath in suite.Config.AssemblyPaths) {
        Assembly? assembly;
        if (assemblyPath == Assembly.GetAssembly(typeof(TestDiscoverer)).Location) {
            assembly = null;
        } else if (assemblyPath == Assembly.GetEntryAssembly().Location) {
          assembly = Assembly.GetEntryAssembly();
        } else if (assemblyPath == Assembly.GetExecutingAssembly().Location) {
          assembly = Assembly.GetExecutingAssembly();
        } else {
          try {
            #if NET50
            assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            #elif NET20
            assembly = Assembly.LoadFrom(assemblyPath); // TODO test with dependencies and add AssemblyResolve callback
            #else
            throw new NotImplementedException();
            #endif
          } catch (Exception e) {
            var dllName = Path.GetFileName(assemblyPath);
            suite.Config.STDERR.WriteLine($"Failed to load test project {dllName}");
            suite.Config.STDERR.WriteLine($"Full path: {assemblyPath}");
            suite.Config.STDERR.WriteLine($"Error message: {e.Message}");
            return;
          }
        }
        if (assembly is not null) DiscoverTestsInAssembly(assembly, suite);
      }
    }

    // TODO STDERR => StandardError and STDOUT => StandardOutput

    void DiscoverTestsInAssembly(Assembly assembly, ITestSuite suite) {
      foreach (var type in assembly.GetTypes()) {
        var typeHasAnyTests = false;
        
        var methods = new List<MethodInfo>(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance));
        methods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));
        foreach (var method in methods) {
          var discoveredMethodTests = DiscoverMethodTests(type, method, suite, methods, assembly);
          if (discoveredMethodTests) typeHasAnyTests = true;
        }

        if (typeHasAnyTests is false) {
          var typeIsSpecType = TestNameUtility.MatchesSpecGroupPattern(type.Name, suite);
          if (typeIsSpecType) {
            foreach (var constructor in type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)) {
              if (constructor.GetParameters().Length == 0) {
                try {
                  constructor.Invoke(null); // Call it, it may define specs!
                } catch (Exception e) {
                  suite.Config.STDOUT.WriteLine($"Whoops, maybe shouldn't have called the constructor for {type.FullName}, it got angry. {e.Message}");
                }
              }
            }
          }
        }
      }
    }

    bool DiscoverMethodTests(Type type, MethodInfo method, ITestSuite suite, List<MethodInfo> typeMethods, Assembly assembly) {
      var anyDiscovered = false;
      // TODO UPDATE TO NOT CHECK FULL NAME, ONLY THE DECLARING TYPE NAME
      var typeIsTestGroup = TestNameUtility.MatchesTestGroupPattern(type.Name, suite) || TestNameUtility.MatchesTestGroupPattern(type.FullName, suite);
      var parentMethodIsTestGroup = TestNameUtility.IsLocalFunction(method.Name) && TestNameUtility.MatchesTestGroupPattern(TestNameUtility.LocalFunctionParentMethodName(method.Name)!, suite);

      var methodMatchesTestName = (typeIsTestGroup || parentMethodIsTestGroup)
        ? TestNameUtility.MatchesTestNameWithinGroupPattern(TestNameUtility.MethodOrFunctionName(method.Name), suite)
        : TestNameUtility.MatchesTestNamePattern(TestNameUtility.MethodOrFunctionName(method.Name), suite);

      var methodIsTestGroup = TestNameUtility.MatchesTestGroupPattern(TestNameUtility.MethodOrFunctionName(method.Name), suite);
      var methodHasChildTests = false;
      if (methodIsTestGroup)
        methodHasChildTests = MethodHasChildFunctionMatchingPattern(method, typeMethods, suite.Config.TestNameWithinGroupPatterns);
      else
        methodHasChildTests = MethodHasChildFunctionMatchingPattern(method, typeMethods, suite.Config.TestNamePatterns);

      var methodDefinesSpecs = MethodDefinesSpecs(method);
      var parentMethodDefinesSpecs = ParentMethodDefinesSpecs(method);
      var methodHasChildWhichDefinesSpecs = MethodHasChildWhichDefinesSpecs(method, typeMethods);

      if (methodMatchesTestName && ! methodHasChildTests) {
        anyDiscovered = true;
        ((TestSuite) suite).AddTest(new Test(
          invoke: GetInvokeAction(method),
          name: TestNameUtility.MethodOrFunctionName(method.Name),
          fullName: TestNameUtility.FullMethodName(method),
          typeName: type.FullName,
          methodName: method.Name,
          assemblyLocation: assembly.Location,
          method: method,
          assembly: assembly
        ));
      }

      return anyDiscovered;
    }

    static bool MethodDefinesSpecs(MethodInfo method) {
      return true;
    }

    static bool ParentMethodDefinesSpecs(MethodInfo method) {
      return true;
    }

    static bool MethodHasChildWhichDefinesSpecs(MethodInfo thisMethod, List<MethodInfo> typeMethods) {
      return true;
    }

    static bool MethodHasChildFunctionMatchingPattern(MethodInfo thisMethod, List<MethodInfo> typeMethods, IEnumerable<Regex> patterns) {
      foreach (var method in typeMethods)
        if (method.Name.StartsWith($"<{thisMethod.Name}>"))
          if (TestNameUtility.MatchesAnyPattern(TestNameUtility.LocalFunctionName(method.Name)!, patterns))
            return true;
      return false;
    }

    static TestFunc<object?> GetInvokeAction(MethodInfo method) {
      if (method.IsStatic) {
        return () => method.Invoke(null, null);
      } else {
        foreach (var constructor in method.DeclaringType.GetConstructors()) {
          if (constructor.GetParameters().Length == 0) {
            var instance = Activator.CreateInstance(method.DeclaringType);
            return () => method.Invoke(instance, null);
          }
        }
      }
      throw new NotImplementedException($"Don't yet know how to invoke method/function: {TestNameUtility.FullMethodName(method)}");
    }
  }
}

// AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveCallback;
// static Assembly AssemblyResolveCallback(object sender, ResolveEventArgs args)