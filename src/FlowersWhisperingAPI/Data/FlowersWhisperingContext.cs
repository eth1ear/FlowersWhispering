using Microsoft.EntityFrameworkCore;
using FlowersWhisperingAPI.Models;

namespace FlowersWhisperingAPI.Data
{
    public class FlowersWhisperingContext : DbContext
    {
        public FlowersWhisperingContext(DbContextOptions<FlowersWhisperingContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MedicinalHerb> MedicinalHerbs { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserPoint> UserPoints { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<PlantRegionSuitability> PlantRegionSuitabilities { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<CareLog> CareLogs { get; set; }
        public DbSet<CareReminder> CareReminders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
