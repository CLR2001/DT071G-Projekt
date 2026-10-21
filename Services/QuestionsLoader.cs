using System.Text.Json;

namespace Quiz
{
  public class QuestionsLoader
  {
    private readonly string _filePath;

    public QuestionsLoader(string filePath)
    {
      _filePath = filePath;
    }

    public List<Question> LoadQuestions()
    {
      if (!File.Exists(_filePath)) return new List<Question>();

      string jsonString = File.ReadAllText(_filePath);

      return JsonSerializer.Deserialize<List<Question>>(jsonString) ?? new List<Question>();
    }
  }
}