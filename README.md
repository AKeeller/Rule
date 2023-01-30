# Rule

![AKeeller-Rule](https://img.shields.io/badge/AKeeller-Rule-blue)
[![Build](https://github.com/AKeeller/Rule/actions/workflows/build.yml/badge.svg)](https://github.com/AKeeller/Rule/actions/workflows/build.yml)

Create business rules in a simple and elegant way.

## How to install

1. Generate a new GitHub [Personal Access Token](https://github.com/settings/tokens) with `read:packages` permission.

2. Add a new *NuGet* source:

    ```shell
    # Substitute <USERNAME> with your GitHub username and <TOKEN> with your Personal Access Token.

    dotnet nuget add source 'https://nuget.pkg.github.com/akeeller/index.json' --name 'github-akeeller' --username <USERNAME> --password <TOKEN>
    ```

    On platforms other than Windows, you might need to add the option `--store-password-in-clear-text`.

3. While in the root folder of your project, simply add the package `AKeeller.Rule`:

    ```shell
    dotnet add package AKeeller.Rule
    ```

## How to use

> **🧑‍💻** In the following examples the `using AKeeller.Rule` directive has been omitted for simplicity.

### Subclass `Rule`

First of all, create a meaningful subclass of `Rule<>`.\
`IsEven` can be a good choice to start practicing:

```csharp
public class IsEven : Rule<int>
{
  protected override ValidationResult PartialValidation(int data) =>
    new() { IsValid = data % 2 is 0 };
}
```

You only need to implement a single method which returns a `ValidationResult`. The latter is a struct with only two properties:

- `bool IsValid`
- `List<string> Messages`

That's it. Now we're ready to use our business rule to validate an `int`.

### Usage

In your main, create an instance of `IsEven` and use it to validate your data. Then, use the `ValidationResult` to do your stuffs.

```csharp
int number = 10;
var rule = new IsEven();
var result = rule.Validate(number);

Console.WriteLine(result.IsValid ? "Valid!" : "Not valid"); // Outputs: Valid!
```

### Chain rules

Now the most important part. You can chain as many rules as you want to validate your data. Let's say we created three `Rule`s: `IsEven`, `IsPerfectSquare` and `IsPalindromic`.
We could check all these rules in one simple step like this:

```csharp
var number = 676;

var rule = new IsEven()
  .AddRule(new IsPerfectSquare())
  .AddRule(new IsPalindromic());

var result = rule.Validate(number);

Console.WriteLine(result.IsValid ? "Valid!" : "Not valid"); // Outputs: Valid!
```

### Messages

It's also possible to include one ore more messages in your validation results. Let's see how they work.

In one of yours `Rule`s, add a `Message` to the `ValidationResult`:

```csharp
public class IsEven : Rule<int>
{
  protected override ValidationResult PartialValidation(int data)
  {
    var result = new ValidationResult
    {
      IsValid = data % 2 == 0
    };

    if (result.IsNotValid)
      result.Messages.Add("This number is not even!");

    return result;
  }
}
```

As you can see, a `Message` is added if the number is not even.

In your main, you can read messages like this:

```csharp
var number = 7;
var rule = new IsEven();
var result = rule.Validate(number);

foreach (string message in result.Messages)
  Console.WriteLine(message); // Outputs: This number is not even!
```

## FAQs

### **Why no lazy evaluation?**

The idea is to receive error, status and info messages from every rule.
