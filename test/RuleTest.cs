using AKeeller.Rule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class RuleTest
{
	[TestMethod]
	public void AddRuleTest()
	{
		Rule<int> hasThreeDigits = new HasDigits(3);
		Rule<int> isEven = new IsEven();
		Rule<int> biggerThanTen = new IsBigger(10);

		var rule = hasThreeDigits
			.AddRule(isEven)
			.AddRule(biggerThanTen);

		Assert.IsTrue(rule == hasThreeDigits);
		Assert.IsTrue(rule.Next == isEven);
		Assert.IsTrue(rule.Next.Next == biggerThanTen);
	}

	[TestMethod]
	public void NoNextRuleTest()
	{
		var isEven = new IsEven();
		Assert.IsNull(isEven.Next);
	}

	[TestMethod]
	public void ValidSingleTest()
	{
		var result = new IsEven().Validate(10);
		Assert.IsTrue(result.IsValid);
	}

	[TestMethod]
	public void ValidChainedTest()
	{
		var rule = new HasDigits(3)
			.AddRule(new IsEven())
			.AddRule(new IsBigger(10));

		var result = rule.Validate(100);

		Assert.IsTrue(result.IsValid);
	}

	[TestMethod]
	public void NotValidSingleTest()
	{
		var result = new HasDigits(1).Validate(123456);
		Assert.IsFalse(result.IsValid);
	}

	[TestMethod]
	[DataRow(1)]
	[DataRow(40000)]
	public void NotValidChainedTest(int value)
	{
		var rule = new IsEven()
			.AddRule(new HasDigits(5))
			.AddRule(new IsBigger(50000));

		var result = rule.Validate(value);

		Assert.IsFalse(result.IsValid);
	}

	[TestMethod]
	public void StringValidTest()
	{
		var result = new IsUppercase().Validate("TEST");
		Assert.IsTrue(result.IsValid);
	}

	[TestMethod]
	public void StringNotValidTest()
	{
		var result = new IsUppercase().Validate("test");
		Assert.IsFalse(result.IsValid);
	}

	[TestMethod]
	public void HasMessagesTest()
	{
		var messages = new HasMessage(1)
			.AddRule(new HasMessage(2))
			.AddRule(new HasMessage(3))
			.Validate(0)
			.Messages;

		List<string> expectedMessages = new()
		{
			"Message 1",
			"Message 2",
			"Message 3"
		};

		var areEqual = messages.SequenceEqual(expectedMessages);

		Assert.IsTrue(areEqual);
	}

	[TestMethod]
	public void NoMessagesTest()
	{
		var messages = new IsEven()
			.Validate(0)
			.Messages;

		Assert.IsTrue(messages.Count is 0);
	}

	[TestMethod]
	public void SequentialValidationsTest()
	{
		var rule = new IsEven()
			.AddRule(new HasDigits(2))
			.AddRule(new IsBigger(20));

		var result1 = rule.Validate(30);
		var result2 = rule.Validate(40);
		var result3 = rule.Validate(11);
		var result4 = rule.Validate(15);
		var result5 = rule.Validate(100);

		Assert.IsTrue(result1.IsValid);
		Assert.IsTrue(result2.IsValid);
		Assert.IsFalse(result3.IsValid);
		Assert.IsFalse(result4.IsValid);
		Assert.IsFalse(result5.IsValid);
	}
}