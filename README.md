# Rule

![AKeeller-Rule](https://img.shields.io/badge/AKeeller-Rule-blue)
![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/AKeeller/Rule/.NET/main)
![Nuget](https://img.shields.io/nuget/dt/AKeeller.Rule)

Create business rules in a simple and elegant way.

## How to install

Simply add the package `AKeeller.Rule`:

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

You only need to implement a single method which returns a `ValidationResult`. The latter is a class with only two properties:

- `bool IsValid`
- `List<string> Messages`.

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

#### **Why no lazy evaluation?**
The idea is to receive error, status and info messages from every rule.
