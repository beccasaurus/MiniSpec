using System;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Executors {
  internal class TestExecutor : ITestExecutor {
    public TestStatus RunTest(ITestSuite suite, ITest test) {
      suite.Config.TestReporter.BeforeTest(suite, test);
      // if (test.Method.IsStatic) {
      // } else {
      //   throw new NotImplementedException("Only static methods can currently be run");
      // }
      suite.Config.TestReporter.AfterTest(suite, test);
      return test.Status;
    }
  }
}