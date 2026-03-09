# Password Strength Analyser (C# CLI Tool)

A C# console application that analyses password strength, detects weak patterns, suggests improvements, and generates stronger password alternatives.

## Features

- Checks whether a password contains:
  - at least 8 characters
  - an uppercase letter
  - a lowercase letter
  - a number
  - a special character
- Calculates a password score out of 100
- Classifies password strength as:
  - Very Weak
  - Weak
  - Medium
  - Strong
  - Very Strong
- Detects common weak patterns such as:
  - 123
  - password
  - admin
  - qwerty
- Gives suggestions for improving weak passwords
- Generates 3 random strong password suggestions for weaker passwords
- Allows the user to check multiple passwords in one session

## Technologies Used

- C#
- .NET SDK
- VS Code

## How It Works

The program checks whether a password meets important strength requirements such as length, character variety, and special characters.

It then calculates a score, applies deductions for common weak patterns, and displays the final strength level.

If the password is weak or medium, the program also generates 3 stronger password suggestions.

The user can continue checking multiple passwords without restarting the application.

## Example Test Inputs

- `password123`
- `Admin123`
- `MyPass@2026`
- `T!gerMoon47#Sky`

## How to Run

1. Open the project folder in VS Code
2. Open the terminal
3. Run:

```bash
dotnet run

## Sample Output

Example session using the CLI tool:

```text
Enter a password to analyse: password123

[PASS] At least 8 characters
[FAIL] At least one uppercase letter
[PASS] At least one lowercase letter
[PASS] At least one number
[FAIL] At least one special character

Score : 0 / 100
Result: Very Weak

Suggestions
- Add at least one uppercase letter (A-Z).
- Use a special character (e.g. ! @ # $ %).
- Consider using 12+ characters for extra strength.
- Avoid common patterns like 'password', '123', or 'qwerty'.

Suggested Strong Passwords
1. (example)
2. (example)
3. (example)

Press ENTER to continue...
Check another password? (y/n):
```
## Author

Gregory Ngatia 