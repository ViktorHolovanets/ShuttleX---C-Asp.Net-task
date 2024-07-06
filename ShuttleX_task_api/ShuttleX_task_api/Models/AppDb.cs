using Microsoft.EntityFrameworkCore;

namespace ShuttleX_task_api.Models
{
    public class AppDb : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDb(DbContextOptions<AppDb> options)
            : base(options) => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);
        }
    }
}
