using Spectre.Console;

namespace Quiz;

public class MenuScreen
{
  public static string Run()
  {
    HeaderPanel.Draw("Navigate with arrows and press Enter");

    string menuChoice = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .AddChoices(["Start game", "High Scores", "Exit" ])
      .HighlightStyle(new Style(foreground: Color.Yellow))
     );

    return menuChoice.Trim().ToLower();
  }
}