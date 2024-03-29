namespace MyDiaryApp.Models;

public class Entry
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}