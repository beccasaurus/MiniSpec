namespace MiniSpec.Testing {
  public interface ITestExecutor {
    TestStatus RunTest(ITestSuite suite, ITest test);
  }
}