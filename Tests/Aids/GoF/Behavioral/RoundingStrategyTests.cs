using MVC.Aids.GoF.Behavioral;
using static MVC.Tests.Aids.GoF.Behavioral.Constants;

namespace MVC.Tests.Aids.GoF.Behavioral;

public static class Constants
{
    public const double x = 14.451;
    public const double y = -14.451;
}
[TestClass] public class RoundTests : BaseTests
{
    protected override Type? setType() => typeof(Round);
    [DataRow(3, 5, x, x)]
    [DataRow(3, 5, y, y)]
    [DataRow(2, 5, x, 14.45)]
    [DataRow(2, 5, y, -14.45)]
    [DataRow(1, 5, x, 14.5)]
    [DataRow(1, 5, y, -14.5)]
    [DataRow(0, 5, x, 14)]
    [DataRow(0, 5, y, -14)]
    [DataRow(-1, 5, x, 10)]
    [DataRow(-1, 5, y, -10)]
    [DataRow(3, 4, x, x)]
    [DataRow(3, 4, y, y)]
    [DataRow(2, 4, x, 14.45)]
    [DataRow(2, 4, y, -14.45)]
    [DataRow(1, 4, x, 14.5)]
    [DataRow(1, 4, y, -14.5)]
    [DataRow(0, 4, x, 15)]
    [DataRow(0, 4, y, -15)]
    [DataRow(-1, 4, x, 20)]
    [DataRow(-1, 4, y, -20)]
    [TestMethod] public void DoRoundTest(int decimals, int roundingDigit, double value, double expected)
    {
        var strategy = new Round(decimals, roundingDigit);
        equal(expected, strategy.DoRound(value));
    }
    [TestMethod] public void DoRoundTest1()
    {
        var strategy = new Round(2, 5);
        float val = (float)x;
        equal(strategy.DoRound(val), val.DoRound(strategy));
    }
    [TestMethod] public void DoRoundTest2()
    {
        var strategy = new Round(2, 5);
        decimal val = (decimal)x;
        equal(strategy.DoRound(val), val.DoRound(strategy));
    }
}
[TestClass] public class RoundUpTests : BaseTests
{
    protected override Type? setType() => typeof(RoundUp);
    [DataRow(3, x, x)]
    [DataRow(3, y, y)]
    [DataRow(2, x, 14.46)]
    [DataRow(2, y, -14.46)]
    [DataRow(1, x, 14.5)]
    [DataRow(1, y, -14.5)]
    [DataRow(0, x, 15)]
    [DataRow(0, y, -15)]
    [DataRow(-1, x, 20)]
    [DataRow(-1, y, -20)]
    [TestMethod] public void DoRoundTest(int decimals, double value, double expected)
    {
        var s = new RoundUp(decimals);
        equal(expected, value.DoRound(s));
    }
}
[TestClass] public class RoundDownTests : BaseTests
{
    protected override Type? setType() => typeof(RoundDown);
    [DataRow(3, x, x)]
    [DataRow(3, y, y)]
    [DataRow(2, x, 14.45)]
    [DataRow(2, y, -14.45)]
    [DataRow(1, x, 14.4)]
    [DataRow(1, y, -14.4)]
    [DataRow(0, x, 14)]
    [DataRow(0, y, -14)]
    [DataRow(-1, x, 10)]
    [DataRow(-1, y, -10)]
    [TestMethod] public void DoRoundTest(int decimals, double value, double expected)
    {
        var s = new RoundDown(decimals);
        equal(expected, value.DoRound(s));
    }
}
[TestClass] public class RoundUpByStepTests : BaseTests
{
    protected override Type? setType() => typeof(RoundUpByStep);
    [DataRow(10, x, 20)]
    [DataRow(10, y, -20)]
    [DataRow(5, x, 15)]
    [DataRow(5, y, -15)]
    [DataRow(2, x, 16)]
    [DataRow(2, y, -16)]
    [DataRow(1, x, 15)]
    [DataRow(1, y, -15)]
    [DataRow(0.5, x, 14.5)]
    [DataRow(0.5, y, -14.5)]
    [DataRow(0.25, x, 14.5)]
    [DataRow(0.25, y, -14.5)]
    [DataRow(0.2, x, 14.6)]
    [DataRow(0.2, y, -14.6)]
    [DataRow(0.1, x, 14.5)]
    [DataRow(0.1, y, -14.5)]
    [DataRow(0, x, x)]
    [DataRow(0, y, y)]
    [DataRow(-1, x, x)]
    [DataRow(-1, y, y)]
    [TestMethod] public void DoRoundTest(double step, double value, double expected)
    {
        var s = new RoundUpByStep(step);
        equal(expected, value.DoRound(s));
    }
}
[TestClass] public class RoundDownByStepTests : BaseTests
{
    protected override Type? setType() => typeof(RoundDownByStep);
    [DataRow(10, x, 10)]
    [DataRow(10, y, -10)]
    [DataRow(5, x, 10)]
    [DataRow(5, y, -10)]
    [DataRow(2, x, 14)]
    [DataRow(2, y, -14)]
    [DataRow(1, x, 14)]
    [DataRow(1, y, -14)]
    [DataRow(0.5, x, 14)]
    [DataRow(0.5, y, -14)]
    [DataRow(0.25, x, 14.25)]
    [DataRow(0.25, y, -14.25)]
    [DataRow(0.2, x, 14.40)]
    [DataRow(0.2, y, -14.40)]
    [DataRow(0.1, x, 14.4)]
    [DataRow(0.1, y, -14.4)]
    [DataRow(0, x, x)]
    [DataRow(0, y, y)]
    [DataRow(-1, x, x)]
    [DataRow(-1, y, y)]
    [TestMethod] public void DoRoundTest(double step, double value, double expected)
    {
        var s = new RoundDownByStep(step);
        equal(expected, value.DoRound(s));
    }
}
[TestClass] public class RoundTowardsPositiveTests : BaseTests
{
    protected override Type? setType() => typeof(RoundTowardsPositive);
    [DataRow(3, x, x)]
    [DataRow(2, x, 14.46)]
    [DataRow(1, x, 14.5)]
    [DataRow(0, x, 15)]
    [DataRow(-1, x, 20)]
    [DataRow(3, y, y)]
    [DataRow(2, y, -14.45)]
    [DataRow(1, y, -14.4)]
    [DataRow(0, y, -14)]
    [DataRow(-1, y, -10)]
    [TestMethod] public void DoRoundTest(int decimals, double value, double expected)
    {
        var s = new RoundTowardsPositive(decimals);
        equal(expected, value.DoRound(s));
    }
}
[TestClass] public class RoundTowardsNegativeTests : BaseTests
{
    protected override Type? setType() => typeof(RoundTowardsNegative);
    [DataRow(3, y, y)]
    [DataRow(2, y, -14.46)]
    [DataRow(1, y, -14.5)]
    [DataRow(0, y, -15)]
    [DataRow(-1, y, -20)]
    [DataRow(3, x, x)]
    [DataRow(2, x, 14.45)]
    [DataRow(1, x, 14.4)]
    [DataRow(0, x, 14)]
    [DataRow(-1, x, 10)]
    [TestMethod] public void DoRoundTest(int decimals, double value, double expected)
    {
        var s = new RoundTowardsNegative(decimals);
        equal(expected, value.DoRound(s));
    }
}
[TestClass] public class RoundingStrategyTests : BaseTests
{
    protected override Type? setType() => typeof(RoundingStrategy);
    [TestMethod] public void DoRoundTest()
    {
        double x = 1.234;
        var doublestrategy = new Round();
        var doubleresult = RoundingStrategy.DoRound(x, doublestrategy);
        equal(doublestrategy.DoRound(x), doubleresult);
        float y = 1.234f;
        var floatstrategy = new Round();
        var floatresult = RoundingStrategy.DoRound(y, floatstrategy);
        equal(floatstrategy.DoRound(y), floatresult);
        decimal z = 1.234m;
        var decimalstrategy = new Round();
        var decimalresult = RoundingStrategy.DoRound(z, decimalstrategy);
        equal(decimalstrategy.DoRound(z), decimalresult);
    }
}
[TestClass] public class BaseRoundingTests : BaseTests
{
    protected override Type? setType() => typeof(BaseRounding);
    private class TestRounding : BaseRounding
    {
        public override double DoRound(double x) => 42.42;
    }
    [TestMethod] public void DoRoundTest()
    {
        var strategy = new TestRounding();
        var result = strategy.DoRound(1.23);
        equal(42.42, result);
    }
    [TestMethod] public void DoRoundTest1()
    {
        var strategy = new TestRounding();
        var result = strategy.DoRound(1.23f);
        equal(42.42f, result);
    }
    [TestMethod] public void DoRoundTest2()
    {
        var strategy = new TestRounding();
        var result = strategy.DoRound(1.23m);
        equal(42.42m, result);
    }
}