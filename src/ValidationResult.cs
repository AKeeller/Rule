namespace AKeeller.Rule;

using System.Collections.Generic;

public class ValidationResult
{
	public bool IsValid = true;
	public List<string> Messages = new();
}