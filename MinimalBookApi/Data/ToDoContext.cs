using MinimalBookApi.Models;

namespace MinimalBookApi.Data;

public class ToDoContext
{
    public List<Todo> Todos { get; set; }

    public ToDoContext()
    {
        Todos = new List<Todo>()
        {
            new Todo(1,"todo1","description",DateTime.Now)
        };
    }

    public void AddTodo(Todo todo)
    {
        Todos.Add(todo);
    }

    public Todo? GetById(int id)
    {
        return Todos.Find(x => x.Id.Equals(id));
    }

    public int GetNewId()
    {
        return Todos.Count + 1;
    }

    public void Delete(Todo todo)
    {
        Todos.Remove(todo);
    }

    public void Update(UpdateTodoDto command)
    {
        var todo = Todos.First(x => x.Id.Equals(command.Id));
        if (!string.IsNullOrEmpty(command.Description)) todo.Description = command.Description;
        if (!string.IsNullOrEmpty(command.Title)) todo.Title = command.Title;
    }
}