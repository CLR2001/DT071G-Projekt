using Spectre.Console;
using System.Net;
using System.Xml.Linq;

namespace Quiz;

public class GameScreen()
{
  public static GameResult Run(GameSession gameSession)
  {
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

      if (correct)
      {
        AnsiConsole.MarkupLine("[green]Correct![/]");
      }
      else
      {
        AnsiConsole.MarkupLine($"[red]Wrong! Correct answer was:[/] [green]{WebUtility.HtmlDecode(question.CorrectAnswer)}[/]");

        Thread.Sleep(1500);
        while (Console.KeyAvailable)
        {
          Console.ReadKey(intercept: true);
        }

        return GameResult.GameOver;
      }

      Thread.Sleep(1000);
      while (Console.KeyAvailable)
      {
        Console.ReadKey(intercept: true);
      }
    } while (true);
  }
}