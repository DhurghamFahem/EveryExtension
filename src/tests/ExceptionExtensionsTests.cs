namespace EveryExtension.Tests;

public class ExceptionExtensionsTests
{
    [Fact]
    public void GetFullMessage_ReturnsCorrectResult()
    {
        // Arrange
        var innerException1 = new Exception("Inner Exception 1");
        var innerException2 = new Exception("Inner Exception 2", innerException1);
        var exception = new Exception("Main Exception", innerException2);

        // Act
        string fullMessage = exception.GetFullMessage();

        // Assert
        Assert.Equal("Main Exception Inner Exception 2 Inner Exception 1", fullMessage);
    }

    [Fact]
    public void GetFullMessage_ThrowsArgumentNullExceptionForNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => ((Exception)null!).GetFullMessage());
    }
}
