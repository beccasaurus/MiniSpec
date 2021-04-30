using FluentAssertions;
using NUnit.Framework;

namespace Specs.CLI {

  [TestFixture]
  public class VersionSpec : Spec {

    [Test]
    public void GetMiniSpecVersion() {
      var project = CreateProject(framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"return MiniSpec.Tests.Run(args);");

      project.Run("--version");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();
      project.RunResult.StandardOutput.Should().Contain("MiniSpec version");
    }
  }
}