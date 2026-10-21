namespace Quiz
{
  public class HighScore
  {
    public required string Name { get;  set; }
    public required int Score { get; set; }
    public required DateTime DateSubmitted { get; set; }
  }
}