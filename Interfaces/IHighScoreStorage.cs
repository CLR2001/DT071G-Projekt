namespace Quiz
{
  public interface IHighScoreStorage
  {
    List<HighScore> Load();
    void Save(List<HighScore> highScores);
  }
}