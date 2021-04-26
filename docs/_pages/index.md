---
permalink: /
title: ""
layout: singleWithoutTitle
author_profile: true
show_links: true
# sidebar:
#   nav: BookParts
show_download_button: true
always_show_sidebar: true
class: home
---


# MiniSpec

_Mini Testing Framework for .NET_

---

**MiniSpec** is _both_ a mini **test framework** _and_ a mini **book** on how it was authored!

### Why?

1. After returning to C# after many years away, I was dissatisfied with the latest tools.
2. I _love_ authoring test frameworks and want to share a tutorial on doing it yourself.

> Please do not author your own testing framework unless there's a good reason.  
> This is meant to be a fun learning exercise, use conventional tools instead.

### The Testing Framework

As part of the experiment, I wanted to support both [BDD][]-style and [xUnit][]-style tests:

```cs
using MiniSpec;

var dog = new Dog();

Spec.test("Dog can bark", () => {
  Expect(dog.Bark()).ToEqual("Woof!");
});
```

```cs
using MiniSpec;

var dog = new Dog();

void TestDogCanBark() {
  Assert(dog.bark()).Equals("Woof!");
}
```

[BDD]: https://en.wikipedia.org/wiki/Behavior-driven_development
[xUnit]: https://en.wikipedia.org/wiki/XUnit

[<i class="fad fa-book-open"></i> Read the Documentation](/docs) to learn more.