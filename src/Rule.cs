namespace AKeeller.Rule;

public abstract class Rule<T>
{
	public Rule<T> Next { get; private set; }

	protected abstract ValidationResult PartialValidation(T data);

	public ValidationResult Validate(T data)
	{
		var rule = this;
		var result = new ValidationResult();

		while (rule is not null)
		{
			result += rule.PartialValidation(data);
			rule = rule.Next;
		}

		return result;
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