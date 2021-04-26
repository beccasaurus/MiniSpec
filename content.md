\frontmatter

# Introduction

\mainmatter

# Defining the API

Before we begin implementation, we need to decide what we want the end result to _look like_.

What will the experience of authoring tests be like for developers?

## Conventional Testing Styles

Developers who have experience authoring tests will likely have used one or more _testing styles_.

There are different schools of thought on what tests should _look like_.

### xUnit, Behavior-Driven Development (BDD), Gherkin

The most common testing _syntax styles_ are: [xUnit][], [Behavior-Driven Development][BDD], and [Gherkin][].

> Note: Behavior-Driven Development is a software _process_, not a code syntax.  
> However, similar _syntax styles_ have emerged over the years for these different testing paradigms.

#### xUnit

xUnit-style syntax typically...

- Uses built-in language constructs for defining "Test Fixtures" (_groups of tests_) and "Tests"
- Provides `setUp` and `tearDown` functions for test setup and cleanup.
- Uses "Assertions" implemented as functions accepting 2 parameters: "Expected" and "Actual"

```cs
class DogTests {
	Dog dog;
	setUp() { dog = new Dog(); }
	testBark() {  
		assertEqual("Woof!", dog.Bark());
	}
}
```

#### Behavior-Driven Development

BDD-style syntax typically...

- Places an emphasis on using natural language, e.g. `describe("Dog").it("can bark!")`
- Provides `before` and `after` functions for test setup and cleanup.
- Uses natural language for "Expectations", e.g. `x.ShouldEqual()` or `Expect(x).toEqual()`

```cs
Dog dog;
describe("Dog", () => {
  before() { dog = new Dog(); }
  it("can bark", () => {
    expect(dog.Bark()).toEqual("Woof!");
  });
});
```

#### Gherkin (aka Cucumber)

From [Wikipedia](https://en.wikipedia.org/wiki/Cucumber_(software)#Gherkin_language):

> "Cucumber is a software tool that supports behavior-driven development (BDD)."
>
> "Gherkin is the language that Cucumber uses to define test cases."

Gherkin is another BDD testing syntax which places an emphasis on using natural language.

```gherkin
Feature: Dog
  Scenario: Barking
    Given a dog
    When the dog barks
    Then the output should be "Woof!"
```

Rather than defining tests in programming code, Gherkin uses a plain text syntax.

Testing libraries for Gherkin allow you to write an interpreter for your Gherkin code.

```cs
[Then("the output should be \"(.*)\"")]
public void ThenTheOutputShouldBe(string value) {
  Output.Should().Equal(value);
}
```

## Choosing a Style to Implement

So, which style(s) should we support with our MiniSpec testing framework project?

You can implement whatever you like! Whatever syntax your heart desires `<3`

In this book, we will be implementing:

- xUnit syntax where each test is represented by a C# method and uses assertions
- BDD syntax where each test is defined using a lambda and uses expectations
- We will embrace the top-level method support in C# 9 *

> \* C# only supports _one file_ with top-level statements. Not very practical for us.  
> But we'll support this syntax for single-file test suites (_simply because it's neat_)

### Why Multiple Syntaxes?

Let's make it flexible so that users can pick and choose! It's fun. Design goals below:

### xUnit Syntax

```cs
void SetUp() { /* do something */ }
void TearDown() { /* do something */ }

void TestSomething() {
  MiniSpec.Assert.Equals(42, Answer);
}
```

### BDD Syntax

```cs
MiniSpec.Describe((spec) => {
  spec.before(() => { /* do something */ });
  spec.after(() => { /* do something */ });

  spec.it("does something", () => {
    MiniSpec.Expect(Answer).ToEqual(42);
  });
});
```

# Test-Driven Test Development

We will test-drive the development of our testing framework (_test-driven test development!_)

As we're using [Test-Driven Development][TDD] (TDD), the first thing we need is a _failing test_!

## Writing a Red Test

We will be using Behavior-Driven Development, so we'll start off by testing some behavior.

### Project Setup

Create an project folder somewhere. This is where you'll be writing the test framework.

```sh
mkdir MiniSpec
cd MiniSpec
```

> Consider making the folder a git repository to save changes as you walk thru this book:
> ```sh 
> git init
> ```

Let's create a test project and write tests _pretending_ that MiniSpec already works:

```sh
dotnet new console -n MyTests
```

> _A new `console` projects? Wait. What? Why in the... what? So: only console projects support_  
> _the new top-level statements in C# 9, so let's define tests in a console project! This will_  
> _be an optional feature and, well, it's just neato and I'd like to try it out! Let's have fun._

This will create a new project folder `MyTests/`. Let's go there and write our first test!

We'll create a file containing 2 xUnit-style tests, one which should fail and the other should pass.

Rename the generated `Program.cs` file to `Tests.cs` and replace its content with the following:

### Example `Tests.cs` File

```cs
void TestShouldPass() {
  // Do nothing
}

void TestShouldFail() {
  throw new System.Exception("Kaboom!");
}
```

That's it. No `using` statements. Just a tiny file with 2 methods. They're not even `public`.

Now, we have two options:

- Write **implementation code** to _run these two tests_ and **print** out the results
- Write **integration test** which _runs these two tests_ and verifies the results are **printed** correctly.

Either approach is valid. We can treat our new `Tests.cs` _as a failing test_, conceptually.

But let's go ahead and setup a real integration test which we can add to during development!

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

\pagebreak

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

\pagebreak

#### Review

So, what's happening here?

- We assume that there will be a `minispec.exe` executable (_or simply `minispec` on Linux_).
- We invoke the `minispec.exe` process passing the DLL with our defined tests as an argument.
- We read STDOUT and STDERR from the process result, i.e. all of the program's console output.
- _STDOUT and STDERR are combined because we don't currently care which the results output to._
- We look for expected messages in the output, e.g. `PASS [testname]` or `FAIL [testname]`


> We're totally making up some of these things as we go along, e.g. the `PASS`/`FAIL` messages.  
> This is how TDD works. We just need to make it fail, then pass, then we can change it later!

## Making it Go Green

Our goal now is to make the test pass.

Is our goal to fully implement the testing framework? **No.**

Using TDD our goal now is _simply_ to do whatever we need to do to make the test pass.

### MiniSpec Project

Back in the root of our project folder, let's create a new project for `minispec.exe`.

Let's make a new `console` project by running this command from the _root project folder_:

```sh
dotnet new console -n MiniSpec
```

#### MiniSpec Solution

While we're here in the root project folder, let's create a Solution to make building simpler.

We'll add all of projects which we've created so far: `MyTests`, `MiniSpec.Specs`, and `MiniSpec`

```sh
dotnet new sln
dotnet sln add MyTests
dotnet sln add MiniSpec.Specs
dotnet sln add MiniSpec
```

If you'd ever like to build all projects at once, now you can run `dotnet build` from this folder.

### `minispec.exe`

Build the new `MiniSpec` console project by running `dotnet build` from the `MiniSpec` folder.

If you look in the generated `bin/Debug/*/` folder, you should now see a `MiniSpec.exe` file.

We'd like to make one _minor correction_ now and rename the generated executable to `minispec.exe`

We can do this by specifying `<AssemblyName>minispec</AssemblyName>` in the `.csproj` file.

Update `MiniSpec.csproj` to the following:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
</Project>
```

Rebuild the project with `dotnet build` and you will see `minispec.exe` in `bin/Debug/*/`

Great! That's the filename we specified in `IntegrationTest.cs`. Let's try running that now!


### Run the Integration Test

Back in the `MiniSpec.Specs` project, add project references for `MiniSpec` and `MyTests`:

```
cd MiniSpec.Specs/
dotnet add reference ../MiniSpec
dotnet add reference ../MyTests
```

Now run the tests with `dotnet test` (_except below_)

```
IntegrationTest.ExpectedSpecsPassAndFail [FAIL]
  Failed IntegrationTest.ExpectedSpecsPassAndFail]
  Error Message:
   Assert.Contains() Failure
Not found: PASS TestShouldPass   <---- What We Expected
In value:  Hello World!          <---- Actual Value
  Stack Trace:
     at IntegrationTest.ExpectedSpecsPassAndFail()
Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1
```

Ah ha! The test looked for `"PASS TestShouldPass"` but found `"Hello World!"`

This is fabulous, it means that `minispec.exe` is running correctly!

Take a look at the generated `Program.cs` in the new `MiniSpec` project:

```cs
using System;

namespace MiniSpec
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```

This is where the `"Hello World!"` value is coming from.

#### Update MiniSpec `Program.cs`

Try updating `MiniSpec/Program.cs` to the following:

```cs
using System;

Console.WriteLine($"Received Args: {string.Join(", ", args)}");
```

> Where's the `Main` method?  
> C# 9 supports top-level statements _used in one file_ to define your main program more easily.

And now, still from `MiniSpec.Specs/`, run `dotnet test` again to see the change:

```
$ dotnet test
...
Not found: PASS TestShouldPass
In value:  Received Args: MyTests.dll
...
```

Wonderful. Ok. Our program runs. It gets a list of DLLs. Now let's run the tests in the DLLs!

### Running Tests in DLLs

Our `minispec.exe` program is currently seeing a list of paths to DLL files.

Our current example DLL test file is defined as an application with 2 top-level methods:

- `TestShouldPass()`
- `TestShouldFail()`

Let's load up the DLL assembly and invoke those methods!

#### Get List of Methods in DLL

First, let's just see the test print out a list of the methods that we're looking for!

Update `MiniSpec/Program.cs` to the following:

```cs
...
```

## Red, Green, Refactor



[xUnit]: https://en.wikipedia.org/wiki/XUnit
[BDD]: https://en.wikipedia.org/wiki/Behavior-driven_development
[Gherkin]: https://en.wikipedia.org/wiki/Cucumber_(software)#Gherkin_language
[TDD]: https://en.wikipedia.org/wiki/Test-driven_development