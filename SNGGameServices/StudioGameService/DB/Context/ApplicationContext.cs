using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;

namespace StudioGameService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        DbSet<Game> Games { get; set; }
        DbSet<GameLibrary> GameLibraries { get; set; }
        DbSet<GameSelectedGenre> GameSelectedGenres { get; set; }
        DbSet<GameSelectedTag> GameSelectedTags { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Game GST Tag
            modelBuilder
                .Entity<GameSelectedTag>()
                .HasOne(gst => gst.Game)
                .WithMany(g => g.Tags)
                .HasForeignKey(gst => gst.GameId);

            modelBuilder
                .Entity<GameSelectedTag>()
                .HasOne(gst => gst.Tag)
                .WithMany(t => t.Games)
                .HasForeignKey(gst => gst.TagId);

            // Game GST Genre
            modelBuilder
                .Entity<GameSelectedGenre>()
                .HasOne(gsg => gsg.Game)
                .WithMany(g => g.Genres)
                .HasForeignKey(gsg => gsg.GameId);

            modelBuilder
                .Entity<GameSelectedGenre>()
                .HasOne(gsg => gsg.Genre)
                .WithMany(genre => genre.Games)
                .HasForeignKey(gsg => gsg.GenreId);

            // GameLib -> Game
            modelBuilder
                .Entity<GameLibrary>()
                .HasOne(gl => gl.Game)
                .WithMany(g => g.GameLibrarys)
                .HasForeignKey(gl => gl.GameId);

            // Game -> Studio
            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.Studio)
                .WithMany(s => s.Games)
                .HasForeignKey(g => g.StudioId);
        }
    }
}
