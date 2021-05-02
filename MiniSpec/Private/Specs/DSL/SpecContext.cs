using System.Reflection;

using MiniSpec.Private.Testing;
using MiniSpec.Specs.DSL;
using MiniSpec.Testing;

namespace MiniSpec.Private.Specs.DSL {
  internal class SpecContext : ISpecContext {
    internal SpecContext(DescribeBlock describeBlock) {
      _describeBlock = describeBlock;
    }
    
    public void Describe(string description, TestAction<ISpecContext> body) {
      var describeBlock = new DescribeBlock(description, DescribeBlock);
      var specContext = new SpecContext(describeBlock);
      body(specContext);
    }

    public void RegisterTest(string description, TestAction? body = null) {
      // Register test! Walk up the describe tree to get all SETUP and TEARDOWN for this :)
      var test = new Test(
        invoke: () => { if (body is not null) body(); return null; }, // TODO setup/teardown :)
        name: description,
        fullName: (DescribeBlock is null) ? description : $"{DescribeBlock.FullDescription} {description}",
        typeName: null,
        methodName: null,
        assemblyLocation: Assembly.GetEntryAssembly().Location,
        method: null,
        assembly: null
      );
      if (body is null) test.Status = TestStatus.Skipped;
      TestSuite.InitializeOrGetGlobalInstance().Tests.Add(test);
    }

    public void It(string description, TestAction body) { RegisterTest($"It {description}", body); }
    public void Can(string description, TestAction body) { RegisterTest($"Can {description}", body); }
    public void Should(string description, TestAction body) { RegisterTest($"Should {description}", body); }
    public void Test(string description, TestAction body) { RegisterTest(description, body); }


    public void It(string description) { RegisterTest($"It {description}"); }
    public void Can(string description) { RegisterTest($"Can {description}"); }
    public void Should(string description) { RegisterTest($"Should {description}"); }
    public void Test(string description) { RegisterTest(description); }

    DescribeBlock? _describeBlock;
    public IDescribeBlock? DescribeBlock { get => _describeBlock; }
  }
}