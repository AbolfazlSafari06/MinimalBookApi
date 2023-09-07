namespace MinimalBookApi.Models;

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CreateDateFormatted { get; set; }
    public string? FinishedDateFormatted { get; set; }
    public bool IsDone { get; set; }
}