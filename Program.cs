using Quiz;

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "questions.json");
IQuestionLoader questionLoader = new JsonQuestionLoader(filePath);
IHighScoreStorage highScoreStorage = new JsonHighScoreStorage("highScores.json");
QuizService quizService = new QuizService(questionLoader);

var gameSession = new GameSession(quizService, highScoreStorage);

try { Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight); } catch { };

bool running = true;

do
{
  string menuChoice = MenuScreen.Run();

  switch (menuChoice)
  {
    case "start game":
      GameSettings gameSettings = SettingsScreen.Run(quizService);
      GameScreen.Run();
      break;

    case "high scores":
      HighScoreScreen.Run();
      break;

    case "exit":
      running = false;
      break;
  }

} while (running == true);

