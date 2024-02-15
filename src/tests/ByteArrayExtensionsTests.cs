namespace EveryExtension.Tests;

public class ByteArrayExtensionsTests
{
    [Theory]
    [InlineData(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "00000000-0000-0000-0000-000000000000")]
    [InlineData(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 }, "ffffffff-ffff-ffff-ffff-ffffffffffff")]
    [InlineData(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, "00000000-0000-0000-0000-000000000001")]
    public void ToGuid_ReturnsCorrectGuid(byte[] bytes, string expectedGuid)
    {
        // Act
        Guid result = bytes.ToGuid();

        // Assert
        Assert.Equal(new Guid(expectedGuid), result);
    }

    [Theory]
    [InlineData(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new string[] { "00000000-0000-0000-0000-000000000000" })]
    [InlineData(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 }, new string[] { "ffffffff-ffff-ffff-ffff-ffffffffffff" })]
    [InlineData(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, new string[] { "00000000-0000-0000-0000-000000000001" })]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, new string[] { "04030201-0605-0807-090a-0b0c0d0e0f10" })]
    public void ToGuidArray_ReturnsCorrectGuidArray(byte[] byteArray, string[] expectedGuids)
    {
        // Act
        Guid[] result = byteArray.ToGuidArray();

        // Assert
        Assert.Equal(expectedGuids.Length, result.Length);

        for (int i = 0; i < expectedGuids.Length; i++)
        {
            Assert.Equal(new Guid(expectedGuids[i]), result[i]);
        }
    }

    [Fact]
    public void ToGuidArray_ThrowsArgumentExceptionForInvalidLength()
    {
        // Arrange
        byte[] byteArray = [ 1, 2, 3 ];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => byteArray.ToGuidArray());
    }
}
