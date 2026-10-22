using Spectre.Console;

namespace Quiz;

public static class HeaderPanel
{
  public static void Draw(string ruleText)
  {
    AnsiConsole.Clear();

    var headerGrid = new Grid();
    headerGrid.AddColumn(new GridColumn().Centered());
    headerGrid.AddRow(new FigletText("QUIZMANIA").Centered().Color(Color.Yellow));

    var headerPanel = new Panel(headerGrid)
      .Border(BoxBorder.None)
      .Padding(0, 1);

    AnsiConsole.Write(Align.Center(headerPanel));
    AnsiConsole.Write(new Rule($"[dim]{ruleText}[/]\n"));
    Console.WriteLine();
  }
}