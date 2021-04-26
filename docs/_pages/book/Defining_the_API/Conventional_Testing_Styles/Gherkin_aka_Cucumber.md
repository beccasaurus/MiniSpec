---
permalink: /Conventional_Testing_Styles/Gherkin_aka_Cucumber
title: "Gherkin (aka Cucumber)"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Conventional_Testing_StylesGherkin_aka_Cucumber
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Conventional_Testing_Styles">Conventional Testing Styles</a></h2>

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


---

<a class="reading-navigation next" href="/Choosing_a_Style_to_Implement" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Choosing a Style to Implement</strong></a><a class="reading-navigation previous" href="/Conventional_Testing_Styles/Behavior_Driven_Development"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Behavior-Driven Development</strong></a>