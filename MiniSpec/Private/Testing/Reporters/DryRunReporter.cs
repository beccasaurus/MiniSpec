using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Reporters {
  internal class DryRunReporter : ITestReporter {
    public void BeforeDiscovery(ITestSuite suite) {  }
    public void AfterDiscovery(ITestSuite suite) {  }
    public void BeforeSuite(ITestSuite suite) {  }
    public void AfterSuite(ITestSuite suite) {  }
    public void BeforeTest(ITestSuite suite, ITest test) {
      suite.Config.STDOUT.WriteLine(test.FullName);
    }
    public void AfterTest(ITestSuite suite, ITest test) {  }
  }
}