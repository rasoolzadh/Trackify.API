// File: Trackify.API/Program.cs
using Microsoft.EntityFrameworkCore;
using Trackify.API.Data;
using Trackify.API.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services to allow the MAUI app to connect
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

// 2. Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Use the CORS policy


// 3. Define API Endpoints
app.MapGet("/api/transactions", async (AppDbContext db) =>
    await db.Transactions.OrderByDescending(t => t.Date).ToListAsync());

app.MapPost("/api/transactions", async (AppDbContext db, Transaction transaction) =>
{
    await db.Transactions.AddAsync(transaction);
    await db.SaveChangesAsync();
    return Results.Created($"/api/transactions/{transaction.Id}", transaction);
});

app.MapDelete("/api/transactions/{id}", async (AppDbContext db, int id) =>
{
    var transaction = await db.Transactions.FindAsync(id);
    if (transaction is null) return Results.NotFound();

    db.Transactions.Remove(transaction);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// A simple endpoint to trigger database migration on startup (for development)
app.MapGet("/migrate", async (AppDbContext db) => {
    await db.Database.MigrateAsync();
    return Results.Ok("Database migration attempted.");
});


app.Run();