using Microsoft.AspNetCore.Mvc;
using MiniTodo.Data;
using MiniTodo.Models;
using MiniTodo.ViewModel;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/todos", (AppDbContext context) => 
{
    var todo = context.Todos.ToList();
    return Results.Ok(todo); 
}).Produces<Todo>();

app.MapGet("v1/todos/id:Guid", (AppDbContext context, Guid id) => 
{
    var todo = context.Todos.FirstOrDefault(x => x.Id == id);

    if(todo is null)

        return Results.NotFound();
    return Results.Ok(todo); 
}).Produces<Todo>();

app.MapPost("v1/todos", (
    AppDbContext context,
    CreateTodoViewModel model) => {
    
    var todo = model.MapTo();
    if(!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"v1/todos/{todo.Id}", todo);
    }).Produces<Todo>();

app.MapPut("v1/todos/id:Guid", (
    AppDbContext context,
    CreateTodoViewModel model,
    Guid id) => {
        
        if(!model.IsValid)
            return Results.BadRequest();

        var todo = context.Todos.FirstOrDefault(x => x.Id == id);

        if(todo is null)
            return Results.NotFound();

        todo.Title = model.Title;

        context.Todos.Update(todo);
        context.SaveChanges();

        return Results.Ok(todo);

    }).Produces<Todo>();

app.MapDelete("v1/todos/{id:Guid}", (AppDbContext context, Guid id) => 
{
    var todo = context.Todos.FirstOrDefault(x => x.Id == id);

    if(todo == null)

        return Results.NotFound();

    context.Todos.Remove(todo);
    context.SaveChanges();

    return Results.Ok(todo);

}).Produces<Todo>();




app.Run();
