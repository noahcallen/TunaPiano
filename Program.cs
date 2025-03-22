
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tuna.Models;

var builder = WebApplication.CreateBuilder(args);


// Enable OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow passing DateTimes without timezone data
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Connect API to PostgreSQL Database
builder.Services.AddNpgsql<TunaPianoDbContext>(builder.Configuration["TunaPianoDbConnectionString"]);

// Set JSON serialization options (Prevents circular JSON errors)
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//  CORS Policy (Allow frontend at `localhost:3000`)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000") //  Adjusted origin for your frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

var app = builder.Build();

// ✅ Enable CORS Middleware BEFORE routing
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Get All Song
app.MapGet("/Song", async (TunaPianoDbContext db) =>
{
    return await db.Song
        .Include(s => s.Genres)
        .ToListAsync();
});

// Get a Single Song with associated genres and artist details
app.MapGet("/Song/{id}", async (TunaPianoDbContext db, int id) =>
{
    var song = await db.Song
        .Include(s => s.Artist)
        .Include(s => s.Genres)
        .SingleOrDefaultAsync(s => s.Id == id);

    return song == null ? Results.NotFound() : Results.Ok(song);
});

// Create a Song
app.MapPost("/Song", async (TunaPianoDbContext db, Song newSong) =>
{
    bool artistExists = await db.Artists.AnyAsync(a => a.Id == newSong.ArtistId);
    if (!artistExists)
    {
        return Results.BadRequest("Artist not found.");
    }

    db.Song.Add(newSong);
    await db.SaveChangesAsync();

    return Results.Created($"/Song/{newSong.Id}", newSong);
});

// Update a Song
app.MapPut("/Song/{id}", async (TunaPianoDbContext db, int id, Song song) =>
{
    var updatedSong = await db.Song.SingleOrDefaultAsync(s => s.Id == id);
    if (updatedSong == null)
    {
        return Results.NotFound();
    }

    updatedSong.Title = song.Title;
    updatedSong.ArtistId = song.ArtistId;
    updatedSong.Album = song.Album;
    updatedSong.Length = song.Length;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete a Song
app.MapDelete("/Song/{id}", async (TunaPianoDbContext db, int id) =>
{
    var song = await db.Song.SingleOrDefaultAsync(s => s.Id == id);
    if (song == null)
    {
        return Results.NotFound();
    }

    db.Song.Remove(song);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Artist Routes

// Get All Artists
app.MapGet("/artists", async (TunaPianoDbContext db) =>
{
    return await db.Artists.ToListAsync();
});

// Get a Single Artist with associated Song
app.MapGet("/artists/{id}", async (TunaPianoDbContext db, int id) =>
{
    var artist = await db.Artists
        .Include(a => a.Songs)
        .SingleOrDefaultAsync(a => a.Id == id);

    return artist == null ? Results.NotFound() : Results.Ok(artist);
});

// Create an Artist
app.MapPost("/artists", async (TunaPianoDbContext db, Artist newArtist) =>
{
    db.Artists.Add(newArtist);
    await db.SaveChangesAsync();
    return Results.Created($"/artists/{newArtist.Id}", newArtist);
});

// Update an Artist
app.MapPut("/artists/{id}", async (TunaPianoDbContext db, int id, Artist artist) =>
{
    var artistToUpdate = await db.Artists.SingleOrDefaultAsync(a => a.Id == id);
    if (artistToUpdate == null)
    {
        return Results.NotFound();
    }

    artistToUpdate.Name = artist.Name;
    artistToUpdate.Age = artist.Age;
    artistToUpdate.Bio = artist.Bio;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete an Artist
app.MapDelete("/artists/{id}", async (TunaPianoDbContext db, int id) =>
{
    var artist = await db.Artists.SingleOrDefaultAsync(a => a.Id == id);
    if (artist == null)
    {
        return Results.NotFound();
    }

    db.Artists.Remove(artist);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Genre Routes

// Get All Genres
app.MapGet("/genres", async (TunaPianoDbContext db) =>
{
    return await db.Genre.ToListAsync();
});

// Get a Single Genre with Its Associated Song
app.MapGet("/genres/{id}", async (TunaPianoDbContext db, int id) =>
{
    var genre = await db.Genre
        .Include(g => g.Songs)
        .SingleOrDefaultAsync(g => g.Id == id);

    return genre == null ? Results.NotFound() : Results.Ok(genre);
});



// Create a Genre
app.MapPost("/genres", async (TunaPianoDbContext db, Genre newGenre) =>
{
    db.Genre.Add(newGenre);
    await db.SaveChangesAsync();
    return Results.Created($"/genres/{newGenre.Id}", newGenre);
});

// Update a Genre
app.MapPut("/genres/{id}", async (TunaPianoDbContext db, int id, Genre genre) =>
{
    var genreToUpdate = await db.Genre.SingleOrDefaultAsync(g => g.Id == id);
    if (genreToUpdate == null)
    {
        return Results.NotFound();
    }

    genreToUpdate.Description = genre.Description;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete a Genre
app.MapDelete("/genres/{id}", async (TunaPianoDbContext db, int id) =>
{
    var genre = await db.Genre.SingleOrDefaultAsync(g => g.Id == id);
    if (genre == null)
    {
        return Results.NotFound();
    }

    db.Genre.Remove(genre);
    await db.SaveChangesAsync();
    return Results.NoContent();
});



app.Run();