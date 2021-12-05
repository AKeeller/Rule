using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RuleSetTest
{
	[TestMethod]
	public void ValidTest()
	{
		var ruleSet = new IsBigAndEven();
		var result = ruleSet.Validate(5000000);

		Assert.IsTrue(result.IsValid);
	}

	[TestMethod]
	[DataRow(10)]
	[DataRow(10000001)]
	[DataRow(1)]
	public void NotValidTest(int value)
	{
		var ruleSet = new IsBigAndEven();
		var result = ruleSet.Validate(value);

		Assert.IsFalse(result.IsValid);
	}
}