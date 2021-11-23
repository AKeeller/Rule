namespace Rule.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rule.Test.Mock;

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
    public void NotValidChainedTest()
    {
        var rule = new IsEven()
        .AddRule(new HasDigits(5))
        .AddRule(new IsBigger(10));

        var result = rule.Validate(1);

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
}