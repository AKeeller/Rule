using System.Collections.Generic;
using AKeeller.Rule;

public class IsBigAndEven : RuleSet<int>
{
	protected override HashSet<Rule<int>> rules => new()
	{
		new IsBigger(1000000),
		new IsEven()
	};
}