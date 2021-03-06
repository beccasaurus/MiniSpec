# Test-Driven Test Development

We will test-drive the development of our testing framework (_test-driven test development!_)

As we're using [Test-Driven Development][TDD] (TDD), the first thing we need is a _failing test_!

[TDD]: https://en.wikipedia.org/wiki/Test-driven_development

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
                RedirectStandardOutput = true, // Get the StandardOutput
                RedirectStandardError = true,  // Get the StandardError
                FileName = minispecExe,
                Arguments = "MyTests.dll"
            }
        };

        // Act
        minispec.Start();
        minispec.WaitForExit();
        var StandardOutput = minispec.StandardOutput.ReadToEnd();
        var StandardError = minispec.StandardError.ReadToEnd();
        var output = $"{StandardOutput}{StandardError}";
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
- We read StandardOutput and StandardError from the process result, i.e. all of the program's console output.
- _StandardOutput and StandardError are combined because we don't currently care which the results output to._
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

Now run the tests with `dotnet test` (_excerpt below_)

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

### Discovering Tests in DLLs

Our `minispec.exe` program is currently seeing a list of paths to DLL files.

Let's load the provided DLLs and find our defined test methods inside of them!

#### Get List of Methods in DLL

First, let's update the test to _print out a list of methods_ from the provided DLL.

Update `MiniSpec/Program.cs` to the following:

```cs
using System;
using System.Reflection;
using System.Runtime.Loader;

foreach (var dll in args) {
    Console.WriteLine($"Loading {dll}");
    var dllPath = System.IO.Path.GetFullPath(dll);
    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);

    foreach (var type in assembly.GetTypes()) {
        Console.WriteLine($"Found type: {type}");
        foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            Console.WriteLine($"Instance Method: {method.Name}");
        foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static))
            Console.WriteLine($"Static Method: {method.Name}");
    }
}
```

##### Review

- Load any argument as a .NET DLL assembly
- Loop over every defined type in the assembly (_`args` is available to top-level statements_)
- Loop over every instance method on the type (_and print out the method name_)
- Loop over every static method on the type (_and print out the method name_)

\pagebreak

Run the tests again with `dotnet test` (_excerpt below_)

```
Not found: PASS TestShouldPass
In value:  Loading MyTests.dll
Found type: <Program>$
Instance Method: MemberwiseClone
Instance Method: Finalize
Static Method: <Main>$
Static Method: <<Main>$>g__TestShouldPass|0_0
Static Method: <<Main>$>g__TestShouldFail|0_1
```

The test is still failing (_"Not found: PASS TestShouldPass"_) but we can see new output, which is good!

Even though we did not _explicitly_ define it, C# 9 added a `<Program>` class for us.

As you would expect from a console application, this class has a static `<Main>` method.

And it looks like we found the test methods which we defined as top-level statements too!

> Huh. `<<Main>$>g__TestShouldPass|0_0`. I guess _that's_ how local methods are represented.

### Running Tests in DLLs

What now? Well, remember our goal? _"do whatever we need to do to make the test pass"_

Let's be naive and simply run every static method we find with `Test` in the name.

Update `MiniSpec/Program.cs` to the following:

```cs
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

foreach (var dll in args) {
    var dllPath = System.IO.Path.GetFullPath(dll);
    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
    foreach (var type in assembly.GetTypes()) {
        var testMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(m => m.Name.Contains("Test"));
        foreach (var method in testMethods) {
            try {
                method.Invoke(null, null);
                Console.WriteLine($"PASS {method.Name}");
            } catch (Exception e) {
                Console.WriteLine($"FAIL {method.Name}");
                Console.WriteLine($"ERROR {e.Message}");
            }
        }
    }
}
```

Run the tests again with `dotnet test` (_excerpt below_)

```
Not found: PASS TestShouldPass
In value:  PASS <<Main>$>g__TestShouldPass|0_0
FAIL <<Main>$>g__TestShouldFail|0_1
ERROR Exception has been thrown by the target of an invocation.
```

Yikes, we tried but a few things are incorrect which we need to fix.

- Name of the test is showing up as `<<Main>$>g__TestShouldPass|0_0`
- ^--- this should be: `TestShouldPass`
- Exception message only says _Exception has been thrown by the target of an invocation_
- ^--- this should be `Kaboom!`

#### Fix `Program.cs`

Update `MiniSpec/Program.cs` to the following:

```cs
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

foreach (var dll in args) {
    var dllPath = System.IO.Path.GetFullPath(dll);
    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
    foreach (var type in assembly.GetTypes()) {
        var testMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(m => m.Name.Contains("Test"));
        foreach (var method in testMethods) {
            var displayName = method.Name;
            if (Regex.IsMatch(displayName, @"[^\w]"))
                displayName =
                    Regex.Match(displayName, @"Test([\w]+)").Value;
            try {
                method.Invoke(null, null);
                Console.WriteLine($"PASS {displayName}");
            } catch (Exception e) {
                Console.WriteLine($"FAIL {displayName}");
                Console.WriteLine($"ERROR {e.InnerException.Message}");
            }
        }
    }
}
```

Run the tests again with `dotnet test` (_excerpt below_)

```
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1
```

*Phew!* We did it! Green, passing tests! Goodness gracious! Hooray!

Try it yourself!

```
bin/Debug/*/minispec.exe bin/Debug/*/MyTests.dll
PASS TestShouldPass
FAIL TestShouldFail
ERROR Kaboom!
```

> On Linux: `./bin/Debug/*/minispec bin/Debug/*/MyTests.dll`

## Red, Green, Refactor

If you wrote code different from what we have at home, _now is the time to Refactor!_

As the author, I am doing BDD (Book-Driven Development) and refactoring as I go.

At home, _it is really important not to forget the Refactor step!_

In the next section, we'll come up with a list of features to implement and walk thru them.
