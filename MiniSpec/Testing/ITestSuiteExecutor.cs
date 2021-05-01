namespace MiniSpec.Testing {
  public interface ITestSuiteExecutor {
    TestStatus RunTestSuite(ITestSuite suite);
  }
}