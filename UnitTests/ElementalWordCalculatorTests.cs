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
    public void ElementalForms_HandlesNullAndEmpty(string? input)
    {
        // Act
        var result = _calculator.ElementalForms(input);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(0);
    }

    [Theory]
    [InlineData("H", "Hydrogen (H)")]
    [InlineData("Y", "Yttrium (Y)")]
    public void ElementalForms_HandlesSingleCharElement(string? input, string expectedOutput)
    {
        // Simple happy path, input is an exact match of a single symbol char
        // Act
        var result = _calculator.ElementalForms(input);

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
    public void ElementalForms_HandlesSingleCharElement_WhichDoesntExist(string? input)
    {
        // There are no elements with a symbol of purely A or D
        // So we should get an empty response
        // Act
        var result = _calculator.ElementalForms(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void ElementalForms_HandlesCompleteAndPartialMatch()
    {
        // Arrange
        var input = "Sc";

        // Test for a string where the word itself is a match (Scandium)
        // But also splitting it will give us Sulfur and Carbon
        // Act
        var result = _calculator.ElementalForms(input);

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
    public void ElementalForms_CapitalizationIrrelevant(string input)
    {
        // Regardless of capitization, we should get the same result
        // however the output should be consistent
        // Act
        var result = _calculator.ElementalForms(input);

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
    public void ElementalForms_HappyPath()
    {
        // Standard path - Snack is the example word
        // Has multiple combinations that we should
        // Arrange
        var input = "Snack";

        // Act
        var result = _calculator.ElementalForms(input);

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

    [Fact]
    public void ElementalForms_WordFailsMiddway()
    {
        // Want to confirm that if we have a word that matches one path
        // But then fails on a following char, we don't return a partial result
        // Xe is Xenon
        // But R, Ro and Rox are not elements
        // Arrange
        var input = "Xerox";

        // Act
        var result = _calculator.ElementalForms(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void ElementalForms_ComplexHappyPath()
    {
        // Just for my own sanity check, run the happy path again
        // nonrepresentationalisms is (according to reddit) one of the longest words that can be spelt with chemical symbols
        // Arrange
        var input = "Nonrepresentationalisms";

        // Act
        var result = _calculator.ElementalForms(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(4);

        // These are... very long, I'm not going to spend time asserting all of them one by one
        // One example will do
        result[0].Count.ShouldBe(16);
        result[1].Count.ShouldBe(15);
        result[2].Count.ShouldBe(15);
        result[3].Count.ShouldBe(14);
    }

    [Fact]
    public void ElementalForms_ThreeCharacterStrings()
    {
        // Kata deliberately calls out that we have 3 character element symbols due to the date of the periodic table
        // So an explicit test to confirm we can handle these is necessary
        // I can't think of a word that uses these letter combinations, so I'm going to use a jargon string
        // Arrange
        var input = "UusWUuoYUue";

        // Act
        var result = _calculator.ElementalForms(input);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(4);

        // 4 different ways to split this
        result[0].Count.ShouldBe(9);
        result[1].Count.ShouldBe(7);
        result[2].Count.ShouldBe(7);
        result[3].Count.ShouldBe(5);

        // Uus and Uuo broken up
        result[0][0].ShouldBe("Uranium (U)");
        result[0][1].ShouldBe("Uranium (U)");
        result[0][2].ShouldBe("Sulfur (S)");
        result[0][3].ShouldBe("Tungsten (W)");
        result[0][4].ShouldBe("Uranium (U)");
        result[0][5].ShouldBe("Uranium (U)");
        result[0][6].ShouldBe("Oxygen (O)");
        result[0][7].ShouldBe("Yttrium (Y)");
        result[0][8].ShouldBe("Ununennium (Uue)");

        // Uus broken up
        result[1][0].ShouldBe("Uranium (U)");
        result[1][1].ShouldBe("Uranium (U)");
        result[1][2].ShouldBe("Sulfur (S)");
        result[1][3].ShouldBe("Tungsten (W)");
        result[1][4].ShouldBe("Ununoctium (Uuo)");
        result[1][5].ShouldBe("Yttrium (Y)");
        result[1][6].ShouldBe("Ununennium (Uue)");

        // Uuo broken up
        result[2][0].ShouldBe("Ununseptium (Uus)");
        result[2][1].ShouldBe("Tungsten (W)");
        result[2][2].ShouldBe("Uranium (U)");
        result[2][3].ShouldBe("Uranium (U)");
        result[2][4].ShouldBe("Oxygen (O)");
        result[2][5].ShouldBe("Yttrium (Y)");
        result[2][6].ShouldBe("Ununennium (Uue)");

        // None of the three char symbols broken up
        result[3][0].ShouldBe("Ununseptium (Uus)");
        result[3][1].ShouldBe("Tungsten (W)");
        result[3][2].ShouldBe("Ununoctium (Uuo)");
        result[3][3].ShouldBe("Yttrium (Y)");
        result[3][4].ShouldBe("Ununennium (Uue)");
    }
}