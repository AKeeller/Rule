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
	public void NotValidTest1()
	{
		var ruleSet = new IsBigAndEven();
		var result = ruleSet.Validate(10);

		Assert.IsFalse(result.IsValid);
	}

	[TestMethod]
	public void NotValidTest2()
	{
		var ruleSet = new IsBigAndEven();
		var result = ruleSet.Validate(10000001);

		Assert.IsFalse(result.IsValid);
	}

	[TestMethod]
	public void NotValidTest3()
	{
		var ruleSet = new IsBigAndEven();
		var result = ruleSet.Validate(1);

		Assert.IsFalse(result.IsValid);
	}
}