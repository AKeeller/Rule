namespace Rule.Test.Mock;

public class HasMessage : Rule<int>
{
	private int number;

	public HasMessage(int number) => this.number = number;

	protected override ValidationResult PartialValidation(int data)
	{
		ValidationResult result = new();
		result.Messages.Add($"Message {number}");
		return result;
	}
}