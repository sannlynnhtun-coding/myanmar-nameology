using MyanmarNameology.Models;

namespace MyanmarNameology.Services;

public static class NameologyCalculator
{
    private const char Asat = '်';

    public static IReadOnlyList<LetterValueGroup> LetterGroups { get; } =
    [
        new(1, "တနင်္ဂနွေ", "၁", ['အ', 'ဣ', 'ဤ', 'ဥ', 'ဦ', 'ဧ', 'ဩ', 'ဪ']),
        new(2, "တနင်္လာ", "၂", ['က', 'ခ', 'ဂ', 'ဃ', 'င']),
        new(3, "အင်္ဂါ", "၃", ['စ', 'ဆ', 'ဇ', 'ဈ', 'ည']),
        new(4, "ဗုဒ္ဓဟူး", "၄", ['ယ', 'ရ', 'လ', 'ဝ']),
        new(5, "ကြာသပတေး", "၅", ['ပ', 'ဖ', 'ဗ', 'ဘ', 'မ']),
        new(6, "သောကြာ", "၆", ['သ', 'ဟ', 'ဠ']),
        new(7, "စနေ", "၇", ['တ', 'ထ', 'ဒ', 'ဓ', 'န'])
    ];

    private static readonly IReadOnlyDictionary<char, int> LetterValues =
        LetterGroups
            .SelectMany(group => group.Letters.Select(letter => new KeyValuePair<char, int>(letter, group.Value)))
            .ToDictionary(pair => pair.Key, pair => pair.Value);

    private static readonly IReadOnlyDictionary<int, NameologyMeaning> Meanings =
        new Dictionary<int, NameologyMeaning>
        {
            [1] = new(1, "သူဆင်းရဲကိန်း", "ကြိုးစားသလောက် အကျိုးခံစားရမှုနည်းတတ်သူလို့ ယူဆကြပါတယ်။"),
            [2] = new(2, "သူဌေးကိန်း", "ငွေသုံးကြမ်းတတ်ပေမယ့် လိုအပ်ချိန်မှာ အခွင့်အလမ်းရတတ်သူလို့ ယူဆကြပါတယ်။"),
            [3] = new(3, "သူခိုးကိန်း", "အန္တရာယ်၊ သတိထားစရာ၊ အလွဲအမှားများကို သတိထားသင့်သူလို့ ယူဆကြပါတယ်။"),
            [4] = new(4, "မင်းကိန်း", "ထက်မြက်သူ၊ သြဇာရှိသူ၊ ဦးဆောင်နိုင်သူလို့ ယူဆကြပါတယ်။"),
            [5] = new(5, "မိဖုရားကိန်း", "အထောက်အပံ့ကောင်းရင် အောင်မြင်တတ်သူလို့ ယူဆကြပါတယ်။"),
            [6] = new(6, "ဘီလူးကိန်း", "ဘဝအတက်အကျ မြန်တတ်သူလို့ ယူဆကြပါတယ်။"),
            [7] = new(7, "အမတ်ကိန်း", "လူချစ်လူခင် ပေါများပြီး ဆက်ဆံရေးကောင်းတတ်သူလို့ ယူဆကြပါတယ်။"),
            [8] = new(8, "ပုဏ္ဏားကိန်း", "ကိုယ်ပိုင်အမြင်ပြင်းပြီး ကိုယ်တိုင်ဆုံးဖြတ်ချင်စိတ်များတတ်သူလို့ ယူဆကြပါတယ်။"),
            [9] = new(9, "ရဟန်းကိန်း", "တည်ငြိမ်မှု၊ သန့်ရှင်းမှု၊ သဘောထားကြီးမှုနဲ့ ဆက်စပ်ယူဆကြပါတယ်။")
        };

    public static NameologyResult Calculate(string? input)
    {
        var name = input?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            return new NameologyResult
            {
                Status = NameologyResultStatus.Empty,
                Name = string.Empty,
                Keyword = string.Empty,
                Message = "တွက်ရန်အတွက် နာမည်တစ်ခု ထည့်ပေးပါ။"
            };
        }

        var matchedLetters = new List<MatchedLetter>();
        var keywordLetters = ExtractKeywordLetters(name);

        foreach (var character in keywordLetters)
        {
            if (LetterValues.TryGetValue(character, out var value))
            {
                matchedLetters.Add(new MatchedLetter(character, value));
            }
        }

        if (matchedLetters.Count == 0)
        {
            return new NameologyResult
            {
                Status = NameologyResultStatus.NoMyanmarLetters,
                Name = name,
                Keyword = string.Empty,
                Message = "တွက်လို့ရမယ့် မြန်မာအက္ခရာများ ထည့်ပေးပါ။"
            };
        }

        var total = matchedLetters.Sum(item => item.Value);
        var totalPlusSeven = total + 7;
        var remainder = totalPlusSeven % 9;

        if (remainder == 0)
        {
            remainder = 9;
        }

        return new NameologyResult
        {
            Status = NameologyResultStatus.Success,
            Name = name,
            Message = "ရလဒ်ထွက်ပြီးပါပြီ။",
            Keyword = new string(keywordLetters.ToArray()),
            Total = total,
            TotalPlusSeven = totalPlusSeven,
            Remainder = remainder,
            MatchedLetters = matchedLetters,
            Meaning = Meanings[remainder]
        };
    }

    private static List<char> ExtractKeywordLetters(string text)
    {
        var letters = new List<char>();

        for (var index = 0; index < text.Length; index++)
        {
            var character = text[index];

            if (!LetterValues.ContainsKey(character) || IsFinalConsonant(text, index))
            {
                continue;
            }

            letters.Add(character);
        }

        return letters;
    }

    private static bool IsFinalConsonant(string text, int index)
    {
        for (var nextIndex = index + 1; nextIndex < text.Length; nextIndex++)
        {
            var character = text[nextIndex];

            if (character == Asat)
            {
                return true;
            }

            if (!CanAppearBeforeAsat(character))
            {
                return false;
            }
        }

        return false;
    }

    private static bool CanAppearBeforeAsat(char character)
    {
        return character is '့' or 'း';
    }
}
