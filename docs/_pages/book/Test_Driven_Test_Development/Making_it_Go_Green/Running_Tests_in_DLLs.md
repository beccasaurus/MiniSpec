---
permalink: /Making_it_Go_Green/Running_Tests_in_DLLs
title: "Running Tests in DLLs"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Making_it_Go_GreenRunning_Tests_in_DLLs
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Making_it_Go_Green">Making it Go Green</a></h2>

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


---

<a class="reading-navigation next" href="/Red_Green_Refactor" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Red, Green, Refactor</strong></a><a class="reading-navigation previous" href="/Making_it_Go_Green/Discovering_Tests_in_DLLs"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Discovering Tests in DLLs</strong></a>