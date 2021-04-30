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

    // [TestCase(framework, TestName = "")] ~ for different framework versions (skip unsupported with Assert.Ignore())
    [Test]
    public void ListInstanceMethodsOfClass() {
      var project = CreateProject(csharp: 9, framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      return MiniSpec.Tests.Run(args);
      class RegularClass {
        void TestOne() {}
        void TestTwo() {}
        void notATest() {}
        void _NotATest() {}
        void ItDoesSomething() {} // Not a test because 'It' only works in a test group
      }
      class DogTest {
        void ItBarks() {}
        void ShouldBark() {}
        void CanBark() {}
        void UnrelatedDog() {}
      }
      namespace Tests {
        class Cat {
          void ItMeows() {}
          void ShouldMeow() {}
          void CanMeow() {}
          void UnrelatedCat() {}
        }
      }
      ");

      project.Run("-l");

      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      foreach (var expectedTestName in new[] { "TestOne", "TestTwo", "ItBarks", "ShouldBark", "CanBark", "ItMeows", "ShouldMeow", "CanMeow" })
        new Regex($"^{expectedTestName}", RegexOptions.Multiline)
        .IsMatch(project.RunResult.StandardOutput).Should().BeTrue($"Expected listed test name: {expectedTestName}");

      foreach (var unexpectedTestName in new[] { "notATest", "_NotATest", "UnrelatedDog", "UnrelatedCat" })
        new Regex($"^{unexpectedTestName}", RegexOptions.Multiline)
        .IsMatch(project.RunResult.StandardOutput).Should().BeFalse($"Expected this not to be listed as a test name: {unexpectedTestName}");
    }

    // Next: --list --details (details reporter) for Setup/Teardown/Constructor/Dispose/Group
  }
}