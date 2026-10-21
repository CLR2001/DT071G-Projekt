using System.Text.Json;

namespace Quiz
{
  public class JsonHighScoreStorage(string filePath) : IHighScoreStorage
  {
    public List<HighScore> Load()
    {
      if (!File.Exists(filePath)) return [];

      string jsonString = File.ReadAllText(filePath);

      return JsonSerializer.Deserialize<List<HighScore>>(jsonString) ?? [];

    }

    public void Save(List<HighScore> highScores)
    {
      var options = new JsonSerializerOptions { WriteIndented = true };

      string jsonString = JsonSerializer.Serialize(highScores, options);

      File.WriteAllText(filePath, jsonString);
    }
  }
}