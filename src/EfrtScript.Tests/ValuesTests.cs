/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.Values;


public sealed class ValuesTests
{
    [Fact]
    public void IsIntegerValueReturnsTrueForIntegerValue()
    {
        Assert.True(new IntegerValue(1).IsIntegerValue());
    }
    
    [Fact]
    public void IsIntegerValueReturnsFalseForNonIntegerValue()
    {
        Assert.False(new FloatValue(1).IsIntegerValue());
    }
    
    [Fact]
    public void IntegerValueHoldsValue()
    {
        Assert.Equal(123, new IntegerValue(123).Integer);
    }
    
    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(-100, true)]
    public void IntegerValueConvertsValuesToBooleanCorrectly(int i, bool expected)
    {
        var v = new IntegerValue(i);
        
        Assert.Equal(expected, v.Boolean);
    }
    
    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(1, 1.0)]
    [InlineData(-100, -100.0)]
    public void IntegerValueConvertsValuesToRealCorrectly(int i, double expected)
    {
        var v = new IntegerValue(i);
        
        Assert.Equal(expected, v.Float);
    }
    
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(-100, "-100")]
    public void IntegerValueConvertsValuesToStringCorrectly(int i, string expected)
    {
        var v = new IntegerValue(i);
        
        Assert.Equal(expected, v.String);
    }
    

    [Fact]
    public void IsRealValueReturnsTrueForIntegerValue()
    {
        Assert.True(new FloatValue(1).IsRealValue());
    }
    
    [Fact]
    public void IsRealValueReturnsFalseForNonIntegerValue()
    {
        Assert.False(new IntegerValue(1).IsRealValue());
    }
    
    [Theory]
    [InlineData(0.0, false)]
    [InlineData(1.0, true)]
    [InlineData(-100.0, true)]
    public void RealValueConvertsValuesToBooleanCorrectly(double r, bool expected)
    {
        var v = new FloatValue(r);
        
        Assert.Equal(expected, v.Boolean);
    }
    
    [Theory]
    [InlineData(0.0, 0)]
    [InlineData(0.5, 0)]
    [InlineData(0.99, 0)]
    [InlineData(1.0, 1)]
    [InlineData(-100.0, -100)]
    public void RealValueConvertsValuesToIntegerCorrectly(double r, int expected)
    {
        var v = new FloatValue(r);
        
        Assert.Equal(expected, v.Integer);
    }
    
    [Theory]
    [InlineData(0.0, "0")]
    [InlineData(1.0, "1")]
    [InlineData(-100.5, "-100.5")]
    public void RealValueConvertsValuesToStringCorrectly(double r, string expected)
    {
        var v = new FloatValue(r);
        
        Assert.Equal(expected, v.String);
    }
    
    
    [Fact]
    public void IsStringValueReturnsTrueForStringValue()
    {
        Assert.True(new StringValue("1").IsStringValue());
    }
    
    [Fact]
    public void IsStringValueReturnsFalseForNonStringValue()
    {
        Assert.False(new IntegerValue(1).IsStringValue());
    }
}
