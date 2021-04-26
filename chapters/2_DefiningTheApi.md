# Defining the API

Before we begin implementation, we need to decide what we want the end result to _look like_.

What will the experience of authoring tests be like for developers?

## Conventional Testing Styles

Developers who have experience authoring tests will likely have used one or more _testing styles_.

There are different schools of thought on what tests should _look like_.

#### xUnit, Behavior-Driven Development (BDD), Gherkin

The most common testing _syntax styles_ are: [xUnit][], [Behavior-Driven Development][BDD], and [Gherkin][].

[xUnit]: https://en.wikipedia.org/wiki/XUnit
[BDD]: https://en.wikipedia.org/wiki/Behavior-driven_development
[Gherkin]: https://en.wikipedia.org/wiki/Cucumber_(software)#Gherkin_language

> Note: Behavior-Driven Development is a software _process_, not a code syntax.  
> However, similar _syntax styles_ have emerged over the years for these different testing paradigms.

### xUnit

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

### Behavior-Driven Development

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

### Gherkin (aka Cucumber)

From [Wikipedia](https://en.wikipedia.org/wiki/Cucumber_(software)#Gherkin_language):

> "Cucumber is a software tool that supports behavior-driven development (BDD)."
>
> "Gherkin is the language that Cucumber uses to define test cases."

Gherkin is another BDD testing syntax which places an emphasis on using natural language.

Rather than defining tests in programming code, Gherkin uses a plain text syntax:

```gherkin
Feature: Dog
  Scenario: Barking
    Given a dog
    When the dog barks
    Then the output should be "Woof!"
```

Testing libraries for Gherkin allow you to write an interpreter for your Gherkin code:

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
- We will embrace the [top-level statement support][TLS] in C# 9 ( _just for fun!_ )

[TLS]: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#top-level-statements

#### Why Multiple Syntaxes?

Let's make it flexible so that users can pick and choose! It's fun. Design goals below:

### xUnit Syntax

```cs
using static MiniSpec.Assert;

void SetUp() { /* do something */ }
void TearDown() { /* do something */ }
void TestSomething() {
  AssertEquals(42, TheAnswer);
}
bool TestAnotherThing => 1 == 2;
```

### BDD Syntax

```cs
using static MiniSpec.Expect;

MiniSpec.Describe((spec) => {
  spec.Before(() => { /* do something */ });
  spec.After(() => { /* do something */ });
  spec.It("does something", () => {
    Expect(TheAnswer).ToEqual(42);
  });
});
```
