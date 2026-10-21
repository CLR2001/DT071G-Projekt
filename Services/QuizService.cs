namespace Quiz
{
  public class QuizService(IQuestionLoader questionLoader)
  {
    public List<string> GetCategories()
    {
      var questions = questionLoader.LoadQuestions();

      return [.. questions
          .Select(q => q.Category)
          .Where(c => !string.IsNullOrEmpty(c))
          .Select(cleanCategory)
          .Distinct()
          .OrderBy(c => c)];
    }

    public List<Question> GetQuestionsForCategories(List<string> selectedCategories)
    {
      var allQuestions = questionLoader.LoadQuestions();

      if (selectedCategories.Contains("All"))
      {
        return allQuestions;
      }

      return [.. allQuestions
            .Where(q => selectedCategories.Contains(cleanCategory(q.Category)))];
    }

    private string cleanCategory(string category)
    {
      if (string.IsNullOrEmpty(category)) return string.Empty;

      return category.Contains(':')
          ? category.Split(':').Last().Trim()
          : category.Trim();
    }
  }
}