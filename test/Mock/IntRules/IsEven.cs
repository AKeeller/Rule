namespace Rule.Test.Mock;

public class IsEven : Rule<int>
{
    protected override ValidationResult PartialValidation(int data) => new() { IsValid = data % 2 is 0 };
}