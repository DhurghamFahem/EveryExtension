using EveryExtension;

public class CharExtensionsTests
{
    [Theory]
    [InlineData('5', true)]
    [InlineData('x', false)]
    public void IsDigit_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsDigit();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('A', true)]
    [InlineData('5', false)]
    public void IsLetter_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsLetter();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(' ', true)]
    [InlineData('x', false)]
    public void IsWhiteSpace_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsWhiteSpace();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('a', 'A')]
    [InlineData('Z', 'Z')]
    public void ToUpperCase_ValidCharacter_ReturnsExpected(char character, char expected)
    {
        var result = character.ToUpperCase();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('A', 'a')]
    [InlineData('z', 'z')]
    public void ToLowerCase_ValidCharacter_ReturnsExpected(char character, char expected)
    {
        var result = character.ToLowerCase();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('?', true)]
    [InlineData('x', false)]
    public void IsPunctuation_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsPunctuation();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('a', true)]
    [InlineData('B', false)]
    public void IsVowel_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsVowel();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('x', true)]
    [InlineData('A', false)]
    public void IsConsonant_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsConsonant();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('A', true)]
    [InlineData('x', false)]
    public void IsUpperCase_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsUpperCase();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('a', true)]
    [InlineData('X', false)]
    public void IsLowerCase_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsLowerCase();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('A', true)]
    [InlineData('g', false)]
    public void IsHexDigit_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsHexDigit();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('1', 1)]
    [InlineData('9', 9)]
    public void ToDigit_ValidCharacter_ReturnsExpected(char character, int expected)
    {
        var result = character.ToDigit();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('*', 5, "*****")]
    [InlineData('x', 3, "xxx")]
    public void Repeat_ValidCharacter_ReturnsExpected(char character, int count, string expected)
    {
        var result = character.Repeat(count);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('+', true)]
    [InlineData('x', false)]
    public void IsMathOperator_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsMathOperator();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('\n', true)]
    [InlineData('x', false)]
    public void IsControlCharacter_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsControlCharacter();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('\uD800', true)]
    [InlineData('x', false)]
    public void IsHighSurrogate_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsHighSurrogate();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('\uDC00', true)]
    [InlineData('x', false)]
    public void IsLowSurrogate_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsLowSurrogate();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(' ', true)]
    [InlineData('x', false)]
    public void IsSeparator_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsSeparator();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('a', true)]
    [InlineData('5', true)]
    [InlineData('*', false)]
    public void IsAlphanumeric_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsAlphanumeric();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('A', 1)]
    [InlineData('Z', 26)]
    public void ToAlphabeticalIndex_ValidCharacter_ReturnsExpected(char character, int expected)
    {
        var result = character.ToAlphabeticalIndex();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('$', true)]
    [InlineData('x', false)]
    public void IsSymbol_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsSymbol();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('x', false)]
    public void IsSurrogatePair_ValidCharacter_ReturnsExpected(char character, bool expected)
    {
        var result = character.IsSurrogatePair();
        Assert.Equal(expected, result);
    }
}

