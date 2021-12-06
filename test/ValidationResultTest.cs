using AKeeller.Rule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class ValidationResultTest
{
	[TestMethod]
	[DataRow(true, true, true)]
	[DataRow(true, false, false)]
	[DataRow(false, true, false)]
	[DataRow(false, false, false)]
	public void Sum(bool aIsValid, bool bIsValid, bool expectedIsValid)
	{
		var a = new ValidationResult { IsValid = aIsValid };
		var b = new ValidationResult { IsValid = bIsValid };
		var sum = a + b;

		Assert.AreEqual(expectedIsValid, sum.IsValid);
	}

	[TestMethod]
	public void SumMessagesTest()
	{
		var (m1, m2, m3) = ("Message 1", "Message 2", "Message 3");

		var a = new ValidationResult { Messages = new() { m1, m2 } };
		var b = new ValidationResult { Messages = new() { m3 } };
		var sum = a + b;

		var expectedMessages = new List<string>() { m1, m2, m3 };

		Assert.IsTrue(Enumerable.SequenceEqual(sum.Messages, expectedMessages));
	}

	[TestMethod]
	public void SumNoMessagesTest()
	{
		var a = new ValidationResult();
		var b = new ValidationResult();

		var sum = a + b;

		Assert.IsTrue(sum.Messages.Count is 0);
	}
}