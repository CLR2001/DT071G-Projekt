using Spectre.Console;

namespace Quiz;

public class MenuScreen
{
  public static string Run()
  {
    AnsiConsole.Clear();

    var headerGrid = new Grid();
    headerGrid.AddColumn(new GridColumn().Centered());
    headerGrid.AddRow(new FigletText("QUIZMANIA").Centered().Color(Color.Yellow));

    var headerPanel = new Panel(headerGrid)
      .Border(BoxBorder.None)
      .Padding(0, 1);

    AnsiConsole.Write(Align.Center(headerPanel));
    AnsiConsole.Write(new Rule("[dim]Navigate with arrows and press Enter[/]\n"));
    Console.WriteLine();

    string menuChoice = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .AddChoices(["Start game", "High Scores", "Exit" ])
      .HighlightStyle(new Style(foreground: Color.Yellow))
     );

    return menuChoice.Trim().ToLower();
  }
}