using Quiz;

IQuestionLoader questionLoader = new JsonQuestionLoader("questions.json");
IHighScoreStorage highScoreStorage = new JsonHighScoreStorage("highScores.json");
QuizService quizService = new QuizService(questionLoader);

var gameSession = new GameSession(quizService, highScoreStorage);

try { Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight); } catch { };
string choice = MenuScreen.Run();