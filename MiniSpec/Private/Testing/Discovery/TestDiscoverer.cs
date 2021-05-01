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
        Assembly assembly;
        if (assemblyPath == Assembly.GetEntryAssembly().Location) {
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
            suite.Config.STDERR.WriteLine($"Failed to load test project {dllName}");
            suite.Config.STDERR.WriteLine($"Full path: {assemblyPath}");
            suite.Config.STDERR.WriteLine($"Error message: {e.Message}");
            return;
          }
        }
        DiscoverTestsInAssembly(assembly, suite);
      }
    }

    void DiscoverTestsInAssembly(Assembly assembly, ITestSuite suite) {
      foreach (var type in assembly.GetTypes()) 
        DiscoverTestsInType(type, suite, assembly);
    }

    void DiscoverTestsInType(Type type, ITestSuite suite, Assembly assembly) {
      var methods = new List<MethodInfo>(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance));
      methods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));
      foreach (var method in methods)
        DiscoverMethodTests(type, method, suite, methods, assembly);
    }

    void DiscoverMethodTests(Type type, MethodInfo method, ITestSuite suite, List<MethodInfo> typeMethods, Assembly assembly) {
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

      if (methodMatchesTestName && ! methodHasChildTests) {
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
    }

    static bool MethodHasChildFunctionMatchingPattern(MethodInfo thisMethod, List<MethodInfo> typeMethods, IEnumerable<Regex> patterns) {
      foreach (var method in typeMethods)
        if (method.Name.StartsWith($"<{thisMethod.Name}>"))
          if (TestNameUtility.MatchesAnyPattern(TestNameUtility.LocalFunctionName(method.Name)!, patterns))
            return true;
      return false;
    }

    static TestAction GetInvokeAction(MethodInfo method) {
      if (method.IsStatic) {
        return () => { method.Invoke(null, null); };
      } else {
        foreach (var constructor in method.DeclaringType.GetConstructors()) {
          if (constructor.GetParameters().Length == 0) {
            var instance = Activator.CreateInstance(method.DeclaringType);
            return () => { method.Invoke(instance, null); };
          }
        }
      }
      throw new NotImplementedException($"Don't yet know how to invoke method/function: {TestNameUtility.FullMethodName(method)}");
    }
  }
}

// AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveCallback;
// static Assembly AssemblyResolveCallback(object sender, ResolveEventArgs args)