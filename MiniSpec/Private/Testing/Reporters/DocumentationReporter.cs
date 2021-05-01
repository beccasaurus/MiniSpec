using System;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Reporters {
  internal class DocumentationReporter : ITestReporter {
    public void BeforeDiscovery(ITestSuite suite) { throw new NotImplementedException(); }
    public void AfterDiscovery(ITestSuite suite) { throw new NotImplementedException(); }
    public void BeforeSuite(ITestSuite suite) { throw new NotImplementedException(); }
    public void AfterSuite(ITestSuite suite) { throw new NotImplementedException(); }
    public void BeforeTest(ITestSuite suite, ITest test) { throw new NotImplementedException(); }
    public void AfterTest(ITestSuite suite, ITest test) { throw new NotImplementedException(); }
  }
}