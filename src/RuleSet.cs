using System.Collections.Generic;
using System.Linq;

public abstract class RuleSet<T>
{
	protected abstract HashSet<Rule<T>> rules { get; }

	public ValidationResult Validate(T data)
	{
		var rule = rules.First();
		rules.Remove(rule);

		foreach (var r in rules)
			rule.AddRule(r);

		return rule.Validate(data);
	}
}