using BadamApplicationAndForum.Data.Models;
using ExamCards.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BadamApplicationAndForum.Data.Contexts
{
    public class ApplicationDatabaseContext:IdentityDbContext<PanelUser>
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
        {

        }
        public DbSet<PanelUser> PanelUsers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<MessageReply> MessageReplies { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
    }
}
