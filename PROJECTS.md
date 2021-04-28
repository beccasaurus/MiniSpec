# Project Overview

## `MiniSpec`

```
dotnet tool install MiniSpec
```

> Intended to be a _local_ tool _only_

```
dotnet minispec
```

\- OR -

```
dotnet add package MiniSpec
```

```
MiniSpec.Tests.Run();
```

## `MiniSpec.Core`

> Standard .NET
>
> Target all the things.

```
dotnet add package MiniSpec.Core
```

```
MiniSpec.Tests.Run();
```


## `Expect`

```
dotnet add package Expect
```

```
using static Expect;

Expect("something").ToEqual("something")
```