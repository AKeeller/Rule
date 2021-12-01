using AKeeller.Rule;

public class IsBigger : Rule<int>
{
	private int number { get; init; }

	public IsBigger(int number) => this.number = number;

	protected override ValidationResult PartialValidation(int data) => new() { IsValid = data > number };
}