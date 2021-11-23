namespace Rule.Test.Mock;

using System;

public class HasDigits : Rule<int>
{
    private int digits { get; init; }

    public HasDigits(int digits) => this.digits = digits;

    protected override ValidationResult PartialValidation(int data) => new() { IsValid = Math.Abs(data).ToString().Length == digits };
}