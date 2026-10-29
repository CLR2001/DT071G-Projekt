namespace Quiz;

public record HighScore
{
  public required string Name { get; init; }
  public required int Score { get; init; }
  public required DateOnly Date { get; init; }
  public required string Difficulty { get; init; } 
}