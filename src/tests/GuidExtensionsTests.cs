using EveryExtension;

public class GuidExtensionsTests
{
    [Fact]
    public void IsEmpty_EmptyGuid_ReturnsTrue()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act
        var isEmpty = emptyGuid.IsEmpty();

        // Assert
        Assert.True(isEmpty);
    }

    [Fact]
    public void IsEmpty_NonEmptyGuid_ReturnsFalse()
    {
        // Arrange
        var nonEmptyGuid = Guid.NewGuid();

        // Act
        var isEmpty = nonEmptyGuid.IsEmpty();

        // Assert
        Assert.False(isEmpty);
    }

    [Fact]
    public void ToBase64_ReturnsBase64Representation()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var base64 = guid.ToBase64();

        // Assert
        Assert.Equal(Convert.ToBase64String(guid.ToByteArray()), base64);
    }

    [Fact]
    public void ToByteArray_ReturnsByteArrayRepresentation()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var byteArray = guid.ToByteArray();

        // Assert
        Assert.Equal(guid.ToByteArray(), byteArray);
    }

    [Fact]
    public void CombineGuids_ValidGuids_CombinesGuids()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();

        // Act
        var combinedGuid = guid1.CombineGuids(guid2);

        // Assert
        Assert.NotEqual(Guid.Empty, combinedGuid);
    }
}

