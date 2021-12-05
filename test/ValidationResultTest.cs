using AKeeller.Rule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class ValidationResultTest
{
	[TestMethod]
	public void Sum1()
	{
		var a = new ValidationResult { IsValid = true };
		var b = new ValidationResult { IsValid = true };
		var sum = a + b;

		Assert.IsTrue(sum.IsValid);
	}

	[TestMethod]
	public void Sum2()
	{
		var a = new ValidationResult { IsValid = true };
		var b = new ValidationResult { IsValid = false };
		var sum = a + b;

		Assert.IsFalse(sum.IsValid);
	}

	[TestMethod]
	public void Sum3()
	{
		var a = new ValidationResult { IsValid = false };
		var b = new ValidationResult { IsValid = true };
		var sum = a + b;

		Assert.IsFalse(sum.IsValid);
	}

	[TestMethod]
	public void SumMessages()
	{
		var (m1, m2, m3) = ("Message 1", "Message 2", "Message 3");

		var a = new ValidationResult { Messages = new() { m1, m2 } };
		var b = new ValidationResult { Messages = new() { m3 } };
		var sum = a + b;

		var expectedMessages = new List<string>() { m1, m2, m3 };

		Assert.IsTrue(Enumerable.SequenceEqual(sum.Messages, expectedMessages));
	}
}