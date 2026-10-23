namespace Quiz;

public record GameSettings
{
  public required List<string> SelectedCategories { get; init; }
  public required string SelectedDifficulty { get; init; }
}