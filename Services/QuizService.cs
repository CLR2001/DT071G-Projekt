/*
  QuizService.cs
  Handles logic, filtering, and loading of quiz questions.
*/

using System.Net;

namespace Quiz;

public class QuizService(IQuestionLoader questionLoader)
{
  public List<string> GetCategories()
  {
    var questions = questionLoader.LoadQuestions();

    return [.. questions
        .Select(q => q.Category)
        .Where(c => !string.IsNullOrEmpty(c))
        .Select(CleanCategory)
        .Distinct()
        .OrderBy(c => c)];
  }

  public List<Question> GetFilteredQuestions(List<string> selectedCategories, string selectedDifficulty)
  {
    var allQuestions = questionLoader.LoadQuestions();
    IEnumerable<Question> filteredQuestions = allQuestions; 

    if (!selectedCategories.Contains("All"))
    {
      filteredQuestions = filteredQuestions.Where(q => selectedCategories.Contains(CleanCategory(q.Category)));
    }

    if (selectedDifficulty != "All")
    {
      filteredQuestions = filteredQuestions.Where(q => q.Difficulty.Equals(selectedDifficulty, StringComparison.OrdinalIgnoreCase));
    }
    return [.. filteredQuestions.Where(q => q.Type.Equals("multiple", StringComparison.OrdinalIgnoreCase))];
  }

  private string CleanCategory(string category)
  {
    if (string.IsNullOrEmpty(category)) return string.Empty;

    return WebUtility.HtmlDecode(
            category.Contains(':')
            ? category.Split(':').Last().Trim()
            : category.Trim()
        );
  }
}