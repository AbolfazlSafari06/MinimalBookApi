using MinimalBookApi.Data;
using MinimalBookApi.Models;

namespace MinimalBookApi.Endpoints;

public static class ToDoEndpoint
{
    private static readonly ToDoContext _context = new();

    public static void TodoEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/", () => TypedResults.Ok(_context.Todos.Select(Todo.TodoToDto).ToList())).WithName("TEst");

        app.MapPost("/", (CreateTodoDto command) =>
        {
            if (command is null)
            {
                return Results.BadRequest("درخواست اشتباه است");
            }

            var newId = _context.GetNewId();

            var newTodo = new Todo(newId, command.Title, command.Description, null);

            _context.AddTodo(newTodo);

            return TypedResults.Ok();
        });

        app.MapGet("/{id:int}", (int id) =>
        {
            var todo = _context.GetById(id);
            if (todo is null)
            {
                return Results.NotFound("موجود نمیباشد");
            }

            return TypedResults.Ok(Todo.TodoToDto(todo));
        });

        app.MapDelete("/{id:int}", (int id) =>
        {
            var todo = _context.GetById(id);

            if (todo is null)
            {
                return Results.NotFound("موجود نمیباشد");
            }

            _context.Delete(todo);

            return TypedResults.Ok();
        });

        app.MapPut("/", (UpdateTodoDto command) =>
        {
            var todo = _context.GetById(command.Id);

            if (todo is null)
            {
                return Results.BadRequest("موجود نمیباشد");
            }

            _context.Update(command);

            return TypedResults.Ok();
        });

        app.MapPut("/changeStatus/{id:int}", (int id) =>
        {
            var todo = _context.GetById(id);

            if (todo is null)
            {
                return Results.BadRequest("موجود نمیباشد");
            }

            todo.ChangeStatus();

            return TypedResults.Ok();
        });
    }
}