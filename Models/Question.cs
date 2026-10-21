using System.Text.Json.Serialization;

namespace Quiz
{
  public class Question
  {
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("difficulty")]
    public required string Difficulty { get; set; }

    [JsonPropertyName("category")]
    public required string Category { get; set; }

    [JsonPropertyName("question")]
    public required string QuestionText { get; set; }

    [JsonPropertyName("correct_answer")]
    public required string CorrectAnswer { get; set; }

    [JsonPropertyName("incorrect_answers")]
    public required List<string> IncorrectAnswers { get; set; }

    public List<string> GetAllAnswers()
    {
      var allAnswers = new List<string>(IncorrectAnswers);
      allAnswers.Add(CorrectAnswer);

      return allAnswers.OrderBy(_ => Guid.NewGuid()).ToList();
    }
  }
}