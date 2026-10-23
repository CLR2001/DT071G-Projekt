using Spectre.Console;
using System.Xml.Linq;

namespace Quiz;

public class GameScreen()
{
  public static void Run(GameSession gameSession)
  {
    bool running = true;
    do
    {
      HeaderPanel.Draw($"Question {gameSession.GetCurrentQuestionIndex()}");

      if (!gameSession.AnyQuestionsLeft())
      {
        AnsiConsole.MarkupLine("You answered all questions correct!");
        AnsiConsole.MarkupLine("Choose more categories in order to get a higher score.");
      }


      Question question = gameSession.GetQuestion();

      string userAnswer = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .AddChoices(question.GetAllAnswers())
      .HighlightStyle(new Style(foreground: Color.Yellow))
     );


    } while (running == true);
  }
}