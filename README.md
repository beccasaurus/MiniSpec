[![.NET 5 Tests](https://github.com/beccasaurus/minispec/workflows/dotnet%20test/badge.svg)](https://github.com/beccasaurus/minispec/actions?query=workflow%3A%22dotnet+test%22)

# 🐿️ minispec

> Mini Test Framework for .NET

---

ℹ️ _Coming soon to a package manager near you..._

---

`MiniSpec` is:

- [Simple](#simple)
- [Modern](#modern)
- [Small](#small)
- [Powerful](#powerful)
- [Conventional](#conventional)
- [Flexible](#flexible)
- [Pretty](#pretty)
- [Supported](#supported)
- [Extensible](#extensible)
- [Documented](#documented)
- [Fun](#fun)

Read the [documentation](#documentation) below for syntax, usage, instructions, etc.

## Simple

What if .NET tests could be simpler? No `[Test]` attributes. No `Assert`.

```cs
string greeting = "Hello";

bool TestShouldPass() => greeting == "Hello";
bool TestShouldFail() => greeting == "Hey there";

MiniSpec.Tests.Run(args);
```

> _This is the **entire code** for a valid MiniSpec project_ ⤴

## Modern

What if one test framework provided both xUnit and BDD style tests?

```cs
using MiniSpec;

class DogTests {
  void TestBark() { /* ... */ }
}

Spec.Describe("Dog", dog => {
  dog.Can("Bark", () => { /* ... */ })
});
```

## Small

No `Assert` provided. _Feel free to bring your own!_

Use `Assert` from `xUnit` or `NUnit`. Or `FluentAssertions`. Or `NExpect`. It all works.

```cs
using NUnit.Framework;

void TestTheAnswer() {
  Assert.That(TheAnswer, Is.EqualTo(42));
}
```

> The _entire_ public interface of `MiniSpec` is just 3 methods:
> - `MiniSpec.Tests.Run(string[] args)`
> - `MiniSpec.Spec.Describe(string description, Func body)`
> - `MiniSpec...` `TODO` `Add method for registering dependency injection`
> 
> There. You just learned the whole API. Congratulations.

## Powerful

You want Dependency Injection? Have dependency injection.

```cs
TODO
```

## Conventional

What if conventional terms like `SetUp` and `TearDown` just worked?

```cs
class TestDog {
  void Setup()     { /* runs before each test */ }
  void Teardown()  { /* runs after each test  */ }
  void ItCanBark() { /* test code */ }
  void ItCanSit()  { /* test code */ }
}
```

## Flexible

Make it your own and configure your own personal syntax.

```cs
class Specification {
  void Prepare()    { /* setup code */ }
  void Cleanup()    { /* teardown code */ }
  void ExampleOne() { /* test code */ }
  void ExampleTwo() { /* test code */ }
}
```

```
$ dotnet test --before Prepare --after Cleanup --test Example
```

## Pretty

Outputs nice pretty colors with no annoying clutter by default.

(_screenshot_)

Also supports TAP out-of-the-box. Just pass `--tap`.

## Supported

Works on _every current version of .NET_ from .NET 2.0 and beyond.

> _Tested on .NET Framework 2.0-4.8, .NET 5, Core 1.0-3.1, and Standard 1.0-2.1_

## Extensible

Easily write your own output reporter. You don't even need a `MiniSpec` dependency.

Also supports custom test discovery and execution extensions out-of-the-box.

## Documented

Hang out here in the README or head over to [https://minispec.io](https://minispec.io) for documentation.

## Fun

Written with developer fun and friendliness in mind. Have fun with it!

## Why?

I recently returned to `C#` from many years away and wasn't enjoying the existing `dotnet test` developer experience.

I wanted to scrape the rust off of my `C#` skills by making something and... apparently this is what I ended up with!

# Documentation

_Coming soon! Still authoring. Having fun with it_ 😄