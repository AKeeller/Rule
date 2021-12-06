using AKeeller.Rule;

public class HasMessage : Rule<int>
{
	private int number;

	public HasMessage(int number) => this.number = number;

	protected override ValidationResult PartialValidation(int data) => new() { Messages = { $"Message {number}" } };
}