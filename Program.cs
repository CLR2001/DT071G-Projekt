using Quiz;
using Spectre.Console;

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "questions.json");
IQuestionLoader questionLoader = new JsonQuestionLoader(filePath);
IHighScoreStorage highScoreStorage = new JsonHighScoreStorage("highScores.json");
QuizService quizService = new QuizService(questionLoader);

var gameSession = new GameSession(quizService);

try { Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight); } catch { };

bool running = true;
do
{
  string menuChoice = MenuScreen.Run();

  switch (menuChoice)
  {
    case "start game":
      GameSettings gameSettings = SettingsScreen.Run(quizService);

      while (true)
      {
        gameSession.Initialize(gameSettings);

        GameResult result = GameScreen.Run(gameSession);

        PostGameScreen.SaveGame(gameSession.GetScore(), highScoreStorage, result);

        bool playAgain = PostGameScreen.PlayAgain();

        if (!playAgain) break;
      }
      break;

    case "high scores":
      HighScoreScreen.Run();
      break;

    case "exit":
      string confirmExit = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title($"Are you sure you want to exit?")
        .AddChoices([
          "(Y) - Yes",
          "(N) - No"])
        .HighlightStyle(new Style(foreground: Color.Yellow)));

      if (confirmExit == "(Y) - Yes") running = false;
      break;
  }

} while (running == true);

