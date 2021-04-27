using System;
using NUnit.Framework;

namespace Specs.xUnit {

  [TestFixture]
  public class InstanceMethodTests : Spec {

    [Test]
    public void TestSomething() {
      var project = CreateProject(csharp: 2, framework: Project.TargetFrameworks.Net50);
      project.WriteFile("MyFile.cs", "public class Testing { int? nullableInt; }");
      project.Build();

      Console.WriteLine($"PROJECT DIR: {project.ProjectDirectory}");
      Console.WriteLine($"STDOUT: {project.BuildResult.StandardOutput}");
      Console.WriteLine($"STDERR: {project.BuildResult.StandardError}");

      Assert.True(project.BuildResult.OK);
    }
  }
}

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