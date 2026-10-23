namespace Quiz;

public record HighScore
{
  public required string Name { get; init; }
  public required int Score { get; init; }
  public required DateOnly DateSubmitted { get; init; }
}