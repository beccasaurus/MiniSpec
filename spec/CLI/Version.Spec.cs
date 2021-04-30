using FluentAssertions;
using NUnit.Framework;

namespace Specs.CLI {

  [TestFixture]
  public class VersionSpec : Spec {

    [Test]
    public void TestGetMiniSpecVersion() {
      var project = CreateProject(framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"MiniSpec.Tests.Run(args);");

      project.Run("--version");

      project.RunResult.OK.Should().Equals(true);
      project.RunResult.StandardOutput.Should().Contain("MiniSpec version");
    }
  }
}