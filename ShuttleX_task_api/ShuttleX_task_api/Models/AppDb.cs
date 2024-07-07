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
            // User
            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedChats)
                .WithOne(c => c.CreatedByUser)
                .HasForeignKey(c => c.CreatedByUserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedMessages)
                .WithOne(m => m.CreatedByUser)
                .HasForeignKey(m => m.CreatedByUserId);

            //  Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.CreatedByUser)
                .WithMany(u => u.CreatedMessages)
                .HasForeignKey(m => m.CreatedByUserId);

            // Chat
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedChats)
                .HasForeignKey(c => c.CreatedByUserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
