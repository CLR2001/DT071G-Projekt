namespace Quiz;

public class GameSession(QuizService quizService)
{

  private List<Question> _questions = [];
  private int _currentQuestion = 1;
  private int _score = 0;
  
  public void Initialize(GameSettings settings)
  {
    _questions = quizService.GetFilteredQuestions(settings.SelectedCategories, settings.SelectedDifficulty);

  }

  public bool AnyQuestionsLeft() => _currentQuestion <= _questions.Count;

  public Question GetQuestion()
  {
    Question currentQuestion = _questions[Random.Shared.Next(_questions.Count)];
    _questions.Remove(currentQuestion);
    return currentQuestion;
  }

  public bool SubmitAnswer(Question question, string answer)
  {
    _currentQuestion++;

    if (question.CorrectAnswer.Equals(answer, StringComparison.OrdinalIgnoreCase))
    {
      _score++;
      return true;
    }
    return false;
  }

  public int GetScore() => _score;
  public int GetCurrentQuestionIndex() => _currentQuestion;
}