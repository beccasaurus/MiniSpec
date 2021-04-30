using System;
using FluentAssertions;
using NUnit.Framework;

namespace Specs.xUnit {

  [TestFixture]
  public class InstanceMethodTests : Spec {

    [Test]
    public void TestSomething() {
      var project = CreateProject(framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      project.WriteFile("Program.cs", @"MiniSpec.Tests.Run(System.Console.Out, System.Console.Error);");
      project.Run();

      project.RunResult.OK.Should().Equals(true);
      project.RunResult.StandardOutput.Should().Contain("Hello, world!"); // <--- this means it ran MiniSpec.Tests.Run OK (for now)
    }
  }
}


      // var project = CreateProject(csharp: 2, framework: Project.TargetFrameworks.Net50, type: Project.OutputTypes.Exe);
      // project.WriteFile("MyFile.cs", "public class Testing { int? nullableInt; }");

//   public class InstanceMethods {
//     public class PublicClass {
//       [TestFixture]
//       public class PublicMethod {
//         // Longhand:
//         [Test]
//         public void BoolReturnType() {
//           Assert.That("Foo", Is.EqualTo("bar"));

//           Assert.

//           // CreateFile("MyTests.cs");

//           // RunTestCode(@"public class MyTests { public bool TestPass() { return true; } public bool TestFail() { return false; }}");
//           // AssertOnlyTestNames("TestPass", "TestFail");
//           // AssertPassed("TestPass");
//           // AssertFailed("TestFail");
//         }

//         // Shorthand:
//         //
//         // [Fact]
//         // public void BoolReturnType() {
//         //   RunTestCode(@"public class MyTests { public bool TestPass() { return true; } public bool TestFail() { return false; }}");
//         //   AssertOnlyTestNames("TestPass", "TestFail");
//         //   AssertPassed("TestPass");
//         //   AssertFailed("TestFail");
//         // }
//       }
//     }
//   }
// }