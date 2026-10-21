using System.Text.Json;

namespace Quiz
{
  public class JsonQuestionLoader(string filePath) : IQuestionLoader
  {
    public List<Question> LoadQuestions()
    {
      if (!File.Exists(filePath)) return [];

      string jsonString = File.ReadAllText(filePath);

      return JsonSerializer.Deserialize<List<Question>>(jsonString) ?? [];
    }
  }
}