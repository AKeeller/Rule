namespace AKeeller.Rule;

using System.Collections.Generic;

public struct ValidationResult
{
	public bool IsValid = true;
	public List<string> Messages = new();
}