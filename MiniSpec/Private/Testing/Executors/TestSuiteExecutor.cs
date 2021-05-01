using System;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Executors {
  internal class TestSuiteExecutor : ITestSuiteExecutor {
    public TestStatus RunTestSuite(ITestSuite suite) {
      suite.Config.TestReporter.BeforeSuite(suite);
      foreach (var test in suite.Tests)
        suite.Config.TestExecutor.RunTest(suite, test);
      suite.Config.TestReporter.AfterSuite(suite);
      return suite.Status;
    }
  }
}