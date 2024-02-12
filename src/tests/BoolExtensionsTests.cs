namespace EveryExtension.Tests;

public class BoolExtensionsTests
{
    [Theory]
    [InlineData(true, "Yes")]
    [InlineData(false, "No")]
    public void ToYesNoString_ReturnsCorrectString(bool value, string expected)
    {
        // Act
        string result = value.ToYesNoString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "1")]
    [InlineData(false, "0")]
    public void ToBinaryString_ReturnsCorrectString(bool value, string expected)
    {
        // Act
        string result = value.ToBinaryString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void ToBit_ReturnsCorrectInt(bool value, int expected)
    {
        // Act
        int result = value.ToBit();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void Toggle_ReturnsCorrectBool(bool value, bool expected)
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
    public void ToggleIf_ReturnsCorrectBool(bool originalValue, bool condition, bool expectedResult)
    {
        // Act
        bool result = originalValue.ToggleIf(condition);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
