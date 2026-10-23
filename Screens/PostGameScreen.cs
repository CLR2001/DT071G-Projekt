using Spectre.Console;

namespace Quiz;

public class PostGameScreen()
{
  public static void SaveGame(int finalScore, IHighScoreStorage highScoreStorage, GameResult result)
  {
    string message = string.Empty;
    if (result == GameResult.Completed)
    {
      HeaderPanel.Draw("Game completed");
      message = "You completed all questions";
    }
    else
    {
      HeaderPanel.Draw("Game over");
      message = "Game Over!";
    }

    AnsiConsole.MarkupLine(message);

    AnsiConsole.MarkupLine($"Your final score was [yellow]{finalScore}[/]");
    Console.WriteLine();

    string confirm = AnsiConsole.Prompt(
     new SelectionPrompt<string>()
     .Title($"Do you want to save your score?")
     .AddChoices(["(Y) - Yes", "(N) - No"])
     .HighlightStyle(new Style(foreground: Color.Yellow)));

    if (confirm != "(Y) - Yes") return;

    string playerName = AnsiConsole.Prompt(
      new TextPrompt<string>("Enter your name: ")
      .Validate(name =>
      {
        string trimmedName = name.Trim();
        if (trimmedName.Length < 3)
          return ValidationResult.Error("[red]Your name must contain at least 3 symbols. Try again![/]");
        if (trimmedName.Length > 15)
          return ValidationResult.Error("[red]Your name can't contain more than 15 symbols. Try again![/]");
        return ValidationResult.Success();
      })
    );

    List<HighScore> highScores = highScoreStorage.Load();

    HighScore newHighScore = new HighScore
    {
      Name = playerName,
      Score = finalScore,
      DateSubmitted = DateOnly.FromDateTime(DateTime.Now)
    };

    highScores.Add(newHighScore);
    var sortedHighScores = highScores.OrderByDescending(s => s.Score).ToList();
    highScoreStorage.Save(sortedHighScores);
  }

  public static bool PlayAgain()
  {
    HeaderPanel.Draw("Play again?");

    string confirmSave = AnsiConsole.Prompt(
     new SelectionPrompt<string>()
     .Title($"Do you want to play again?")
     .AddChoices(["(Y) - Yes", "(N) - No"])
     .HighlightStyle(new Style(foreground: Color.Yellow)));

    if (confirmSave == "(Y) - Yes") return true;

    return false;
  }
}