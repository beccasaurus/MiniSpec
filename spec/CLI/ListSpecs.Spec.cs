using FluentAssertions;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Specs.CLI {

  [TestFixture]
  public class ListSpecsSpec : Spec {

    [Test]
    public void ListTopLevelLocalFunctions() {
      var project = CreateProject(csharp: 9, framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      void TestSomething() {}
      void UnreleatedFunction1() {}
      void SpecSomething() {}
      void UnreleatedFunction2() {}
      return MiniSpec.Tests.Run(args);");

      project.Run("-l");

      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      foreach (var expectedTestName in new[] { "TestSomething", "SpecSomething" })
        new Regex($"^{expectedTestName}", RegexOptions.Multiline)
        .IsMatch(project.RunResult.StandardOutput).Should().BeTrue($"Expected listed test name: {expectedTestName}");

      foreach (var unexpectedTestName in new[] { "UnreleatedFunction1", "UnreleatedFunction2" })
        new Regex($"^{unexpectedTestName}", RegexOptions.Multiline)
        .IsMatch(project.RunResult.StandardOutput).Should().BeFalse($"Expected this not to be listed as a test name: {unexpectedTestName}");
    }
  }
}