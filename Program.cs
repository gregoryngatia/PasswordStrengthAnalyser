// ================================================================
//  PasswordStrengthChecker — Advanced Edition
// ================================================================

using System;
using System.Collections.Generic;

class Program
{
    const string SpecialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";

    static readonly string[] WeakPatterns =
    {
        "123","1234","12345","123456",
        "password","pass","admin","user",
        "qwerty","abc","letmein","welcome",
        "111","000","aaa"
    };

    static void Main(string[] args)
    {
        bool runAgain = true;

        while (runAgain)
        {
            PrintBanner();

            Console.Write("  Enter a password to analyse: ");
            string password = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(password))
            {
                bool hasMinLength = HasMinimumLength(password);
                bool hasUppercase = HasUppercaseLetter(password);
                bool hasLowercase = HasLowercaseLetter(password);
                bool hasDigit = HasDigit(password);
                bool hasSpecial = HasSpecialCharacter(password);

                int rawScore = CalculateScore(password, hasMinLength, hasUppercase,
                                              hasLowercase, hasDigit, hasSpecial);

                int penalty = CalculateWeakPatternPenalty(password);
                int finalScore = Math.Max(0, rawScore - penalty);

                string strengthLabel = GetStrengthLabel(finalScore);

                List<string> suggestions = BuildSuggestions(password,
                        hasMinLength, hasUppercase, hasLowercase,
                        hasDigit, hasSpecial, penalty);

                PrintRequirementsSection(hasMinLength, hasUppercase,
                                         hasLowercase, hasDigit, hasSpecial);

                PrintScoreSection(finalScore, penalty, strengthLabel);
                PrintSuggestionsSection(suggestions);

                if (finalScore <= 60)
                {
                    List<string> generated = GenerateStrongPasswords(3);
                    PrintGeneratedPasswordsSection(generated);
                }
            }

            Console.WriteLine();
            Console.WriteLine("  Press ENTER to continue...");
            Console.ReadLine();

            Console.Write("  Check another password? (y/n): ");
            string response = (Console.ReadLine() ?? "").Trim().ToLower();

            runAgain = response == "y" || response == "yes";

            if (runAgain)
            {
                Console.WriteLine();
                Console.WriteLine("  ────────────────────────────────────────────────────");
                Console.WriteLine();
            }
        }

        Console.WriteLine("\n  Goodbye!\n");
    }

    static bool HasMinimumLength(string pw) => pw.Length >= 8;

    static bool HasUppercaseLetter(string pw)
    {
        foreach (char c in pw)
            if (char.IsUpper(c)) return true;
        return false;
    }

    static bool HasLowercaseLetter(string pw)
    {
        foreach (char c in pw)
            if (char.IsLower(c)) return true;
        return false;
    }

    static bool HasDigit(string pw)
    {
        foreach (char c in pw)
            if (char.IsDigit(c)) return true;
        return false;
    }

    static bool HasSpecialCharacter(string pw)
    {
        foreach (char c in pw)
            if (!char.IsLetterOrDigit(c)) return true;
        return false;
    }

    static int CalculateScore(string pw, bool min, bool up, bool low, bool digit, bool special)
    {
        int score = 0;

        if (min) score += 10;
        if (up) score += 10;
        if (low) score += 10;
        if (digit) score += 10;
        if (special) score += 10;

        int len = pw.Length;

        if (len >= 20) score += 30;
        else if (len >= 16) score += 20;
        else if (len >= 12) score += 10;

        int categories = 0;
        if (up) categories++;
        if (low) categories++;
        if (digit) categories++;
        if (special) categories++;

        if (categories > 1)
            score += (categories - 1) * 5;

        return Math.Min(score, 100);
    }

    static int CalculateWeakPatternPenalty(string pw)
    {
        string lower = pw.ToLower();
        int penalty = 0;

        foreach (string pattern in WeakPatterns)
            if (lower.Contains(pattern))
                penalty += 15;

        return penalty;
    }

    static string GetStrengthLabel(int score)
    {
        if (score <= 20) return "Very Weak";
        if (score <= 40) return "Weak";
        if (score <= 60) return "Medium";
        if (score <= 80) return "Strong";
        return "Very Strong";
    }

    static List<string> BuildSuggestions(string pw, bool min, bool up, bool low, bool digit, bool special, int penalty)
    {
        var tips = new List<string>();

        if (!min) tips.Add("Make your password at least 8 characters long.");
        if (!up) tips.Add("Add at least one uppercase letter (A-Z).");
        if (!low) tips.Add("Add at least one lowercase letter (a-z).");
        if (!digit) tips.Add("Include at least one number (0-9).");
        if (!special) tips.Add("Use a special character (e.g. ! @ # $ %).");
        if (pw.Length < 12) tips.Add("Consider using 12+ characters for extra strength.");
        if (penalty > 0) tips.Add("Avoid common patterns like 'password', '123', or 'qwerty'.");

        if (tips.Count == 0)
            tips.Add("Excellent password! Keep it secret and don't reuse it.");

        return tips;
    }

    static List<string> GenerateStrongPasswords(int count)
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        string all = upper + lower + digits + SpecialChars;

        var random = new Random();
        var passwords = new List<string>();

        while (passwords.Count < count)
        {
            var chars = new List<char>
            {
                upper[random.Next(upper.Length)],
                lower[random.Next(lower.Length)],
                digits[random.Next(digits.Length)],
                SpecialChars[random.Next(SpecialChars.Length)]
            };

            int targetLength = random.Next(12, 17);

            while (chars.Count < targetLength)
                chars.Add(all[random.Next(all.Length)]);

            for (int i = chars.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            string candidate = new string(chars.ToArray());

            if (!passwords.Contains(candidate))
                passwords.Add(candidate);
        }

        return passwords;
    }

    static void PrintBanner()
    {
        Console.WriteLine();
        Console.WriteLine("  ════════════════════════════════════════════════════");
        Console.WriteLine("       PASSWORD STRENGTH ANALYSER  —  Advanced       ");
        Console.WriteLine("  ════════════════════════════════════════════════════");
        Console.WriteLine();
    }

    static void PrintRequirementsSection(bool min, bool up, bool low, bool digit, bool special)
    {
        Console.WriteLine();
        Console.WriteLine("  ┌─ REQUIREMENT CHECKS ──────────────────────────────┐");
        PrintCheck("At least 8 characters", min);
        PrintCheck("At least one uppercase letter", up);
        PrintCheck("At least one lowercase letter", low);
        PrintCheck("At least one number", digit);
        PrintCheck("At least one special character", special);
        Console.WriteLine("  └───────────────────────────────────────────────────┘");
    }

    static void PrintCheck(string label, bool passed)
    {
        string mark = passed ? "PASS" : "FAIL";
        Console.WriteLine($"  │  [{mark,-4}]  {label,-34}│");
    }

    static void PrintScoreSection(int score, int penalty, string label)
    {
        Console.WriteLine();
        Console.WriteLine("  ┌─ SCORE & STRENGTH ────────────────────────────────┐");

        int filled = score / 5;
        string bar = new string('#', filled) + new string('-', 20 - filled);

        Console.WriteLine($"  │  Score   : {score,3} / 100  [{bar}]  │");
        Console.WriteLine($"  │  Penalty : -{penalty,2} pts                       │");
        Console.WriteLine($"  │  Result  : {label,-28}│");

        Console.WriteLine("  └───────────────────────────────────────────────────┘");
    }

    static void PrintSuggestionsSection(List<string> suggestions)
    {
        Console.WriteLine();
        Console.WriteLine("  ┌─ SUGGESTIONS ─────────────────────────────────────┐");

        foreach (string tip in suggestions)
        {
            List<string> wrapped = WrapText(tip, 43);

            for (int i = 0; i < wrapped.Count; i++)
            {
                if (i == 0)
                    Console.WriteLine($"  │   - {wrapped[i],-43}│");
                else
                    Console.WriteLine($"  │     {wrapped[i],-43}│");
            }
        }

        Console.WriteLine("  └───────────────────────────────────────────────────┘");
    }

    static void PrintGeneratedPasswordsSection(List<string> passwords)
    {
        Console.WriteLine();
        Console.WriteLine("  ┌─ SUGGESTED STRONG PASSWORDS ──────────────────────┐");
        Console.WriteLine("  │  Randomly generated strong alternatives:         │");
        Console.WriteLine("  │                                                  │");

        int i = 1;
        foreach (string pw in passwords)
        {
            Console.WriteLine($"  │   {i}. {pw,-42}│");
            i++;
        }

        Console.WriteLine("  │                                                  │");
        Console.WriteLine("  │  Tip: Store these in a password manager!         │");
        Console.WriteLine("  └──────────────────────────────────────────────────┘");
    }

    static List<string> WrapText(string text, int maxWidth)
    {
        var lines = new List<string>();

        while (text.Length > maxWidth)
        {
            int breakPoint = text.LastIndexOf(' ', maxWidth);
            if (breakPoint <= 0) breakPoint = maxWidth;

            lines.Add(text.Substring(0, breakPoint));
            text = text.Substring(breakPoint).TrimStart();
        }

        lines.Add(text);
        return lines;
    }
}