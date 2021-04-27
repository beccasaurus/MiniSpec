# 🐿️ minispec

> Mini Test Framework for .NET

---

_Coming soon..._

_Currently just using this as a fun C# refresher project!_

---

## MiniSpec Syntax

> Note: No `using` statements required.
>
> You don't even need to add a reference to MiniSpec.
>
> This is a complete file (_thanks to C# 9 top-level statements_)

```cs
var dog = new Dog();

void TearDown() => { dog.Delete(); }

bool DogCanBark => dog.Bark() == "Woof!";
```

## Expect Syntax

> Completely Optional.
>
> You can use simple booleans, custom Exceptions,  
> or existing xUnit, NUnit, or FluentAssertions (et al.)

```cs
using static MiniSpec.Expect;

bool DogCanBark() {
  Expect(dog.Bark()).ToEqual("Woof!");
}
```

Read the [tutorial book](https://minispec.io/Introduction) or the [Usage Documentation](https://minispec.io/docs) to learn more!

## MiniSpec Tutorial Book

![Book Cover - MiniSpec - How-to Author a Testing Framework in .NET - by Rebecca Taylor](docs/assets/images/cover.png)