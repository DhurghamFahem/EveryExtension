namespace EveryExtension.Tests;

public class BoolExtensionsTests
{
    [Theory]
    [InlineData(true, "Yes")]
    [InlineData(false, "No")]
    public void ToYesNoString_ReturnsCorrectResult(bool value, string expected)
    {
        // Act
        string result = value.ToYesNoString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "1")]
    [InlineData(false, "0")]
    public void ToBinaryString_ReturnsCorrectResult(bool value, string expected)
    {
        // Act
        string result = value.ToBinaryString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void ToBit_ReturnsCorrectResult(bool value, int expected)
    {
        // Act
        int result = value.ToBit();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void Toggle_ReturnsCorrectResult(bool value, bool expected)
    {
        // Act 
        bool result = value.Toggle();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(false, false, false)]
    public void ToggleIf_ReturnsCorrectResult(bool originalValue, bool condition, bool expectedResult)
    {
        // Act
        bool result = originalValue.ToggleIf(condition);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void IfTrue_ActionExecution(bool value, bool expected)
    {
        // Arrange
        bool actionExecuted = false;

        // Act
        value.IfTrue(() => actionExecuted = true);

        // Assert
        Assert.Equal(expected, actionExecuted);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, false, true)]
    public void IfTrue_TwoActionsExecution(bool value, bool trueActionExecuted, bool falseActionExecuted)
    {
        // Arrange
        bool trueActionFlag = false;
        bool falseActionFlag = false;

        // Act
        value.IfTrue(() => trueActionFlag = true, () => falseActionFlag = true);

        // Assert
        Assert.Equal(trueActionExecuted, trueActionFlag);
        Assert.Equal(falseActionExecuted, falseActionFlag);
    }

    [Theory]
    [InlineData(true, "TrueValue", "FalseValue", "TrueValue")]
    [InlineData(false, "TrueValue", "FalseValue", "FalseValue")]
    public void IfTrue_TwoFunctionsExecution(bool value, string trueValue, string falseValue, string expectedResult)
    {
        // Act
        string result = value.IfTrue(() => trueValue, () => falseValue);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true, true, true, true)]
    [InlineData(false, true, true, false)]
    [InlineData(false, true, false, false)]
    [InlineData(false, false, true, false)]
    [InlineData(false, false, false, false)]
    public void And_ReturnsCorrectResult(bool orginalValue, bool condition1, bool condition2, bool expected)
    {
        // Act
        bool result = orginalValue.And(condition1)
                                  .And(condition2);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, true, true, true)]
    [InlineData(false, true, true, true)]
    [InlineData(false, true, false, true)]
    [InlineData(false, false, true, true)]
    [InlineData(false, false, false, false)]
    public void Or_ReturnsCorrectResult(bool orginalValue, bool condition1, bool condition2, bool expected)
    {
        // Act
        bool result = orginalValue.Or(condition1)
                                  .Or(condition2);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(false, false, false)]
    public void Xor_ReturnsCorrectResult(bool orginalValue, bool value2, bool expectedResult)
    {
        // Act
        bool result = orginalValue.Xor(value2);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true, "IfTrue", "IfFalse", "IfTrue")]
    [InlineData(false, "IfTrue", "IfFalse", "IfFalse")]
    public void ToString_ReturnsCorrectResult(bool orginalValue, string ifTrue, string ifFalse, string expected)
    {
        // Act
        string result = orginalValue.ToString(ifTrue, ifFalse);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(null, true)]
    [InlineData(true, false)]
    public void IsNullOrFalse_ReturnsCorrectResult(bool? orginalValue, bool expected)
    {
        // Act
        bool result = orginalValue.IsNullOrFalse();

        // Assert
        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(true, "✅")]
    [InlineData(false, "❌")]
    public void ToEmoji_ReturnsCorrectResult(bool value, string expectedEmoji)
    {
        // Act
        string result = value.ToEmoji();

        // Assert
        Assert.Equal(expectedEmoji, result);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public void ToggleIfTrue_ReturnsCorrectResult(bool originalValue, bool expectedResult)
    {
        // Act
        bool result = originalValue.ToggleIfTrue();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, true)]
    public void ToggleIfFalse_ReturnsCorrectResult(bool originalValue, bool expectedResult)
    {
        // Act
        bool result = originalValue.ToggleIfFalse();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public void IsSameAs_ReturnsCorrectResult(bool value, bool other, bool expectedResult)
    {
        // Act
        bool result = value.IsSameAs(other);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(false, false, false)]
    public void IsDistinct_ReturnsCorrectResult(bool value1, bool value2, bool expectedResult)
    {
        // Act
        bool result = value1.IsDistinct(value2);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
