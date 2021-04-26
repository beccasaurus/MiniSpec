---
permalink: /Conventional_Testing_Styles/Behavior_Driven_Development
title: "Behavior-Driven Development"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Conventional_Testing_StylesBehavior_Driven_Development
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Conventional_Testing_Styles">Conventional Testing Styles</a></h2>

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


---

<a class="reading-navigation next" href="/Conventional_Testing_Styles/Gherkin_aka_Cucumber" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Gherkin (aka Cucumber)</strong></a><a class="reading-navigation previous" href="/Conventional_Testing_Styles/xUnit"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;xUnit</strong></a>