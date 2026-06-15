namespace MyanmarNameology.Models;

public enum NameologyResultStatus
{
    Empty,
    NoMyanmarLetters,
    Success
}

public sealed record MatchedLetter(char Letter, int Value);

public sealed record LetterValueGroup(
    int Value,
    string DayName,
    string MyanmarNumber,
    IReadOnlyList<char> Letters);

public sealed record NameologyMeaning(
    int Number,
    string Title,
    string Text);

public sealed record NameologyResult
{
    public required NameologyResultStatus Status { get; init; }
    public required string Name { get; init; }
    public required string Message { get; init; }
    public int Total { get; init; }
    public int TotalPlusSeven { get; init; }
    public int Remainder { get; init; }
    public IReadOnlyList<MatchedLetter> MatchedLetters { get; init; } = Array.Empty<MatchedLetter>();
    public NameologyMeaning? Meaning { get; init; }
    public bool IsSuccess => Status == NameologyResultStatus.Success;
}
