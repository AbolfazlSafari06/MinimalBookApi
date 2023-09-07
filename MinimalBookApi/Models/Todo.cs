namespace MinimalBookApi.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public bool IsDone { get; set; } 

    public Todo(int id, string title, string description, DateTime? finishedDate)
    {
        Id = id;
        Title = title;
        Description = description;
        CreateDate = DateTime.Now;
        FinishedDate = finishedDate;
        IsDone = false;
    }

    public static TodoDto TodoToDto(Todo todo)
    {
        return new TodoDto()
        {
            CreateDateFormatted = todo.CreateDate.ToString(),
            Description = todo.Description,
            FinishedDateFormatted = todo.FinishedDate.ToString(),
            Id = todo.Id,
            Title = todo.Title,
            IsDone = todo.IsDone,
        };
    }

    public void ChangeStatus()
    {
        if (IsDone)
        {
            this.FinishedDate = null;
        }
        else
        {
            this.FinishedDate = DateTime.Now;
        }
        this.IsDone = !this.IsDone;
    }
}