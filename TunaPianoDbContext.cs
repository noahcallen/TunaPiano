using Microsoft.EntityFrameworkCore;
using Tuna.Models;

public class TunaPianoDbContext : DbContext
{
    public DbSet<Song> Song { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Genre> Genre { get; set; } // Change `Genres` to `Genre`

    // Constructor with DbContextOptions
    public TunaPianoDbContext(DbContextOptions<TunaPianoDbContext> options) : base(options) // Renamed 'context' to 'options'
    {
    }

    // OnModelCreating to seed the data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ✅ SEED THE MANY-TO-MANY RELATIONSHIP (SongGenre)
        modelBuilder.Entity<Song>()
            .HasMany(s => s.Genres)
            .WithMany(g => g.Songs)
            .UsingEntity(j => j.HasData(
                new { SongsId = 1, GenresId = 2 }, // "Good News" -> "Hip Hop"
                new { SongsId = 1, GenresId = 5 }, // "Good News" -> "Pop"
                new { SongsId = 2, GenresId = 2 }, // "Hometown" -> "Rock"
                new { SongsId = 2, GenresId = 5 }, // "Hometown" -> "Alternative"
                new { SongsId = 3, GenresId = 2 }, // "Welcome to the Black Parade" -> "Rock"
                new { SongsId = 3, GenresId = 3 }, // "Welcome to the Black Parade" -> "Emo"
                new { SongsId = 4, GenresId = 1 }, // "Clever" -> "Indie"
                new { SongsId = 4, GenresId = 5 }  // "Clever" -> "Pop"
            ));

        // Seed Data
        modelBuilder.Entity<Artist>().HasData(new Artist[]
        {
            new Artist { Id = 1, Name = "Mac Miller", Age = 26, Bio = "A talented rapper known for his introspective lyrics and smooth beats." },
            new Artist { Id = 2, Name = "Cleopatrick", Age = 28, Bio = "A Canadian rock duo blending gritty, raw guitar sounds with emotionally charged lyrics." },
            new Artist { Id = 3, Name = "My Chemical Romance", Age = 20, Bio = "An iconic emo rock band known for their dramatic style and powerful anthems." }
        });

        modelBuilder.Entity<Song>().HasData(new Song[]
        {
            new Song { Id = 1, Title = "Good News", ArtistId = 1, Album = "Circles", Length = 5.39m },
            new Song { Id = 2, Title = "Hometown", ArtistId = 2, Album = "Bummer", Length = 4.12m },
            new Song { Id = 3, Title = "Welcome to the Black Parade", ArtistId = 3, Album = "The Black Parade", Length = 5.11m },
            new Song { Id = 4, Title = "Clever", ArtistId = 1, Album = "Swimming", Length = 4.14m }
        });

        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new Genre { Id = 1, Description = "Indie" },
            new Genre { Id = 2, Description = "Rock" },
            new Genre { Id = 3, Description = "Emo" },
            new Genre { Id = 4, Description = "Alternative" },
            new Genre { Id = 5, Description = "Pop" },
            new Genre { Id = 6, Description = "Hip Hop" }
        });
    }
}
