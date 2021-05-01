using MiniSpec.Testing;

namespace MiniSpec.Specs.DSL {
  public interface ISpecContext {
    // string SpecName { get; set; }
    void Describe(string description, TestAction<ISpecContext> body);
    void It(string description, TestAction body);
    void It(string description);
    void Can(string description, TestAction body);
    void Can(string description);
    void Should(string description, TestAction body);
    void Should(string description);
    void Test(string description, TestAction body);
    void Test(string description);

    // void Example(string description, SpecBody body);
    // void Before(SetupBody body);
    // void Setup(SetupBody body);
    // void After(TeardownBody body);
    // void Teardown(TeardownBody body);
  }
}