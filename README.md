# FileOrganizerCLI

![Language](https://img.shields.io/badge/language-C%23-239120?style=flat&logo=csharp)
![Platform](https://img.shields.io/badge/platform-.NET-512BD4?style=flat&logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-blue?style=flat)

A C# command-line tool that automatically organizes files in any folder into categorized subfolders — and can reverse the process to restore everything back.
No external dependencies. Single file. Runs anywhere .NET is installed.

> Built as a portfolio project to practice C#, file system operations, and CLI application development.

---

## Description

FileOrganizerCLI scans a folder and sorts every file into logical categories like Documents, Media, Code, Archives, and more — based on file extension. It shows you a preview before making any changes, and if you change your mind, the reverse feature restores everything back to where it was and cleans up the empty folders.

---

## Features

| Feature | Description |
|---|---|
| File Organization | Moves files into category subfolders based on extension |
| Preview Before Action | Shows exactly where each file will go before anything is moved |
| Reverse Organization | Restores all files back to the parent folder and removes empty subfolders |
| Duplicate Handling | Safely renames files if a filename conflict exists |
| Category Breakdown | Summary report showing how many files went into each category |
| Color-coded Output | Green for success, yellow for warnings, red for errors |
| Confirmation Prompt | Asks for Y/N confirmation before organizing or reversing |
| Clean CLI Interface | Styled headers, section dividers, and formatted tables |

---

## Categories

| Category | Extensions |
|---|---|
| Documents | `.pdf` `.doc` `.docx` `.txt` `.xls` `.xlsx` `.csv` `.ppt` `.pptx` |
| Media | `.jpg` `.png` `.mp4` `.mp3` `.gif` `.wav` `.mkv` `.svg` and more |
| Code | `.cs` `.java` `.py` `.js` `.html` `.css` `.cpp` `.json` `.xml` |
| Archives | `.zip` `.rar` `.7z` `.tar` `.gz` |
| Applications | `.exe` `.msi` `.bat` `.cmd` |
| Fonts | `.ttf` `.otf` |
| Shortcuts | `.lnk` |
| Other | Any unrecognized file types |

---

## Example Screenshots

### Organize Files
![Organize Demo](screenshots/organize-demo.png)

### Reverse Organization
![Reverse Demo](screenshots/reverse-demo.png)

### Exit Screen
![Exit Demo](screenshots/exit-demo.png)

---

## How It Works

When you choose **Organize**, the tool scans the selected folder for files at the top level only — it does not touch subfolders. Each file's extension is matched against the category map and a move plan is built. You see a full preview table before anything happens. On confirmation, files are moved and a summary breakdown is printed by category.

When you choose **Reverse**, the tool scans all known category subfolders and builds a restore plan. Again you see a preview before anything moves. On confirmation, files are moved back to the parent folder and any empty category folders are automatically deleted.

Duplicate filenames are handled safely — if a file with the same name already exists at the destination, the incoming file is renamed with a counter suffix like `filename (1).ext`.

---

## How to Run

**Requirements:** [.NET SDK](https://dotnet.microsoft.com/download) installed
```bash
git clone https://github.com/gregoryngatia/FileOrganizerCLI.git
cd FileOrganizerCLI
dotnet run
```

Once running, enter any valid folder path when prompted — for example:
- Windows: `C:\Users\YourName\Downloads`

---

## Project Structure
```
FileOrganizerCLI/
│
├── Program.cs                  ← entire application, single file
├── FileOrganizerCLI.csproj
├── FileOrganizerCLI.sln
├── README.md
├── .gitignore
│
└── screenshots/
    ├── organize-demo.png
    ├── reverse-demo.png
    └── exit-demo.png
```

---

## Technologies Used

- **C# / .NET** — core language and runtime
- **System.IO** — file and directory operations
- **Dictionary-based extension mapping** — for fast category lookups
- No external libraries or NuGet packages required

---

## License

This project is open source and available under the [MIT License](LICENSE).

---

## Author

**Gregory Ngatia**
[github.com/gregoryngatia](https://github.com/gregoryngatia)