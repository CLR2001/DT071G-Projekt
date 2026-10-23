using System.Text.Json.Serialization;

namespace Quiz;

public record Question
{
  [JsonPropertyName("type")]
  public required string Type { get; init; }

  [JsonPropertyName("difficulty")]
  public required string Difficulty { get; init; }

  [JsonPropertyName("category")]
  public required string Category { get; init; }

  [JsonPropertyName("question")]
  public required string QuestionText { get; init; }

  [JsonPropertyName("correct_answer")]
  public required string CorrectAnswer { get; init; }

  [JsonPropertyName("incorrect_answers")]
  public required List<string> IncorrectAnswers { get; init; }

  public List<string> GetAllAnswers()
  {
    var allAnswers = new List<string>(IncorrectAnswers);
    allAnswers.Add(CorrectAnswer);

    return allAnswers.OrderBy(_ => Guid.NewGuid()).ToList();
  }
}