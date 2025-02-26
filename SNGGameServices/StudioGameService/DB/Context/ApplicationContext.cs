using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;

namespace StudioGameService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base(dbContext)
        {
        }
        DbSet<Game> Games { get; set; }
        DbSet<GameLibrary> GameLibraries { get; set; }
        DbSet<GameSelectedGenre> GameSelectedGenres { get; set; }
        DbSet<GameSelectedTag> GameSelectedTags { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<JobInStudio> JobsInStudio { get; set; }
        DbSet<Studio> Studios { get; set; }
    }
}
