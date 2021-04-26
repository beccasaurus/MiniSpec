---
permalink: /Making_it_Go_Green/Run_the_Integration_Test
title: "Run the Integration Test"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Making_it_Go_GreenRun_the_Integration_Test
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Making_it_Go_Green">Making it Go Green</a></h2>

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


---

<a class="reading-navigation next" href="/Making_it_Go_Green/Discovering_Tests_in_DLLs" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Discovering Tests in DLLs</strong></a><a class="reading-navigation previous" href="/Making_it_Go_Green/minispec_exe"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;`minispec.exe`</strong></a>