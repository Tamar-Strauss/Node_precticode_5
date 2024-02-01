using System.Xml.Serialization;
//import Item from './Item.cs';

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Item", () => "This is a GET");

// app.MapPost("/Item", (Item Item) => AddItem(Item));

// app.MapPut("/Item/{ItemId}", (int ItemId, Item updatedItem) => UpdateItem(ItemId, updatedItem));

// app.MapDelete("/Item/{ItemId}", (int ItemId) => DeleteItem(ItemId));

app.MapGet("/", () => "Hello World!");

app.Run();

