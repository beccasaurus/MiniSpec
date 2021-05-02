using System;
using System.IO;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Executors {
  internal class TestExecutor : ITestExecutor {
    public TestStatus RunTest(ITestSuite suite, ITest test) {
      if (suite.Config is null) throw new Exception("Please set ITestSuite.Config before running TestExecutor");
      if (suite.Config.TestReporter is null) throw new Exception("Please set ITestSuite.Config.TestReporter before running TestExecutor");

      var originalStandardOutput = Console.Out;
      var originalStandardError = Console.Error;
      var mockStandardOutput = new StringWriter();
      var mockStandardError = new StringWriter();
      Console.SetOut(mockStandardOutput);
      Console.SetError(mockStandardError);

      suite.Config.TestReporter.BeforeTest(suite, test);
      try {
        test.ReturnObject = test.Invoke();
        if (test.Method is not null) {
          if (test.Method.ReturnType == typeof(bool)) {
            test.Status = (test.ReturnObject is bool && (bool) test.ReturnObject == true) ? TestStatus.Passed : TestStatus.Failed;
          } else {
            test.Status = TestStatus.Passed;
          }
        }
      } catch (Exception e) {
        test.Exception = e;
        test.Status = TestStatus.Failed;
      } finally {
        Console.SetOut(originalStandardOutput);
        Console.SetError(originalStandardError);
        test.StandardOutput = mockStandardOutput.ToString();
        test.StandardError = mockStandardError.ToString();
      }
      suite.Config.TestReporter.AfterTest(suite, test);
      return test.Status;
    }
  }
}