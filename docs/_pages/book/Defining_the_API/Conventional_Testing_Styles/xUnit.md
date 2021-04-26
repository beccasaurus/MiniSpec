---
permalink: /Conventional_Testing_Styles/xUnit
title: "xUnit"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Conventional_Testing_StylesxUnit
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Conventional_Testing_Styles">Conventional Testing Styles</a></h2>

### xUnit

xUnit-style syntax typically...

- Uses built-in language constructs for defining "Test Fixtures" (_groups of tests_) and "Tests"
- Provides `setUp` and `tearDown` functions for test setup and cleanup.
- Uses "Assertions" implemented as functions accepting 2 parameters: "Expected" and "Actual"

```cs
class DogTests {
	Dog dog;
	SetUp() { dog = new Dog(); }
	TestBark() {  
		AssertEqual("Woof!", dog.Bark());
	}
}
```


---

<a class="reading-navigation next" href="/Conventional_Testing_Styles/Behavior_Driven_Development" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Behavior-Driven Development</strong></a><a class="reading-navigation previous" href="/Conventional_Testing_Styles"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Conventional Testing Styles</strong></a>