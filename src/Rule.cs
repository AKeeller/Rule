namespace AKeeller.Rule;

public abstract class Rule<T>
{
	public Rule<T> Next { get; set; }

	protected abstract ValidationResult PartialValidation(T data);

	public ValidationResult Validate(T data)
	{
		var result = Next is null ? new() : Next.Validate(data);

		return result += PartialValidation(data);
	}

	public Rule<T> AddRule(Rule<T> next)
	{
		Rule<T> lastRule = this;

		while (lastRule.Next is not null)
			lastRule = lastRule.Next;

		lastRule.Next = next;

		return this;
	}
}