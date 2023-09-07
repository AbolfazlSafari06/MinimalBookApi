using MinimalBookApi.Endpoints; 
using FluentValidation;
using MinimalBookApi.Validator;
using MinimalBookApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateTodoDto));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
 
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGroup("/todos")
    .WithTags("todos") 
    .TodoEndpoints();

app.Run();
