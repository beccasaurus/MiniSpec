using MiniSpec.Testing;
using MiniSpec.Specs.DSL;
using MiniSpec.Private.Specs.DSL;

namespace MiniSpec {
  public class Spec {
    public static void Describe(string description, TestAction<ISpecContext> body) {
      var describeBlock = new DescribeBlock(description);
      var specContext = new SpecContext(describeBlock);
      body(specContext);
    }

    // public static void It(string description, SpecBody body) {
      
    // }

    // public static void Can(string description, SpecBody body) { It(description, body); }
    // public static void Should(string description, SpecBody body) { It(description, body); }
    // public static void Example(string description, SpecBody body) { It(description, body); }

    // public static void Before(SetupBody body) {
      
    // }
    // public static void Setup(SetupBody body) { Before(body); }

    // public static void After(TeardownBody body) {
      
    // }
    // public static void Teardown(TeardownBody body) { After(body); }

    // public static void BeforeSuite(SetupBody body) {

    // }
    // public static void SuiteSetup(SetupBody body) { BeforeSuite(body); }

    // public static void AfterSuite(TeardownBody body) {

    // }
    // public static void SuiteTeardown(TeardownBody body) { AfterSuite(body); }
  }
}