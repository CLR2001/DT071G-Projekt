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
      gameSession.Initialize(gameSettings);

      GameScreen.Run(gameSession);

      int finalScore = gameSession.GetScore();
      List<HighScore> currentHighScores = highScoreStorage.Load();

      string confirmed = AnsiConsole.Prompt(
       new SelectionPrompt<string>()
       .Title($"Do you want to save your score? ({finalScore})")
       .AddChoices([
         "(Y) - Yes",
         "(N) - No"])
       .HighlightStyle(new Style(foreground: Color.Yellow)));

      if (confirmed == "(Y) - Yes")
      {
        string playerName = AnsiConsole.Prompt(
          new TextPrompt<string>("Enter your name: ")
          .Validate(name =>
          {
            string trimmedName = name.Trim();

            if (trimmedName.Length < 3)
            {
              return ValidationResult.Error("[red]Your name must contain at least 3 symbols. Try again![/]");
            }

            if (trimmedName.Length > 15)
            {
              return ValidationResult.Error("[red]Your name can't contain more than 15 symbols. Try again![/]");
            }

            return ValidationResult.Success();
          })
         );

        HighScore newHighScore = new(playerName, finalScore, DateOnly.FromDateTime(DateTime.Now));
        currentHighScores.Append();
        
      }
      break;

    case "high scores":
      HighScoreScreen.Run();
      break;

    case "exit":
      string confirmed = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title($"Are you sure you want to exit?")
        .AddChoices([
          "(Y) - Yes",
          "(N) - No"])
        .HighlightStyle(new Style(foreground: Color.Yellow)));
      if (confirmed == "(Y) - Yes") running = false;
      break;
  }

} while (running == true);

