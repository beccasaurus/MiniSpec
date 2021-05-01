using FluentAssertions;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Specs.CLI {

  [TestFixture]
  public class ListDetailsSpec : Spec {

    [TestCase(Project.TargetFrameworks.Net50)]
    public void ListDetails_InstanceMethods(Project.TargetFrameworks framework) {
      var project = CreateProject(framework: framework, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      public class Program { public static int Main(string[] args) { return MiniSpec.Tests.Run(args); }}

      namespace Tests {
        class Dog {
          void ItBarks() {}
        }
      }
      ");

      project.Run("-lv");
      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();
      project.RunResult.StandardOutput.Should().Contain("Tests.Dog.ItBarks");
    }
  }
}