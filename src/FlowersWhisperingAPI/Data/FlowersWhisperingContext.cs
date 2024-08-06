using Microsoft.EntityFrameworkCore;
using FlowersWhisperingAPI.Models;
using FlowersWhisperingAPI.Extensions;
namespace FlowersWhisperingAPI.Data
{
    public class FlowersWhisperingContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyShortNames();
            modelBuilder.Entity<User>()
                    .Property(u => u.UserId)
                    .HasDefaultValueSql("user_id_seq.NEXTVAL");
        }

        public FlowersWhisperingContext(DbContextOptions<FlowersWhisperingContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                          .EnableSensitiveDataLogging();
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
