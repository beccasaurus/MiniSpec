using System;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Executors {
  internal class TestSuiteExecutor : ITestSuiteExecutor {
    public TestStatus RunTestSuite(ITestSuite suite) {
      if (suite.Config is null) throw new Exception("Please set ITestSuite.Config before running TestSuiteExecutor");
      if (suite.Config.TestExecutor is null) throw new Exception("Please set ITestSuite.Config.TestExecutor before running TestSuiteExecutor");
      if (suite.Config.TestReporter is null) throw new Exception("Please set ITestSuite.Config.TestReporter before running TestSuiteExecutor");

      suite.Config.TestReporter.BeforeSuite(suite);
      foreach (var test in suite.Tests)
        suite.Config.TestExecutor.RunTest(suite, test);
      suite.Config.TestReporter.AfterSuite(suite);
      return suite.Status;
    }
  }
}