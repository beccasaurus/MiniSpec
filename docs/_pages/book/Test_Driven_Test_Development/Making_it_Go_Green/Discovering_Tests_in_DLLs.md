---
permalink: /Making_it_Go_Green/Discovering_Tests_in_DLLs
title: "Discovering Tests in DLLs"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Making_it_Go_GreenDiscovering_Tests_in_DLLs
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Making_it_Go_Green">Making it Go Green</a></h2>

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


---

<a class="reading-navigation next" href="/Making_it_Go_Green/Running_Tests_in_DLLs" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Running Tests in DLLs</strong></a><a class="reading-navigation previous" href="/Making_it_Go_Green/Run_the_Integration_Test"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Run the Integration Test</strong></a>