namespace AKeeller.Rule;

using System.Collections.Generic;
using System.Linq;

public struct ValidationResult
{
	public bool IsValid = true;
	public bool IsNotValid => !IsValid;
	public List<string> Messages = new();

	public ValidationResult(){}

	public static ValidationResult operator +(ValidationResult a, ValidationResult b) =>
		new()
		{
			IsValid = a.IsValid && b.IsValid,
			Messages = a.Messages.Concat(b.Messages).ToList()
		};
}