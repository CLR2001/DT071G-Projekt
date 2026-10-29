using Spectre.Console;

namespace Quiz;

public class PostGameScreen()
{
  public static void SaveGame(int finalScore, IHighScoreStorage highScoreStorage, GameSettings gameSettings, GameResult result)
  {
    if (finalScore <= 0)
    {
      HeaderPanel.Draw("Game Over");
      AnsiConsole.MarkupLine("[red]You got 0 points, so no highscore could be saved![/]");

      Thread.Sleep(1500);
      while (Console.KeyAvailable)
      {
        Console.ReadKey(intercept: true);
      }

      return;
    }

    string message = string.Empty;
    if (result == GameResult.Completed)
    {
      HeaderPanel.Draw("Game Completed");
      message = "You completed all questions";
    }
    else
    {
      HeaderPanel.Draw("Game Over");
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
      Date = DateOnly.FromDateTime(DateTime.Now),
      Difficulty = gameSettings.SelectedDifficulty
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