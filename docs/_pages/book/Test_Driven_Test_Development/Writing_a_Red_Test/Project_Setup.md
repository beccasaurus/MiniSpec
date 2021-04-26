---
permalink: /Writing_a_Red_Test/Project_Setup
title: "Project Setup"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Writing_a_Red_TestProject_Setup
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Writing_a_Red_Test">Writing a Red Test</a></h2>

### Project Setup

Create an project folder somewhere. This is where you'll be writing the test framework.

```sh
mkdir MiniSpec
cd MiniSpec
```

> Consider making the folder a git repository to save changes as you walk thru this book:
> ```sh 
> git init
> ```

Let's create a test project and write tests _pretending_ that MiniSpec already works:

```sh
dotnet new console -n MyTests
```

> _A new `console` projects? Wait. What? Why in the... what? So: only console projects support_  
> _the new top-level statements in C# 9, so let's define tests in a console project! This will_  
> _be an optional feature and, well, it's just neato and I'd like to try it out! Let's have fun._

This will create a new project folder `MyTests/`. Let's go there and write our first test!

We'll create a file containing 2 xUnit-style tests, one which should fail and the other should pass.

Rename the generated `Program.cs` file to `Tests.cs` and replace its content with the following:


---

<a class="reading-navigation next" href="/Writing_a_Red_Test/Example_Tests_cs_File" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Example `Tests.cs` File</strong></a><a class="reading-navigation previous" href="/Writing_a_Red_Test"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Writing a Red Test</strong></a>