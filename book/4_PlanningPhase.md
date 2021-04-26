# Planning Phase

We've created a working prototype. Now we need to decide what to make next!

## Brainstorm Features

What do we want our wonderful new test framework to provide?

> This is _my personal braindump of ideas_ - come up with your own ideas at home!

#### Command-Line Interface

- `[ ]` `minispec --version` - _Print out the current version of minispec_
- `[ ]` `minispec -l/--list` - _Print out test names instead of running them_
- `[ ]` `minispec -f/--filter [Test Name Matcher]` - _Run a subset of the tests_
- `[ ]` `minispec -v/--verbose` - _Print output from every test, even passing ones_
- `[ ]` `minispec -q/--quiet` - _Don't print anything, exit 0 on success or exit 1 on failure_
- `[ ]` `minispec` should always exit `0` on success or non-zero on failure
- `[ ]` Output should show pretty colors

#### Syntax DSL ([Domain-Specific Language][DSL])

- `[ ]` Support running instance methods
- `[ ]` Support DLLS which need to load dependencies
- `[ ]` Support DLLS which have conflicting dependencies

#### xUnit Test Syntax DSL

- `[ ]` Support failing if a Test method with a bool return type returns `false`
- `[ ]` Detect and run `SetUp` and `TearDown` methods before and after _each run_ of a test case
- `[ ]` Provide an attribute, e.g. `MiniSpec.TestData`, to support [parameterized tests][DDT] (DDT)

#### BDD Test Syntax DSL

- `[ ]` Support defining and running tests via `spec.It`
- `[ ]` Support defining `Before` and `After` actions and run them before _each run_ of a test case
- `[ ]` Provide a way of defining parameterized tests, e.g. `spec.WithInputs`

#### Assertions & Expectations

- `[ ]` Should work fine with `xUnit` assertions
- `[ ]` Should work fine with `NUnit` assertions
- `[ ]` Should work fine with `FluentAssertions`
- `[ ]` `using MiniSpec` - `Expect.That(TheAnswer).Equals(42)`
- `[ ]` `using static MiniSpec.Expect` - `Expect(TheAnswer).ToEqual(42)`
- `[ ]` `using MiniSpec` - `Assert.That(TheAnswer).Equals(42)`
- `[ ]` `using static MiniSpec.Assert` - `AssertEqual(42, TheAnswer)`
- `[ ]` Extensibility so it's easy to add comparisons (to both `Assert` and `Expect`)
- `[ ]` Assertion/Expectation for `.Contains`
- `[ ]` Assertion/Expectation for `.Fails` to assert blocks of code throw Exceptions

#### Distribution

- `[ ]` Make available via [MyGet][MyGet]
- `[ ]` Make available via [NuGet][NuGet]

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