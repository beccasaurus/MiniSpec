---
permalink: /Making_it_Go_Green/MiniSpec_Project
title: "MiniSpec Project"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Making_it_Go_GreenMiniSpec_Project
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Making_it_Go_Green">Making it Go Green</a></h2>

### MiniSpec Project

Back in the root of our project folder, let's create a new project for `minispec.exe`.

Let's make a new `console` project by running this command from the _root project folder_:

```sh
dotnet new console -n MiniSpec
```

#### MiniSpec Solution

While we're here in the root project folder, let's create a Solution to make building simpler.

We'll add all of projects which we've created so far: `MyTests`, `MiniSpec.Specs`, and `MiniSpec`

```sh
dotnet new sln
dotnet sln add MyTests
dotnet sln add MiniSpec.Specs
dotnet sln add MiniSpec
```

If you'd ever like to build all projects at once, now you can run `dotnet build` from this folder.


---

<a class="reading-navigation next" href="/Making_it_Go_Green/minispec_exe" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;`minispec.exe`</strong></a><a class="reading-navigation previous" href="/Making_it_Go_Green"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;Making it Go Green</strong></a>