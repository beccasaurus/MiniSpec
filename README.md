[![.NET 5 Tests](https://github.com/beccasaurus/minispec/workflows/dotnet%20test/badge.svg)](https://github.com/beccasaurus/minispec/actions?query=workflow%3A%22dotnet+test%22)

# 🐿️ MiniSpec

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

**What if .NET tests were much simpler? No `[Test]` attributes. No `Assert`.**

```cs
string greeting = "Hello";

bool TestShouldPass() => greeting == "Hello";
bool TestShouldFail() => greeting == "Hey there";

MiniSpec.Tests.Run(args);
```

> _This is the **entire code** for a valid MiniSpec project_ ⤴

## Modern

**What if one test framework provided both xUnit and BDD style tests?**

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

**What if one framework was really tiny and focused on just the testing syntax?**

No `Assert` provided.

Use `Assert` from `xUnit` or `NUnit`. Or `FluentAssertions`. Or `NExpect`. It all works.

```cs
using NUnit.Framework;

void TestTheAnswer() {
  Assert.That(TheAnswer, Is.EqualTo(42));
}
```

> _`MiniSpec` has a very small interface, see [syntax overview](#syntax) below!_

## Powerful

**What if really simple Dependency Injection was included?**

```cs
TODO
```

## Conventional

**What if conventional terms like `SetUp` and `TearDown` just worked?**

```cs
class TestDog {
  void Setup()     { /* runs before each test */ }
  void Teardown()  { /* runs after each test  */ }
  void ItCanBark() { /* test code */ }
  void ItCanSit()  { /* test code */ }
}
```

## Flexible

**What if you could configure a test framework with _your own preferred syntax?_**

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

**What if the test output was pretty, colorful, short, and easy to read?**

(_screenshot_)

> Also supports TAP out-of-the-box. Just run `dotnet run --tap`
>
> For JUnit XML: `dotnet package add MiniSpec.JUnit` and `dotnet run --junit`

## Supported

**What if a test framework worked on _all versions of .NET_, going back to .NET 2.0?**

> _Packages available for:_
> - .NET 5
> - .NET Core 1.0 - 3.1
> - .NET Standard 1.0 - 2.1
> - .NET Framework 2.0 - 4.8

## Extensible

**What if you could easily configure your own output?**

_You can easily write your own output reporter._

_You don't even need a `MiniSpec` dependency!_

> _Also supports:_
> - Custom test discovery
> - Custom executor for tests
> - Custom executor for the whole suite

## Documented

**What if all the documentation you needed was simply in the README?**

_The entire syntax is documented here in this README!_

> _You can also head over to [https://minispec.io](https://minispec.io) to see it on a pretty website._

## Fun

Written with developer fun and friendliness in mind. Have fun with it!

## Why?

I recently returned to `C#` from many years away and wasn't enjoying the existing `dotnet test` developer experience.

I wanted to scrape the rust off of my `C#` skills by making something and... apparently this is what I ended up with!

# Documentation

_Coming soon! Still authoring. Having fun with it_ 😄