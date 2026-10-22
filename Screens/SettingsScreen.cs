using Spectre.Console;

namespace Quiz;

public class SettingsScreen()
{
  public static GameSettings Run(QuizService quizService)
  {
    // Categories setting
    HeaderPanel.Draw("Choose categories");
    var allCategories = quizService.GetCategories();

    var prompt = new MultiSelectionPrompt<string>()
      .AddChoices(allCategories)
      .HighlightStyle(new Style(foreground: Color.Yellow));

    foreach (var category in allCategories)
    {
      prompt.Select(category);
    }

    List<string> selectedCategories = AnsiConsole.Prompt(prompt);

    // Difficulty setting
    HeaderPanel.Draw("Choose difficulty");
    string selectedDifficulty = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
        .AddChoices(["All", "Easy", "Medium", "Hard"])
        .HighlightStyle(new Style(foreground: Color.Yellow))
      );

    return new GameSettings { SelectedCategories = selectedCategories, SelectedDifficulty = selectedDifficulty }; 
  }
}