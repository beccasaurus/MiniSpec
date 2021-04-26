---
permalink: /Making_it_Go_Green/minispec_exe
title: "`minispec.exe`"
layout: singleWithoutTitle
author_profile: true
sidebar:
  nav: Making_it_Go_Greenminispec_exe
---

<h1><a href="/Test_Driven_Test_Development">Test-Driven Test Development</a></h1>

<h2><a href="/Making_it_Go_Green">Making it Go Green</a></h2>

### `minispec.exe`

Build the new `MiniSpec` console project by running `dotnet build` from the `MiniSpec` folder.

If you look in the generated `bin/Debug/*/` folder, you should now see a `MiniSpec.exe` file.

We'd like to make one _minor correction_ now and rename the generated executable to `minispec.exe`

We can do this by specifying `<AssemblyName>minispec</AssemblyName>` in the `.csproj` file.

Update `MiniSpec.csproj` to the following:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
</Project>
```

Rebuild the project with `dotnet build` and you will see `minispec.exe` in `bin/Debug/*/`

Great! That's the filename we specified in `IntegrationTest.cs`. Let's try running that now!


---

<a class="reading-navigation next" href="/Making_it_Go_Green/Run_the_Integration_Test" style="float: right;"><i class="fas fa-arrow-alt-circle-right"></i><strong> &nbsp;Run the Integration Test</strong></a><a class="reading-navigation previous" href="/Making_it_Go_Green/MiniSpec_Project"><i class="fas fa-arrow-alt-circle-left"></i><strong> &nbsp;MiniSpec Project</strong></a>