using Spectre.Console;
using System.Net;
using System.Xml.Linq;

namespace Quiz;

public class GameScreen()
{
  public static GameResult Run(GameSession gameSession)
  {
    bool running = true;
    do
    {
      if (!gameSession.AnyQuestionsLeft())
      {
        return GameResult.Completed;
      }

      HeaderPanel.Draw($"Question {gameSession.GetCurrentQuestionIndex()}");

      Question question = gameSession.GetQuestion();

      AnsiConsole.MarkupLine($"Current score: [yellow]{gameSession.GetScore()}[/]");
      Console.WriteLine();
      AnsiConsole.MarkupLine(WebUtility.HtmlDecode(question.QuestionText));
      Console.WriteLine();

      string userAnswer = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
          .AddChoices(question.GetAllAnswers())
          .HighlightStyle(new Style(foreground: Color.Yellow))
      );


      bool correct = gameSession.SubmitAnswer(question, userAnswer);

      if (!correct) 
      {
        running = false;
        return GameResult.GameOver;
      } 
    } while (running == true);

    return GameResult.Completed;
  }
}