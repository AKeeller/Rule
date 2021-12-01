using AKeeller.Rule;
using System.Linq;

public class IsUppercase : Rule<string>
{
	protected override ValidationResult PartialValidation(string data) => new() { IsValid = data.All(char.IsUpper) };
}