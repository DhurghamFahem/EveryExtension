using System.ComponentModel;

namespace EveryExtension.Tests;

public enum TestGetDescriptionEnum
{
    [Description("First Description")]
    FirstValue,

    [Description("Second Description")]
    SecondValue,

    ThirdValue
}

[Flags]
public enum TestHasFlagFlagEnum
{
    None = 0,
    FirstFlag = 1,
    SecondFlag = 2,
    ThirdFlag = 4,
    AllFlags = FirstFlag | SecondFlag | ThirdFlag
}

public enum TestEnum
{
    [Description("First Description")]
    [Category("CategoryA")]
    FirstValue,

    [Description("Second Description")]
    [Category("CategoryB")]
    SecondValue,

    [Category("CategoryC")]
    ThirdValue
}

public class EnumExtensionsTests
{
    [Theory]
    [InlineData(TestGetDescriptionEnum.FirstValue, "First Description")]
    [InlineData(TestGetDescriptionEnum.SecondValue, "Second Description")]
    [InlineData(TestGetDescriptionEnum.ThirdValue, "ThirdValue")]
    public void GetDescription_ReturnsCorrectResult(TestGetDescriptionEnum enumValue, string expectedDescription)
    {
        // Act
        string result = enumValue.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, result);
    }

    [Theory]
    [InlineData(TestHasFlagFlagEnum.FirstFlag, TestHasFlagFlagEnum.FirstFlag, true)]
    [InlineData(TestHasFlagFlagEnum.FirstFlag | TestHasFlagFlagEnum.SecondFlag, TestHasFlagFlagEnum.FirstFlag, true)]
    [InlineData(TestHasFlagFlagEnum.SecondFlag, TestHasFlagFlagEnum.FirstFlag, false)]
    [InlineData(TestHasFlagFlagEnum.AllFlags, TestHasFlagFlagEnum.FirstFlag | TestHasFlagFlagEnum.SecondFlag, true)]
    [InlineData(TestHasFlagFlagEnum.AllFlags, TestHasFlagFlagEnum.None, true)]
    [InlineData(TestHasFlagFlagEnum.None, TestHasFlagFlagEnum.AllFlags, false)]
    public void HasFlag_ReturnsCorrectResult(TestHasFlagFlagEnum enumValue, TestHasFlagFlagEnum flag, bool expectedResult)
    {
        // Act
        bool result = enumValue.HasFlag(flag);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
