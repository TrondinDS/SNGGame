using Microsoft.EntityFrameworkCore;
using UserActivityService.DB.Models;

namespace UserActivityService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base(dbContext)
        {
        }
        DbSet<Comment> Comments { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<UserReaction> UserReactions { get; set; }

    }
}
