
using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title ="http://localhost:7271", Version = "v1" });
// });
// Add services to the container.
string dbConn = "server=localhost;user=root;password=1234;database=todolist";

builder.Services.AddDbContext<ToDoDbContext>(options =>
{
    options.UseMySql(dbConn, ServerVersion.AutoDetect(dbConn));
});

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors();
// app.UseSwagger();
// app.UseSwaggerUI(options =>
// {
//     options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//     options.RoutePrefix = string.Empty;
// });

// Middleware to handle database context in each request
app.Use(async (context, next) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
    context.Items["DbContext"] = dbContext;
    await next.Invoke();
});

app.MapGet("/", (HttpContext context) =>
{
    var dbContext = context.Items["DbContext"] as ToDoDbContext;
    var items = dbContext.GetAllItems();
    return Results.Json(items);
});
app.MapGet("/Items", (HttpContext context) =>
{
    var dbContext = context.Items["DbContext"] as ToDoDbContext;
    var items = dbContext.Items.ToList();
    return Results.Ok(items);
});

app.MapPost("/Items", async (HttpContext context) =>
{
    var dbContext = context.Items["DbContext"] as ToDoDbContext;
    var item = await context.Request.ReadFromJsonAsync<Item>();
    if (item != null)
    {
        dbContext.Items.Add(item);
        dbContext.SaveChanges();
        return Results.Created($"/Items/{item.Id}", item);
    }
    return Results.BadRequest("Invalid item data");
});

app.MapPut("/Items/{ItemId}", async (HttpContext context, int ItemId) =>
{
    var dbContext = context.Items["DbContext"] as ToDoDbContext;
    var isComplete = await context.Request.ReadFromJsonAsync<Boolean>();
    var item = dbContext.Items.Find(ItemId);
    
    if (item == null || isComplete == null)
        return Results.NotFound();

    item.Name = item.Name;
    item.IsComplate = isComplete;
    dbContext.SaveChanges();
    return Results.Ok(item);
});

app.MapDelete("/Items/{ItemId}", (HttpContext context, int ItemId) =>
{
    var dbContext = context.Items["DbContext"] as ToDoDbContext;
    var item = dbContext.Items.Find(ItemId);
    
    if (item == null)
        return Results.NotFound();

    dbContext.Items.Remove(item);
    dbContext.SaveChanges();

    return Results.NoContent();
});

app.Run();





// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.DependencyInjection;
// // using Microsoft.EntityFrameworkCore;

// using TodoApi;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// string dbConn = "server=localhost;user=root;password=1234;database=todolist";

// builder.Services.AddDbContext<ToDoDbContext>(options =>{
//      var connetionString = dbConn;
//             options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));});

// var app = builder.Build();


// ToDoDbContext toDoDbContext = new ToDoDbContext();
// // app.MapGet("/", () => "Hello World!");


// app.MapGet("/", (ToDoDbContext dbContext) =>
// {
//     var Items = dbContext.GetAllItems();
//     return Results.Json(Items);
// });

// var ItemsDbContext = app.Services.GetRequiredService<ToDoDbContext>();

// app.MapGet("/Items", () =>
// {
//     var Items = ItemsDbContext.Items.ToList();
//     return Results.Ok(Items);
// });

// app.MapPost("/Items", (Item Item) =>
// {
//     ItemsDbContext.Items.Add(Item);
//     ItemsDbContext.SaveChanges();
//     return Results.Created($"/Items/{Item.Id}", Item);
// });

// app.MapPut("/Items/{ItemId}", (int ItemId, Item updatedItem) =>
// {
//     var Item = ItemsDbContext.Items.Find(ItemId);
//     if (Item == null)
//         return Results.NotFound();

//     Item.Name = updatedItem.Name;
//     Item.IsComplate = updatedItem.IsComplate;

//     ItemsDbContext.SaveChanges();

//     return Results.Ok(Item);
// });

// app.MapDelete("/Items/{ItemId}", (int ItemId) =>
// {
//     var Item = ItemsDbContext.Items.Find(ItemId);
//     if (Item == null)
//         return Results.NotFound();

//     ItemsDbContext.Items.Remove(Item);
//     ItemsDbContext.SaveChanges();

//     return Results.NoContent();
// });

// app.Run();


