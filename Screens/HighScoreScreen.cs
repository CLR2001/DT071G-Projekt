/*
  HighScoreScreen.cs
  Handles the user interface for displaying paginated high scores in a formatted table.
*/

using Spectre.Console;

namespace Quiz;

public class HighScoreScreen
{
  public static void Run(IHighScoreStorage highScoreStorage)
  {
    List<HighScore> highScores = highScoreStorage.Load();

    int pageSize = 10;
    int currentPage = 0;
    int totalPages = (int)Math.Ceiling((double)highScores.Count() / pageSize);
    string menuChoice;

    while (true)
    {
      HeaderPanel.Draw("High Scores");

      if (highScores.Count == 0)
      {
        AnsiConsole.MarkupLine("[yellow]No high scores found yet![/]\n");
        AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(["Back to Main Menu"]));
        break;
      }

      var scoresToShow = highScores
        .Skip(currentPage * pageSize)
        .Take(pageSize)
        .ToList();

      var table = new Table();
      table.Border(TableBorder.Rounded);
      table.AddColumn("[yellow]Rank[/]");
      table.AddColumn("[cyan]Name[/]");
      table.AddColumn("[green]Score[/]");
      table.AddColumn("[blue]Difficulty[/]");

      int rank = (currentPage * pageSize) + 1;
      foreach(HighScore score in scoresToShow)
      {
        table.AddRow(rank.ToString(), score.Name, score.Score.ToString(), score.Difficulty);
        rank++;
      }
      AnsiConsole.Write(table);
      AnsiConsole.MarkupLine($"[grey]Current page: {currentPage + 1}/{totalPages}[/]");

      menuChoice = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
        .AddChoices(["Next Page", "Previous Page", "Exit to Main Menu"])
        .HighlightStyle(new Style(foreground: Color.Yellow))
      );

      if (menuChoice == "Next Page")
      {
        currentPage++;

        if (currentPage >= totalPages)
        {
          currentPage = totalPages - 1;
        }
      }
      else if (menuChoice == "Previous Page")
      {
        currentPage--;
        if (currentPage < 0)
        {
          currentPage = 0;
        }
      }
      else
      {
        break;
      }

    }
  }
}