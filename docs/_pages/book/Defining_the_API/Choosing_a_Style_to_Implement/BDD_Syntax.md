---
permalink: /Choosing_a_Style_to_Implement/BDD_Syntax
title: "BDD Syntax"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Choosing_a_Style_to_ImplementBDD_Syntax
---

<h1><a href="/Defining_the_API">Defining the API</a></h1>

<h2><a href="/Choosing_a_Style_to_Implement">Choosing a Style to Implement</a></h2>

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


---

<a class="reading-navigation next" href="/Writing_a_Red_Test" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Writing a Red Test</strong></a><a class="reading-navigation previous" href="/Choosing_a_Style_to_Implement/xUnit_Syntax"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;xUnit Syntax</strong></a>