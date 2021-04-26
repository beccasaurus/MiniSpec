---
permalink: /Writing_a_Red_Test/Integration_Tests
title: "Integration Tests"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Writing_a_Red_TestIntegration_Tests
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Writing_a_Red_Test">Writing a Red Test</a></h2>

### Integration Tests

Back in the root of our project folder, let's create a project using an _existing_ .NET testing framework.

At the time of writing, there are a many choices to choose from: `xUnit`, `NUnit`, `MSTest`, and more.

To make this tutorial easier for most developers out there, let's use the most popular one: `xUnit`

Let's make a new `xUnit` test project now by running this command from the _root project folder_:

```sh
dotnet new xunit -n MiniSpec.Specs
```

This will create a new project folder `MiniSpec.Specs/`. Let's go there and write an integration test!

We'll create a test which:

- Runs `minispec.exe` with the `MyTests.dll` DLL assembly provided as an argument
- Asserts that the output contains text which indicates that `TestShouldPass()` passed
- Asserts that the output contains text which indicates that `TestShouldFail()` failed

> What is `minispec.exe`? It doesn't exist yet, but that's the program we'll make to run tests!



Rename `UnitTest1.cs` to `IntegrationTest.cs` and replace its content with the following:

#### `IntegrationTest.cs`

```cs
using Xunit;

public class IntegrationTest {

    [Fact]
    public void ExpectedSpecsPassAndFail() {
        // Arrange
        var minispecExe = System.IO.File.Exists("minispec.exe") ?
            "minispec.exe" : "minispec"; // No .exe extension on Linux
            
        using var minispec = new System.Diagnostics.Process {
            StartInfo = {
                RedirectStandardOutput = true, // Get the STDOUT
                RedirectStandardError = true,  // Get the STDERR
                FileName = minispecExe,
                Arguments = "MyTests.dll"
            }
        };

        // Act
        minispec.Start();
        minispec.WaitForExit();
        var stdout = minispec.StandardOutput.ReadToEnd();
        var stderr = minispec.StandardError.ReadToEnd();
        var output = $"{stdout}{stderr}";
        minispec.Kill();

        // Assert
        Assert.Contains("PASS TestShouldPass", output);
        Assert.Contains("FAIL TestShouldFail", output);
        Assert.Contains("Kaboom!", output);
    }
}
```



#### Review

So, what's happening here?

- We assume that there will be a `minispec.exe` executable (_or simply `minispec` on Linux_).
- We invoke the `minispec.exe` process passing the DLL with our defined tests as an argument.
- We read STDOUT and STDERR from the process result, i.e. all of the program's console output.
- _STDOUT and STDERR are combined because we don't currently care which the results output to._
- We look for expected messages in the output, e.g. `PASS [testname]` or `FAIL [testname]`


> We're totally making up some of these things as we go along, e.g. the `PASS`/`FAIL` messages.  
> This is how TDD works. We just need to make it fail, then pass, then we can change it later!


---

<a class="reading-navigation next" href="/Making_it_Go_Green" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Making it Go Green</strong></a><a class="reading-navigation previous" href="/Writing_a_Red_Test/Example_Tests_cs_File"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Example `Tests.cs` File</strong></a>