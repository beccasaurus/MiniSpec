using System;

using MiniSpec.Testing;
using MiniSpec.Testing.Utilities;

namespace MiniSpec.Private.Testing.Reporters {
  internal class DryRunReporter : ITestReporter {
    public void BeforeDiscovery(ITestSuite suite) {  }
    public void AfterDiscovery(ITestSuite suite) {  }
    public void BeforeSuite(ITestSuite suite) {  }
    public void AfterSuite(ITestSuite suite) {  }
    public void BeforeTest(ITestSuite suite, ITest test) {
      if (suite.Config.Verbose)
        suite.Config.STDOUT.WriteLine(test.FullName);
      else
        suite.Config.STDOUT.WriteLine(test.Name);
    }
    public void AfterTest(ITestSuite suite, ITest test) {  }
  }
}