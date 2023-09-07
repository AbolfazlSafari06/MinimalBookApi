using FluentValidation;
using MinimalBookApi.Data;
using MinimalBookApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MinimalBookApi.Endpoints;

public static class ToDoEndpoint
{
    private static readonly ToDoContext _context = new();

    public static void TodoEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/", () => TypedResults.Ok(_context.Todos.Select(Todo.TodoToDto).ToList())).WithName("TEst");

        app.MapPost("/", async (IValidator<CreateTodoDto> validator, CreateTodoDto command) =>
        {
            var valid = await validator.ValidateAsync(command);
            if (!valid.IsValid)
            {
                return Results.ValidationProblem(valid.ToDictionary());
            }

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

        app.MapPut("/", async (IValidator <UpdateTodoDto> validator,UpdateTodoDto command) =>
        {
            var valid = await validator.ValidateAsync(command);
            if (!valid.IsValid)
            {
                return Results.ValidationProblem(valid.ToDictionary());
            }


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