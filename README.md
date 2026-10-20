# KemLibCore

KemLibCore is a lightweight .NET library for modeling operation results in a consistent and strongly typed way. It is designed for APIs, service layers, and business logic where a method should return success/failure information alongside optional values without relying on exceptions for normal control flow.

## Features

- Strongly typed result wrappers
- Simple success/failure contract through `IResult`
- Support for single-value and multi-value returns
- Built-in error message handling
- Compatible with .NET 10 projects

## Core types

- `Result` – represents a plain success/failure result
- `Result<T>` – success/failure with one value
- `Result<T1, T2>` – success/failure with two values
- `Result<T1, T2, T3>` – success/failure with three values
- `Result<T1, T2, T3, T4>` – success/failure with four values

## Getting started

### Prerequisites

- .NET SDK 10.0 or later

### Add the project

If you are working within the same solution, add a project reference:

```bash
dotnet add reference ../KemLibCore/KemLibCore.csproj
```

If this package is published to a NuGet feed, you can also install it via:

```bash
dotnet add package KemLibCore
```

## Usage

### Simple result

```csharp
using KemLibCore;

var result = Result.Success();

if (result.IsSuccess)
{
    Console.WriteLine("Operation succeeded.");
}
else
{
    Console.WriteLine(result.ErrorMessage);
}
```

### Result with a value

```csharp
using KemLibCore;

var result = Result.Success("Hello, KemLibCore!");

if (result.IsSuccess)
{
    Console.WriteLine(result.Value);
}
else
{
    Console.WriteLine(result.ErrorMessage);
}
```

### Failure result

```csharp
using KemLibCore;

var result = Result.Fail("Something went wrong.");

if (!result.IsSuccess)
{
    Console.WriteLine(result.ErrorMessage);
}
```

### Multiple values

```csharp
using KemLibCore;

var result = Result.Success("Alice", 42);

if (result.IsSuccess)
{
    Console.WriteLine($"Name: {result.Value1}, Age: {result.Value2}");
}
```

## API contract

The `IResult` interface exposes:

- `bool IsSuccess`
- `string ErrorMessage`

When a result is successful, `ErrorMessage` is an empty string. When a result fails, the library uses the provided error message, or `Unknown` if none is supplied.

## Example use case

This pattern is useful for methods such as:

- data validation
- authentication flows
- file or database operations
- business logic processing
- API request handling

Instead of throwing exceptions for expected failure states, you can return a structured result and handle it explicitly in the caller.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
