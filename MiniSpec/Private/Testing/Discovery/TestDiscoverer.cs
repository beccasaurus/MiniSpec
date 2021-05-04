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
      if (suite.Config is null)
        throw new Exception("Please set ITestSuite.Config before calling DiscoverTests");

      suite.Config.TestReporter!.BeforeDiscovery(suite);
      DiscoverTestsInAllAssemblies(suite);
      suite.Config.TestReporter.AfterDiscovery(suite);
    }

    void DiscoverTestsInAllAssemblies(ITestSuite suite) {
      if (suite.Config is null)
        throw new Exception("Please set ITestSuite.Config before calling DiscoverTests");

      foreach (var assemblyPath in suite.Config.AssemblyPaths) {
        Assembly? assembly;
        #if NO_GET_TYPE_INFO_AVAILABLE
        if (Assembly.GetAssembly(typeof(TestDiscoverer)) is not null && assemblyPath == Assembly.GetAssembly(typeof(TestDiscoverer)).Location) {
        #else
        if (typeof(TestDiscoverer).GetTypeInfo().Assembly is not null && assemblyPath == typeof(TestDiscoverer).GetTypeInfo().Assembly!.Location) {
        #endif
            assembly = null; // Don't discover in MiniSpec, itself!
        } else if (Assembly.GetEntryAssembly() is not null && assemblyPath == Assembly.GetEntryAssembly()!.Location) {
          assembly = Assembly.GetEntryAssembly();
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
            suite.Config.StandardError.WriteLine($"Failed to load test project {dllName}");
            suite.Config.StandardError.WriteLine($"Full path: {assemblyPath}");
            suite.Config.StandardError.WriteLine($"Error message: {e.Message}");
            return;
          }
        }
        if (assembly is not null) DiscoverTestsInAssembly(assembly, suite);
      }
    }

    void DiscoverTestsInAssembly(Assembly assembly, ITestSuite suite) {
      if (suite.Config is null)
        throw new Exception("Please set ITestSuite.Config before calling DiscoverTests");

      foreach (var type in assembly.GetTypes()) { // Is GetTypes supported in all versions of .NET Standard - TODO: verify
        var typeHasAnyTests = false;

        #if NO_GET_TYPE_INFO_AVAILABLE
        var methods = new List<MethodInfo>(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance));
        methods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));
        #else
        var methods = new List<MethodInfo>(type.GetTypeInfo().DeclaredMethods);
        #endif
        foreach (var method in methods) {
          var discoveredMethodTests = DiscoverMethodTests(type, method, suite, methods, assembly);
          if (discoveredMethodTests) typeHasAnyTests = true;
        }

        if (typeHasAnyTests is false) {
          var typeIsSpecType = TestNameUtility.MatchesSpecGroupPattern(type.Name, suite);
          if (typeIsSpecType) {
            #if NO_GET_TYPE_INFO_AVAILABLE
            foreach (var constructor in type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)) {
            #else
            foreach (var constructor in type.GetTypeInfo().DeclaredConstructors) {
            #endif
              if (constructor.GetParameters().Length == 0) {
                try {
                  constructor.Invoke(null); // Call it, it may define specs!
                } catch (Exception e) {
                  suite.Config.StandardOutput.WriteLine($"Whoops, maybe shouldn't have called the constructor for {type.FullName}, it got angry. {e.Message}");
                }
              }
            }
          }
        }
      }
    }

    bool DiscoverMethodTests(Type type, MethodInfo method, ITestSuite suite, List<MethodInfo> typeMethods, Assembly assembly) {
      if (suite.Config is null)
        throw new Exception("Please set ITestSuite.Config before calling DiscoverTests");

      var anyDiscovered = false;
      // TODO UPDATE TO NOT CHECK FULL NAME, ONLY THE DECLARING TYPE NAME
      var typeIsTestGroup = TestNameUtility.MatchesTestGroupPattern(type.Name, suite) || TestNameUtility.MatchesTestGroupPattern((type.FullName is not null) ? type.FullName : type.Name, suite);
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
        ((TestSuite) suite).Tests.Add(new Test(
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
        if (method.DeclaringType is null)
          throw new NotSupportedException($"Provided method {method.Name} has no DeclaringType, unsupported by MiniSpec at this time.");
        #if NO_GET_TYPE_INFO_AVAILABLE
        foreach (var constructor in method.DeclaringType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)) {
        #else
        foreach (var constructor in method.DeclaringType.GetTypeInfo().DeclaredConstructors) {
        #endif
          if (constructor.GetParameters().Length == 0) {
            var instance = Activator.CreateInstance(method.DeclaringType);
            return () => method.Invoke(instance, null);
          }
        }
        // If we didn't find a constructor without arguments, meh, let's try to make an instance anyway!
        // var anotherInstance = Activator.CreateInstance(method.DeclaringType);
        // return () => method.Invoke(anotherInstance, null);
      }
      throw new NotImplementedException($"Don't yet know how to invoke method/function: {TestNameUtility.FullMethodName(method)}");
    }
  }
}

// AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveCallback;
// static Assembly AssemblyResolveCallback(object sender, ResolveEventArgs args)