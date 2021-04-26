# Planning Phase

We've created a working prototype. Now we need to decide what to make next!

If you'd like, you can keep writing failing tests and then making them green.

But we're going to pause to plan a bit here.

## Features to Implement

What do we want our wonderful new test framework to provide?

> This is _my personal braindump of ideas_ - come up with your own ideas at home!

#### Command-Line Interface

- `[ ]` `minispec --version` - _Print out the current version of minispec_
- `[ ]` `minispec -l/--list` - _Print out test names instead of running them_
- `[ ]` `minispec -f/--filter [Test Name Matcher]` - _Run a subset of the tests_
- `[ ]` `minispec -v/--verbose` - _Print output from every test, even passing ones_
- `[ ]` `minispec -q/--quiet` - _Don't print anything, exit 0 on success or exit 1 on failure_
- `[ ]` `minispec` should always exit `0` on success or non-zero on failure

#### Syntax DSL ([Domain-Specific Language][DSL])

- `[ ]` Support running instance methods

#### xUnit Test Syntax DSL

- `[ ]` Detect and run `SetUp` methods before _each run_ of a test case
- `[ ]` Detect and run `TearDown` methods after _each run_ of a test case (_even if it fails_)
- `[ ]` Provide an attribute, e.g. `MiniSpec.TestData`, to support [parameterized tests][DDT] (DDT)

#### BDD Test Syntax DSL

- `[ ]` Support defining and running tests via `spec.It`
- `[ ]` Support defining `spec.Before` action and run it before _each run_ of a test case
- `[ ]` Support defining `spec.After` action and run it after _each run_ of a test case
- `[ ]` Provide a way of defining parameterized tests, e.g. `spec.WithInputs`

#### Assertions & Expectations

- `[ ]` 
- `[ ]` 
- `[ ]` 

## Choose Your Own Adventure
