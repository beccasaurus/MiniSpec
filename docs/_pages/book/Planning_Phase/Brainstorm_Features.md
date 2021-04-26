---
permalink: /Brainstorm_Features
title: "Brainstorm Features"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Brainstorm_Features
---
<h1><a href="/Planning_Phase">Planning Phase</a></h1>

## Brainstorm Features

What do we want our wonderful new test framework to provide?

> This is _my personal braindump of ideas_ - come up with your own ideas at home!

#### Command-Line Interface

- `[ ]` Output should show pretty colors
- `[ ]` `minispec` should always exit `0` on success or non-zero on failure
- `[ ]` `minispec --version` - _Print out the current version of minispec_
- `[ ]` `minispec -l/--list` - _Print out test names instead of running them_
- `[ ]` `minispec -m/--match [Test Name Matcher]` - _Run a subset of the tests_
- `[ ]` `minispec -v/--verbose` - _Print output from every test, even passing ones_
- `[ ]` `minispec -q/--quiet` - _Don't print anything, exit 0 on success or exit 1 on failure_
- `[ ]` `minispec -n/--no-local` - _Don't consider local functions when searching for tests_
- `[ ]` `minispec -p/--pattern` - _Provide a custom pattern used to find test methods_
- `[ ]` `minispec -s/--setup` - _Provide a custom pattern used to find setup methods_
- `[ ]` `minispec -t/--teardown` - _Provide a custom pattern used to find teardown methods_
- `[ ]` `minispec -f/--formatter` - _Name of output reporter formatter to use, e.g. TAP_

#### Syntax DSL ([Domain-Specific Language][DSL])

- `[ ]` Support DLLS which need to load dependencies, including if there are conflicts
- `[ ]` Support failing if a Test method with a bool return type returns `false`
- `[ ]` Support running instance methods
- `[ ]` Invoke parent method(s) before invoking test function (_if local function_)
- `[ ]` Allow for some local functions within a test function _not_ to be run (_use `_` prefix_)
- `[ ]` Detect and run `SetUp` and `TearDown` methods before and after _each run_ of a test case
- `[ ]` Determine and implement a nice way of supporting [parameterized tests][DDT] (DDT)

[DSL]: https://en.wikipedia.org/wiki/Domain-specific_language
[DDT]: https://en.wikipedia.org/wiki/Data-driven_testing

#### Assertions & Expectations

- `[ ]` Should work fine with `xUnit` assertions
- `[ ]` Should work fine with `NUnit` assertions
- `[ ]` Should work fine with `FluentAssertions`
- `[ ]` Extensibility so it's easy to add your own `Expect()` assertions
- `[ ]` `Expect().ToEqual`
- `[ ]` `Expect().ToContain`
- `[ ]` `Expect().ToMatch`
- `[ ]` `Expect(() => { ... }).ToFail("Kaboom!")`

#### Distribution

- `[ ]` `Expect()` should be available on its own via `MiniSpec.Expect`
- `[ ]` `minispec.exe` should be available on its own via `MiniSpec.Console`
- `[ ]` `MiniSpec` package should install both the library and the executable
- `[ ]` Make available via [GitHub Packages](https://github.com/features/packages)
- `[ ]` Make available via [MyGet][MyGet]
- `[ ]` Make available via [NuGet][NuGet]

[MyGet]: https://www.myget.org
[NuGet]: https://www.nuget.org

---

<a class="reading-navigation next" href="/Choose_Your_Own_Adventure" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Choose Your Own Adventure</strong></a><a class="reading-navigation previous" href="/Planning_Phase"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Planning Phase</strong></a>