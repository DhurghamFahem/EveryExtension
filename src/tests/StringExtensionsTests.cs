namespace EveryExtension.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("example@email.com", true)]
    [InlineData("example@email.cm", true)]
    [InlineData("example@emal.com", true)]
    [InlineData("@email.com", false)]
    [InlineData("exampleemail.com", false)]
    [InlineData("example@emailcom", true)]
    [InlineData("192.168.1.1", false)]
    [InlineData("notanipaddress", false)]
    public void IsEmail_ReturnsCorrectResult(string input, bool expectedResult)
    {
        // Act
        bool result = input.IsEmail();

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
