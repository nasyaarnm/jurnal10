using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using tpmodul10_1302223148;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataBase>(opt => opt.UseInMemoryDatabase("mahasiswa"));
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

// configure swagger tutorial di https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio-code
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/", () =>
{
    return Results.Redirect("/swagger");
});

app.MapGet("/mahasiswa", async (DataBase db) =>
{
    var mhs = new Mahasiswa[]
    {
        new Mahasiswa { Id = 0, Nama = "Nasya Kirana Marendra", Nim = "1302223148"},
        new Mahasiswa { Id = 1, Nama = "Zabrina Virgie", Nim = "1302223055"},
        new Mahasiswa { Id = 2, Nama = "Dara Sheiba M.C", Nim = "1302223075"},
        new Mahasiswa { Id = 3, Nama = "M Tsaqif Zayyan", Nim = "1302220141"},
        new Mahasiswa { Id = 4, Nama = "M Arifin Ilham", Nim = "1302223061"},
        new Mahasiswa { Id = 5, Nama = "Rafie Aydin Ihsan", Nim = "1302220065"},
    };

    await db.SaveChangesAsync();
    return Results.Ok(await db.mhs.ToListAsync());
});

app.MapGet("/mahasiswa/{id}", async (DataBase db, int id) =>
{
    var mhs = await db.mhs.FindAsync(id);
    if (mhs == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(mhs);
});

app.MapPost("/mahasiswa", async (DataBase db, Mahasiswa mhs) =>
{
    Console.WriteLine(mhs);
    db.mhs.Add(mhs);
    await db.SaveChangesAsync();
    return Results.Created($"/mahasiswa/{mhs.Id}", mhs);
});

app.MapPut("/mahasiswa/{id}", async (DataBase db, int id, Mahasiswa mhs) =>
{
    if (id != mhs.Id)
    {
        return Results.BadRequest();
    }
    db.Entry(mhs).State = EntityState.Modified;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/mahasiswa/{id}", async (DataBase db, int id) =>
{
    var mhs = await db.mhs.FindAsync(id);
    if (mhs == null)
    {
        return Results.NotFound();
    }
    db.mhs.Remove(mhs);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();