using FluentAssertions;

using NUnit.Framework;

namespace Specs.xUnit
{
  [TestFixture]
  public class CustomReporterSpecs : Spec
  {

    [TestCase(Project.TargetFrameworks.Net50)]
    public void CustomReporterTest(Project.TargetFrameworks framework) {
      var packagesFolder = CreateDirectory("Packages");

      var extensionProject = CreateProject(name: "CoolReporter", assemblyName: "MiniSpec.CoolReporter", csharp: 9, framework: framework, type: Project.OutputTypes.Library, includeMiniSpec: false, packageOutputPath: packagesFolder);
      extensionProject.WriteFile("CoolReporter.cs", @"
      public class CoolReporter {
        public void AfterTest(dynamic suite, dynamic test) {
          System.Console.WriteLine(""Hello! The {test.FullName} test ran and the result was {test.Status}"");
        }
      }");
      extensionProject.Build();

      var testProject = CreateProject(csharp: 9, framework: framework, type: Project.OutputTypes.Exe, packagesFolder: packagesFolder);
      testProject.WriteFile("Program.cs", @"
      #pragma warning disable 8321

      using System;

      bool TestShouldPass() => true;
      bool TestShouldFail() => false;

      return MiniSpec.Tests.Run(Console.Out, Console.Error, args);");
      testProject.RunCommand("dotnet", "add", "package", "CoolReporter");

      testProject.Run();

      System.Console.WriteLine($"RAN: OUTPUT => {testProject.RunResult.StandardOutput}");

      testProject.RunResult.StandardError.Should().BeEmpty();
      testProject.RunResult.Failed.Should().BeTrue();

      var output = testProject.RunResult.StandardOutput;
      output.Should().Contain("Helo! the TestShouldPass test ran and the result was ?????");
      // output.Should().Contain("Kaboom!");
    }
  }
}