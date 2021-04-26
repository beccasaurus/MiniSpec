---
permalink: /Writing_a_Red_Test/Example_Tests_cs_File
title: "Example `Tests.cs` File"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Writing_a_Red_TestExample_Tests_cs_File
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Writing_a_Red_Test">Writing a Red Test</a></h2>

### Example `Tests.cs` File

```cs
void TestShouldPass() {
  // Do nothing
}

void TestShouldFail() {
  throw new System.Exception("Kaboom!");
}
```

That's it. No `using` statements. Just a tiny file with 2 methods. They're not even `public`.

Now, we have two options:

- Write **implementation code** to _run these two tests_ and **print** out the results
- Write **integration test** which _runs these two tests_ and verifies the results are **printed** correctly.

Either approach is valid. We can treat our new `Tests.cs` _as a failing test_, conceptually.

But let's go ahead and setup a real integration test which we can add to during development!


---

<a class="reading-navigation next" href="/Writing_a_Red_Test/Integration_Tests" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Integration Tests</strong></a><a class="reading-navigation previous" href="/Writing_a_Red_Test/Project_Setup"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Project Setup</strong></a>