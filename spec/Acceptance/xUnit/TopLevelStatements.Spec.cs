using FluentAssertions;

using NUnit.Framework;

namespace Specs.xUnit {

  [TestFixture]
  public class TopLevelStatementSpecs : Spec {

    [TestCase(Project.TargetFrameworks.Net50)]
    public void xUnit_TopLevelStatements(Project.TargetFrameworks framework) {
      Assert.Ignore("This is one of the next specs to implement, not ready yet, currently refactoring :)");

      var project = CreateProject(csharp: 9, framework: framework, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      #pragma warning disable 8321

      using System;

      bool TestShouldPass() => true;
      bool TestShouldFail() => false;
      void TestShouldAlsoFail() { throw new System.Exception(""Kaboom!""); }

      return MiniSpec.Tests.Run(Console.Out, Console.Error, args);");

      project.Run("--no-color");
      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.Failed.Should().BeTrue();

      var output = project.RunResult.StandardOutput;
      output.Should().Contain("[PASS] TestShouldPass");
      output.Should().Contain("[FAIL] TestShouldFail");
      output.Should().Contain("Kaboom!");
    }

    // TODO soon :)

    // [Test]
    // public void TestSomething() {
    //   var project = CreateProject(framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
    //   project.WriteFile("Program.cs", @"MiniSpec.Tests.Run();");
    //   project.Run();

    //   project.RunResult.OK.Should().Equals(true);
    //   project.RunResult.StandardOutput.Should().Contain("Hello, world!"); // <--- this means it ran MiniSpec.Tests.Run OK (for now)
    // }
  }
}