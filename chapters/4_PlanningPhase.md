# Planning Phase

We've created a working prototype. Now we need to decide what to make next!

## Brainstorm Features

What do we want our wonderful new test framework to provide?

> This is _my personal braindump of ideas_ - come up with your own ideas at home!

#### Command-Line Interface

- `[ ]` Output should show pretty colors
- `[ ]` `minispec` should always exit `0` on success or non-zero on failure
- `[ ]` `minispec --version` - _Print out the current version of minispec_
- `[ ]` `minispec -l/--list` - _Print out test names instead of running them_
- `[ ]` `minispec -f/--filter [Test Name Matcher]` - _Run a subset of the tests_
- `[ ]` `minispec -v/--verbose` - _Print output from every test, even passing ones_
- `[ ]` `minispec -q/--quiet` - _Don't print anything, exit 0 on success or exit 1 on failure_
- `[ ]` `minispec -n/--no-local` - _Don't consider local functions when searching for tests_
- `[ ]` `minispec -p/--pattern` - _Provide a custom pattern used to find test methods_
- `[ ]` `minispec -s/--setup` - _Provide a custom pattern used to find setup methods_
- `[ ]` `minispec -t/--teardown` - _Provide a custom pattern used to find teardown methods_

#### Syntax DSL ([Domain-Specific Language][DSL])

- `[ ]` Support DLLS which need to load dependencies
- `[ ]` Support DLLS which have conflicting dependencies
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

## Choose Feature to Implement

Looking at the list, as it is now, it looks pretty daunting.

For the next parts of this book, you'll be able to hop around and implement
whichever set of these features that you'd like to
(_although some may depend on completing other sections first_).

My recommendation to you is to start by choosing one of these options:

- Something which will make you **_happy_**
- Something which is **_easy_** to get done
- Something which provides the most **_value_**

Make _sure_ that you _test-drive_ (_and don't forget the Refactor step!_).

**Have fun!**