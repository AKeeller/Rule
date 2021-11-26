using System.Collections.Generic;

namespace Rule.Test.Mock;

public class IsBigAndEven : RuleSet<int>
{
	protected override HashSet<Rule<int>> rules => new()
	{
		new IsBigger(1000000),
		new IsEven()
	};
}