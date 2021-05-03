using System.Text.RegularExpressions;

using MiniSpec.Testing;
using MiniSpec.Private.Utilities;

namespace MiniSpec.Private.Testing.Reporters {
  internal class DocumentationReporter : ITestReporter {
    public void BeforeDiscovery(ITestSuite suite) { if (suite.Config is not null && suite.Config.Verbose && ! suite.Config.Quiet) suite.Config.StandardOutput.WriteLine("Loading tests..."); }
    public void AfterDiscovery(ITestSuite suite) { if (suite.Config is not null && suite.Config.Verbose && ! suite.Config.Quiet) suite.Config.StandardOutput.WriteLine($"Found {EnumerableUtility.GetCount(suite.Tests)} tests"); }
    public void BeforeSuite(ITestSuite suite) { if (suite.Config is not null && suite.Config.Verbose && ! suite.Config.Quiet) suite.Config.StandardOutput.WriteLine("Running tests..."); }
    public void AfterSuite(ITestSuite suite) {
      // Output summary
    }
    public void BeforeTest(ITestSuite suite, ITest test) {
      // Track the test parent names and do depth indentation etc, update test fullname to be an array instead of a string (don't use a separator)
    }
    public void AfterTest(ITestSuite suite, ITest test) {
      if (suite.Config!.Quiet) return;

      var red = "\u001b[31m";
      var boldRed = "\u001b[31;1m";
      var green = "\u001b[32m";
      var yellow = "\u001b[33m";
      // var blue = "\u001b[34m";
      var boldBlue = "\u001b[34;1m";
      // var cyan = "\u001b[36m";
      var boldCyan = "\u001b[36;1m";
      var lightYellow = "\u001b[93m";
      var reset = "\u001b[0m";

      switch (test.Status) {
        case TestStatus.Passed: suite.Config.StandardOutput.WriteLine($"[{green}PASS{reset}] {test.FullName}"); break; // TODO add color (configurable)
        case TestStatus.Failed: suite.Config.StandardOutput.WriteLine($"[{red}FAIL{reset}] {test.FullName}"); break; // TODO add color (configurable)
        case TestStatus.Skipped: suite.Config.StandardOutput.WriteLine($"[{yellow}SKIP{reset}] {test.FullName}"); break; // TODO add color (configurable)
        case TestStatus.NotRun: suite.Config.StandardOutput.WriteLine($"[{lightYellow}NOT RUN{reset}] {test.FullName}"); break; // TODO add color (configurable)
        default: break;
      }

      if ((suite.Config.Verbose || test.Status == TestStatus.Failed) && ! string.IsNullOrEmpty(test.StandardOutput)) {
        suite.Config.StandardOutput.WriteLine($"  [{boldBlue}Standard Output{reset}]");
        suite.Config.StandardOutput.WriteLine(Indent(test.StandardOutput));
      }

      if ((suite.Config.Verbose || test.Status == TestStatus.Failed) && ! string.IsNullOrEmpty(test.StandardError)) {
        suite.Config.StandardOutput.WriteLine($"  [{boldCyan}Standard Error{reset}]");
        suite.Config.StandardOutput.WriteLine(Indent(test.StandardError));
      }

      if (test.Exception is not null) {
        var exception = test.Exception;
        while (exception is not null && exception is System.Reflection.TargetInvocationException)
          exception = exception.InnerException;
        if (exception is null) exception = test.Exception;
        suite.Config.StandardOutput.WriteLine($"  [{boldRed}Exception{reset}]");
        suite.Config.StandardOutput.WriteLine(Indent(exception.Message));
        if (exception.StackTrace is not null) suite.Config.StandardOutput.WriteLine(Indent(exception.StackTrace));
      }
    }

    static readonly Regex REPLACE_START_OF_LINE = new Regex("^");
    static string Indent(string text, string indentation = "  ", int depth = 1) {
      return REPLACE_START_OF_LINE.Replace(text, indentation);
    }
  }
}