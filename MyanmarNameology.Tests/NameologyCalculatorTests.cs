using MyanmarNameology.Models;
using MyanmarNameology.Services;

namespace MyanmarNameology.Tests;

public class NameologyCalculatorTests
{
    public static TheoryData<string, string, int, int, int, string, string> MyanmarNameCases => new()
    {
        { "ဆလထ", "ဆလထ", 14, 21, 3, "သူခိုးကိန်း", "ဆ=3 + လ=4 + ထ=7" },
        { "ဆန်းလင်းထွန်း", "ဆလထ", 14, 21, 3, "သူခိုးကိန်း", "ဆ=3 + လ=4 + ထ=7" },
        { "ဘုန်းဝင့်ထွန်း", "ဘဝထ", 16, 23, 5, "မိဖုရားကိန်း", "ဘ=5 + ဝ=4 + ထ=7" },
        { "ဉာဏ်လင်းထွန်း", "ညလထ", 14, 21, 3, "သူခိုးကိန်း", "ည=3 + လ=4 + ထ=7" },
        { "သီရိလင်း", "သရလ", 14, 21, 3, "သူခိုးကိန်း", "သ=6 + ရ=4 + လ=4" },
        { "လှိုင်မျိုးအောင်", "လမအ", 10, 17, 8, "ပုဏ္ဏားကိန်း", "လ=4 + မ=5 + အ=1" },
        { "ဥက္ကအောင်", "ဥကအ", 4, 11, 2, "သူဌေးကိန်း", "ဥ=1 + က=2 + အ=1" }
    };

    [Theory]
    [MemberData(nameof(MyanmarNameCases))]
    public void Calculate_ReturnsExpectedResultsForMyanmarNames(
        string input,
        string expectedKeyword,
        int expectedTotal,
        int expectedTotalPlusSeven,
        int expectedRemainder,
        string expectedTitle,
        string expectedBreakdown)
    {
        var result = NameologyCalculator.Calculate(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(NameologyResultStatus.Success, result.Status);
        Assert.Equal(input, result.Name);
        Assert.Equal(expectedKeyword, result.Keyword);
        Assert.Equal(expectedTotal, result.Total);
        Assert.Equal(expectedTotalPlusSeven, result.TotalPlusSeven);
        Assert.Equal(expectedRemainder, result.Remainder);
        Assert.Equal(expectedTitle, result.Meaning?.Title);
        Assert.Equal(expectedBreakdown, FormatBreakdown(result));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Calculate_ReturnsEmptyStatusWhenInputIsBlank(string input)
    {
        var result = NameologyCalculator.Calculate(input);

        Assert.False(result.IsSuccess);
        Assert.Equal(NameologyResultStatus.Empty, result.Status);
        Assert.Empty(result.Keyword);
        Assert.Empty(result.MatchedLetters);
    }

    [Fact]
    public void Calculate_ReturnsNoMyanmarLettersStatusForLatinInput()
    {
        var result = NameologyCalculator.Calculate("Sann Lynn Htun");

        Assert.False(result.IsSuccess);
        Assert.Equal(NameologyResultStatus.NoMyanmarLetters, result.Status);
        Assert.Empty(result.Keyword);
        Assert.Empty(result.MatchedLetters);
    }

    private static string FormatBreakdown(NameologyResult result)
    {
        return string.Join(" + ", result.MatchedLetters.Select(item => $"{item.Letter}={item.Value}"));
    }
}
