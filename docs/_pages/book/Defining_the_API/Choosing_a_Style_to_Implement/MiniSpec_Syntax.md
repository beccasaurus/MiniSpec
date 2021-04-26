---
permalink: /Choosing_a_Style_to_Implement/MiniSpec_Syntax
title: "MiniSpec Syntax"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Choosing_a_Style_to_ImplementMiniSpec_Syntax
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Choosing_a_Style_to_Implement">Choosing a Style to Implement</a></h2>

### MiniSpec Syntax

```cs
// Simple tests may simply return a Boolean:
bool TestAnotherThing => 1 == 2;

// Developers may optionally include our Expect() method.
using static MiniSpec.Expect;

// Expect() can be used with simple one-line tests:
bool TestMoreThings => Expect(Foo).ToEqual("Bar");

// Or define full methods (Note: using a class is optional)
void MyTest() {
  Expect(TheAnswer).ToEqual(42);
}

// Support for setup and teardown functionality
void SetUp() { /* do something */ }
void TearDown() { /* do something */ }

// Tests may also be grouped within a class
class MyTests {
  bool PassingTest => true;

  // Or even grouped within a method
  static void Group() {
    bool LocalTestFunction() => Expect("This Syntax").To.Work.OK;
  }
}
```


---

<a class="reading-navigation next" href="/Writing_a_Red_Test" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Writing a Red Test</strong></a><a class="reading-navigation previous" href="/Choosing_a_Style_to_Implement"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Choosing a Style to Implement</strong></a>