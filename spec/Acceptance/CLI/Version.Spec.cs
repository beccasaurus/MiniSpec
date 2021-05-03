using FluentAssertions;
using NUnit.Framework;

namespace Specs.CLI {

  [TestFixture]
  public class VersionSpec : Spec {

    [TestCase(Project.TargetFrameworks.Core10)]
    [TestCase(Project.TargetFrameworks.Core11)]
    [TestCase(Project.TargetFrameworks.Core22)]
    [TestCase(Project.TargetFrameworks.Core31)]
    [TestCase(Project.TargetFrameworks.Net20)]
    [TestCase(Project.TargetFrameworks.Net35)]
    [TestCase(Project.TargetFrameworks.Net40)]
    [TestCase(Project.TargetFrameworks.Net452)]
    [TestCase(Project.TargetFrameworks.Net462)]
    [TestCase(Project.TargetFrameworks.Net48)]
    [TestCase(Project.TargetFrameworks.Net50)]
    public void GetMiniSpecVersion(Project.TargetFrameworks framework) {
      var project = CreateProject(csharp: 9, framework: framework, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"return MiniSpec.Tests.Run(System.Console.Out, System.Console.Error, args);");

      project.Run("--version");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();
      project.RunResult.StandardOutput.Should().Contain("MiniSpec version");
    }
  }
}