---
permalink: /
title: ""
layout: singleWithoutTitle
author_profile: true
show_links: true
# sidebar:
#   nav: Introduction
show_download_button: true
always_show_sidebar: true
class: home
---


# MiniSpec

_Mini Testing Framework for .NET_

---

<i class="fad fa-terminal"></i> `dotnet add package MiniSpec`  
<i class="fad fa-terminal"></i> `dotnet tool install MiniSpec`

**MiniSpec** is _both_ a mini **test framework** _and_ a mini **book** on how it was authored!

### Why?

1. After returning to C# after many years away, I was dissatisfied with the latest tools.
2. I _love_ authoring test frameworks and want to share a tutorial on doing it yourself.

> Please do not author your own testing framework unless there's a good reason.  
> This is meant to be a fun learning exercise, use conventional tools instead.

### MiniSpec Syntax

```cs
// MyTest.cs (no using statement required)
//           (supports C# 9 top-level statements)

var dog = new Dog();

bool TestDogCanBark() => dog.bark() == "Woof!";
```

<hr style="clear: left;" />

[BDD]: https://en.wikipedia.org/wiki/Behavior-driven_development
[xUnit]: https://en.wikipedia.org/wiki/XUnit

[<i class="fad fa-book-open"></i> Read the Documentation](/docs) to learn more.