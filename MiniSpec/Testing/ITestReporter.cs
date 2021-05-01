namespace MiniSpec.Testing {
  public interface ITestReporter {
    void BeforeDiscovery(ITestSuite suite);
    void AfterDiscovery(ITestSuite suite);
    void BeforeSuite(ITestSuite suite);
    void AfterSuite(ITestSuite suite);
    void BeforeTest(ITestSuite suite, ITest test);
    void AfterTest(ITestSuite suite, ITest test);
  }
}