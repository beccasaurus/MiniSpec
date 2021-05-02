using FluentAssertions;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Specs.CLI {

  [TestFixture]
  public class ListSpec : Spec {

    [Test]
    public void List_TopLevelLocalFunctions() {
      var project = CreateProject(csharp: 9, framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      #pragma warning disable 8321

      using System;

      void TestSomething() {}
      void UnreleatedFunction1() {}
      void SpecSomething() {}
      void UnreleatedFunction2() {}

      return MiniSpec.Tests.Run(Console.Out, Console.Error, args);");

      project.Run("-l");
      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      var output = project.RunResult.StandardOutput;
      foreach (var expectedTestName in new[] { "TestSomething", "SpecSomething" })
        output.Should().Contain(expectedTestName);
      foreach (var unexpectedTestName in new[] { "UnreleatedFunction1", "UnreleatedFunction2" })
        output.Should().NotContain(unexpectedTestName);
    }

    // [TestCase(Project.TargetFrameworks.Net20)]
    [TestCase(Project.TargetFrameworks.Net50)]
    public void List_InstanceMethods(Project.TargetFrameworks framework) {
      var project = CreateProject(framework: framework, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      #pragma warning disable 8321

      using System;

      public class Program { public static int Main(string[] args) { return MiniSpec.Tests.Run(Console.Out, Console.Error, args); }}

      class RegularClass {
        void TestOne() {}
        void TestTwo() {}
        void notATest() {}
        void _NotATest() {}
        void ItDoesSomething()   {} // Not a test because 'It' only works in a test group
        void ShouldDoSomething() {} // Not a test because 'It' only works in a test group
        void CanDoSomething()    {} // Not a test because 'It' only works in a test group
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
      if (project.RunResult.StandardOutput.Contains("The runtime version supported by this application is unavailable"))
        Assert.Ignore($"Framework {framework} unsupported on this machine, skipping test");

      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      var output = project.RunResult.StandardOutput;
      foreach (var expectedTestName in new[] { "TestOne", "TestTwo", "ItBarks", "ShouldBark", "CanBark", "ItMeows", "ShouldMeow", "CanMeow" })
        output.Should().Contain(expectedTestName);
      foreach (var unexpectedTestName in new[] { "ItDoesSomething", "ShouldDoSomething", "CanDoSomething", "notATest", "_NotATest", "UnrelatedDog", "UnrelatedCat" })
        output.Should().NotContain(unexpectedTestName);
    }

    // [TestCase(Project.TargetFrameworks.Net20)]
    [TestCase(Project.TargetFrameworks.Net50)]
    public void List_LocalFunctions(Project.TargetFrameworks framework) {
      var project = CreateProject(framework: framework, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      #pragma warning disable 8321

      using System;

      public class Program { public static int Main(string[] args) { return MiniSpec.Tests.Run(Console.Out, Console.Error, args); }}

      class RegularClass {
        void RegularMethod() {
          void TestFoo() {}
          void ItDoesFoo() {}
        }
        void AnotherMethod() {}
        void TestMethod() { // Because this has children, it won't show up as its own test
          void TestBar() {}
          void ItDoesBar() {}
        }
        void Inception() {
          void RegularFunction() {
            void TestFunction() {
              void TestWow() {} // Unfortunately, we know that these are under Inception() but not that they are under Regular or Test function via Reflection
              void ShouldWow() {} // So we will only support 'It'/'Should'/'Can' under a method or class with a Test/Spec name (but TestWow still works!)
            }
            void AnotherFunction() {}
          }
        }
      }
      ");

      project.Run("-l");
      if (project.RunResult.StandardOutput.Contains("The runtime version supported by this application is unavailable"))
        Assert.Ignore($"Framework {framework} unsupported on this machine, skipping test");
      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      var output = project.RunResult.StandardOutput;
      foreach (var expectedTestName in new[] { "TestFoo", "TestBar", "ItDoesBar", "TestWow" })
        output.Should().Contain(expectedTestName);
      foreach (var unexpectedTestName in new[] { "ItDoesFoo", "AnotherMethod" })
        output.Should().NotContain(unexpectedTestName);
    }

    [Test]
    public void List_BDD_TopLevelStatements() {
      var project = CreateProject(csharp: 9, framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"

      using System;
      using static MiniSpec.Spec;

      Describe(""Dog"", dog => {
        dog.Can(""Bark"", () => { /* ... */ });
        dog.Can(""Sit"", () => { /* ... */ });

        dog.Describe(""Barking"", barking => {
          barking.Can(""Be Annoying"");
          barking.Should(""Be Quiet"");
        });
      });

      return MiniSpec.Tests.Run(Console.Out, Console.Error, args);");

      project.Run("-l");
      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      var output = project.RunResult.StandardOutput;
      output.Should().Contain("Dog Can Bark");
      output.Should().Contain("Dog Can Sit");
      output.Should().Contain("Dog Barking Can Be Annoying");
      output.Should().Contain("Dog Barking Should Be Quiet");
    }

    [Test]
    public void List_BDD_DefinedInClasses() {
      var project = CreateProject(csharp: 9, framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"
      using System;
      using MiniSpec;

      return MiniSpec.Tests.Run(Console.Out, Console.Error, args);

      class Specs {
        Specs() {
          Spec.Describe(""Dog"", dog => {
            dog.Can(""Bark"", () => { /* ... */ });
            dog.Can(""Sit"", () => { /* ... */ });

            dog.Describe(""Barking"", barking => {
              barking.Can(""Be Annoying"");
              barking.Should(""Be Quiet"");
            });
          });
        }
      }

      ");

      project.Run("-l");
      System.Console.WriteLine($"OUTPUT: {project.RunResult.StandardOutput}");

      project.RunResult.StandardError.Should().BeEmpty();
      project.RunResult.OK.Should().BeTrue();

      var output = project.RunResult.StandardOutput;
      output.Should().Contain("Dog Can Bark");
      output.Should().Contain("Dog Can Sit");
      output.Should().Contain("Dog Barking Can Be Annoying");
      output.Should().Contain("Dog Barking Should Be Quiet");
    }
  }
}