using DatabaseContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext
{
    public class CrowDoDbContext : DbContext
    {
        public CrowDoDbContext(DbContextOptions<CrowDoDbContext> options) : base(options) { }
        public CrowDoDbContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PledgeOptions> PledgeOptions { get; set; }
        public DbSet<Pledges> Pledges { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectCategories> ProjectCategories { get; set; }
        public DbSet<ProjectInfo> ProjectInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Comment>().HasOne(i => i.User).WithMany(c => c.Mycomments)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pledges>().HasKey(p => new { p.PledgeOptionId, p.UserId });

            modelBuilder.Entity<ProjectCategories>().HasKey(p => new { p.ProjectId, p.CategoryId });

            modelBuilder.Entity<Project>().HasOne(i => i.User).WithMany(c => c.Projects)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder
//        .UseSqlServer(@"Server=localhost;Database=CrowDoDb; Trusted_Connection = True; ConnectRetryCount = 0;");
//}
