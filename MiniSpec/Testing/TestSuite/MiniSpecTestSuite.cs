using MiniSpec.Testing.Configuration;

namespace MiniSpec.Testing {
  public class MiniSpecTestSuite : TestSuite, ITestSuite {
    public MiniSpecTestSuite(IConfig? config = null) : base(config) { }
  }
}