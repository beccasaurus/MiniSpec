---
permalink: /Choosing_a_Style_to_Implement/xUnit_Syntax
title: "xUnit Syntax"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Choosing_a_Style_to_ImplementxUnit_Syntax
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Choosing_a_Style_to_Implement">Choosing a Style to Implement</a></h2>

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


---

<a class="reading-navigation next" href="/Choosing_a_Style_to_Implement/BDD_Syntax" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;BDD Syntax</strong></a><a class="reading-navigation previous" href="/Choosing_a_Style_to_Implement"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Choosing a Style to Implement</strong></a>