using System;
using System.IO;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Executors {
  internal class TestExecutor : ITestExecutor {
    public TestStatus RunTest(ITestSuite suite, ITest test) {
      var originalStdout = Console.Out;
      var originalStderr = Console.Error;
      var mockStdout = new StringWriter();
      var mockStderr = new StringWriter();
      Console.SetOut(mockStdout);
      Console.SetError(mockStderr);

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
        Console.SetOut(originalStdout);
        Console.SetError(originalStderr);
        test.STDOUT = mockStdout.ToString();
        test.STDERR = mockStderr.ToString();
      }
      suite.Config.TestReporter.AfterTest(suite, test);
      return test.Status;
    }
  }
}