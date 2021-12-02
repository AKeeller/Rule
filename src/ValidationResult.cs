namespace AKeeller.Rule;

using System.Collections.Generic;
using System.Linq;

public struct ValidationResult
{
	public bool IsValid = true;
	public List<string> Messages = new();

	public static ValidationResult operator +(ValidationResult a, ValidationResult b) =>
		new()
		{
			IsValid = a.IsValid & b.IsValid,
			Messages = a.Messages.Concat(b.Messages).ToList()
		};
}