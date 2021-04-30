using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniSpec
{
  class TestDiscoverer
  {

    Assembly _assembly;
    TestSuite _suite;
    TestExecutor _executor;
    TestReporter _reporter;

    internal TestDiscoverer(Assembly assembly, TestSuite testSuite, TestExecutor testExecutor, TestReporter testReporter)
    {
      _assembly = assembly;
      _suite = testSuite;
      _executor = testExecutor;
      _reporter = testReporter;
    }

    internal List<string> TEST_OR_TEST_GROUP_NAME_PATTERNS = new List<string> { "^[A-Z].*Test", "^[A-Z].*Spec", "^Test", "^Spec" };
    internal List<string> TEST_GROUP_CHILD_TEST_NAME_PATTERNS = new List<string> { "^[A-Z].*Test", "^[A-Z].*Spec", "^Test", "^Spec", "^It", "^Should", "^Can" };

    internal void FindAndExecutorTests()
    {
      foreach (var type in _assembly.GetTypes())
      {
        var isTestType = false;
        foreach (var pattern in TEST_OR_TEST_GROUP_NAME_PATTERNS)
          if (new Regex(pattern).IsMatch(type.Name) || new Regex(pattern).IsMatch(type.FullName)) { isTestType = true; break; }

        var typeMethods = new List<MethodInfo>(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance));
        typeMethods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));

        foreach (var method in typeMethods) {
          var methodName = GetTestMethodName(method.Name);
          var isTestMethod = false;
          var patternsToTest = isTestType ? TEST_GROUP_CHILD_TEST_NAME_PATTERNS : TEST_OR_TEST_GROUP_NAME_PATTERNS;
          foreach (var pattern in patternsToTest)
              if (new Regex(pattern).IsMatch(methodName)) { isTestMethod = true; break; }
          if (isTestMethod) {
              _reporter.BeforeTest(methodName);
              var result = _executor.ExecuteTestMethod(method);
          }
        }
      }
    }

    string GetTestMethodName(string reflectionMethodName)
    {
      // Example local function format: "<<Main>$>g__UnreleatedFunction1|0_1"
      if (reflectionMethodName.Contains(">g__") && reflectionMethodName.Contains("|"))
        return new Regex(".*g__([^|]+)|").Match(reflectionMethodName).Groups[1].Value;
      else
        return reflectionMethodName;
    }
  }
}
