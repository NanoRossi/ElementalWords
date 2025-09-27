using ElementalWords;
using Shouldly;

namespace ElementalWordsCalculatorTests;

public class ElementalWordCalculatorTests
{
    private ElementalWordCalculator _calculator;

    public ElementalWordCalculatorTests()
    {
        _calculator = new ElementalWordCalculator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Calculate_HandlesNullAndEmpty(string? input)
    {
        // Act
        var result = _calculator.Calculate(input);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(0);
    }

    [Theory]
    [InlineData("H", "Hydrogen (H)")]
    [InlineData("Y", "Yttrium (Y)")]
    public void Calculate_HandlesSingleCharElement(string? input, string expectedOutput)
    {
        // Simple happy path, input is an exact match of a single symbol char
        // Act
        var result = _calculator.Calculate(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
        result[0].Count.ShouldBe(1);
        result[0][0].ShouldBe(expectedOutput);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("D")]
    public void Calculate_HandlesSingleCharElement_WhichDoesntExist(string? input)
    {
        // There are no elements with a symbol of purely A or D
        // So we should get an empty response
        // Act
        var result = _calculator.Calculate(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void Calculate_HandlesCompleteAndPartialMatch()
    {
        // Arrange
        var input = "Sc";

        // Test for a string where the word itself is a match (Scandium)
        // But also splitting it will give us Sulfur and Carbon
        // Act
        var result = _calculator.Calculate(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
        result[0].Count.ShouldBe(2);
        result[0][0].ShouldBe("Sulfur (S)");
        result[0][1].ShouldBe("Carbon (C)");
        result[1].Count.ShouldBe(1);
        result[1][0].ShouldBe("Scandium (Sc)");
    }

    [Theory]
    [InlineData("beach")]
    [InlineData("BEACH")]
    [InlineData("BeAcH")]
    public void Calculate_CapitalizationIrrelevant(string input)
    {
        // Regardless of capitization, we should get the same result
        // however the output should be consistent
        // Act
        var result = _calculator.Calculate(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
        result[0].Count.ShouldBe(3);
        result[0][0].ShouldBe("Beryllium (Be)");
        result[0][1].ShouldBe("Actinium (Ac)");
        result[0][2].ShouldBe("Hydrogen (H)");
    }

    [Fact]
    public void Calculate_HappyPath()
    {
        // Standard path - Snack is the example word
        // Has multiple combinations that we should
        // Arrange
        var input = "Snack";

        // Act
        var result = _calculator.Calculate(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(3);
        result[0].Count.ShouldBe(4);
        result[0][0].ShouldBe("Sulfur (S)");
        result[0][1].ShouldBe("Nitrogen (N)");
        result[0][2].ShouldBe("Actinium (Ac)");
        result[0][3].ShouldBe("Potassium (K)");

        result[1].Count.ShouldBe(4);
        result[1][0].ShouldBe("Sulfur (S)");
        result[1][1].ShouldBe("Sodium (Na)");
        result[1][2].ShouldBe("Carbon (C)");
        result[1][3].ShouldBe("Potassium (K)");

        result[2].Count.ShouldBe(3);
        result[2][0].ShouldBe("Tin (Sn)");
        result[2][1].ShouldBe("Actinium (Ac)");
        result[2][2].ShouldBe("Potassium (K)");
    }
}