---
permalink: /Choosing_a_Style_to_Implement/MiniSpec_Syntax_Example
title: "MiniSpec Syntax Example"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Choosing_a_Style_to_ImplementMiniSpec_Syntax_Example
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Choosing_a_Style_to_Implement">Choosing a Style to Implement</a></h2>

### MiniSpec Syntax Example

```cs
// Feel free to use Xunit or NUnit assertions with MiniSpec.
// You don't even need to reference MiniSpec from your project.
using Xunit;

class AnimalTests {

  Animal animal;

  // Tests may be class methods (instance or static)
  bool TestDefaultAnimalSound => animal.MakeSound() is null;

  // Or tests may be grouped *within* class methods
  void DogTests() {
    animal = new Dog(); // <-- setup can be performed here

    bool ItBarks => Assert.Equal("Woof!", animal.MakeSound())

    void ItCanSit() {
      Assert.Equal(DogStances.Standing, animal.Stance);
      animal.Sit();
      Assert.Equal(DogStances.Sitting, animal.Stance);
    }
  }

  void CatTests() {
    animal = new Cat();

    bool ItMeows => Assert.Equal("Meow!", animal.MakeSound())
  }
}
```


---

<a class="reading-navigation next" href="/Writing_a_Red_Test" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Writing a Red Test</strong></a><a class="reading-navigation previous" href="/Choosing_a_Style_to_Implement"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Choosing a Style to Implement</strong></a>