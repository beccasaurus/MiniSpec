using System;

using MiniSpec.Testing;
using MiniSpec.Private.Utilities;

namespace MiniSpec.Private.Testing.Reporters {
  internal class DocumentationReporter : ITestReporter {
    public void BeforeDiscovery(ITestSuite suite) { if (suite.Config.Verbose && ! suite.Config.Quiet) suite.Config.STDOUT.WriteLine("Loading tests..."); }
    public void AfterDiscovery(ITestSuite suite) { if (suite.Config.Verbose && ! suite.Config.Quiet) suite.Config.STDOUT.WriteLine($"Found {EnumerableUtility.GetCount(suite.Tests)} tests"); }
    public void BeforeSuite(ITestSuite suite) { if (suite.Config.Verbose && ! suite.Config.Quiet) suite.Config.STDOUT.WriteLine("Running tests..."); }
    public void AfterSuite(ITestSuite suite) {
      // Output summary
    }
    public void BeforeTest(ITestSuite suite, ITest test) {
      // Track the test parent names and do depth indentation etc, update test fullname to be an array instead of a string (don't use a separator)
    }
    public void AfterTest(ITestSuite suite, ITest test) {
      if (suite.Config.Quiet) return;

      switch (test.Status) {
        case TestStatus.Passed: suite.Config.STDOUT.WriteLine($"[PASS] {test.FullName}"); break; // TODO add color (configurable)
        case TestStatus.Failed: suite.Config.STDOUT.WriteLine($"[FAIL] {test.FullName}"); break; // TODO add color (configurable)
        case TestStatus.Skipped: suite.Config.STDOUT.WriteLine($"[SKIP] {test.FullName}"); break; // TODO add color (configurable)
        case TestStatus.NotRun: suite.Config.STDOUT.WriteLine($"[NOT RUN] {test.FullName}"); break; // TODO add color (configurable)
        default: break;
      }

      if ((suite.Config.Verbose || test.Status == TestStatus.Failed) && ! string.IsNullOrEmpty(test.STDOUT)) {
        suite.Config.STDOUT.WriteLine("[Standard Output]");
        suite.Config.STDOUT.WriteLine(test.STDOUT);
      }

      if ((suite.Config.Verbose || test.Status == TestStatus.Failed) && ! string.IsNullOrEmpty(test.STDERR)) {
        suite.Config.STDOUT.WriteLine("[Standard Error]");
        suite.Config.STDOUT.WriteLine(test.STDERR);
      }

      if (test.Exception is not null) {
        var exception = test.Exception;
        while (exception is not null && exception is System.Reflection.TargetInvocationException)
          exception = exception.InnerException;
        if (exception is null) exception = test.Exception;
        suite.Config.STDOUT.WriteLine("[Exception]");
        suite.Config.STDOUT.WriteLine(exception.Message);
        suite.Config.STDOUT.WriteLine(exception.StackTrace);
      }
    }
  }
}