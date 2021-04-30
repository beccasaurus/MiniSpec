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

    internal List<string> TEST_OBJECT_NAME_PATTERNS = new List<string> { "^[a-zA-Z].*Test", "^[a-zA-Z].*Spec", "^Test", "^Spec" };
    internal List<string> TEST_OBJECT_CHILD_TEST_NAME_PATTERNS = new List<string> { "^[a-zA-Z]" };

    internal void FindAndExecutorTests()
    {
      foreach (var type in _assembly.GetTypes())
      {
        var isTestType = false;
        foreach (var pattern in TEST_OBJECT_NAME_PATTERNS)
        {
          var typeMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
          foreach (var method in typeMethods)
          {
            var methodName = GetTestMethodName(method.Name);
            var isTestMethod = false;
            foreach (var testObjectPattern in TEST_OBJECT_NAME_PATTERNS) {
                if (new Regex(testObjectPattern).IsMatch(methodName)) {
                    isTestMethod = true;
                    break;
                }
            }
            if (isTestMethod) {
                _reporter.BeforeTest(methodName);
                var result = _executor.ExecuteTestMethod(method);
            }
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
