﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CrowDoCore.Models;

namespace CrowDoCore
{
    public class CrowDoDbContext : DbContext
    {
        //public CrowDoDbContext(DbContextOptions options) : base(options)//erwtisi
        //{
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=CrowDoDb; Trusted_Connection = True; ConnectRetryCount = 0;");
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PledgeOptions> PledgeOptions { get; set; }
        public DbSet<Pledges> Pledges { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectCategories> ProjectCategories { get; set; }
        public DbSet<ProjectInfo> ProjectInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Comment>().HasKey(c => new { c.ProjectId, c.UserId });
            modelBuilder.Entity<Pledges>().HasKey(p => new { p.PledgeOptionId, p.UserId });
            modelBuilder.Entity<ProjectCategories>().HasKey(p => new { p.ProjectId, p.CategoryId });
        }
    }

}
//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder
//        .UseSqlServer(@"Server=localhost;Database=CrowDoDb; Trusted_Connection = True; ConnectRetryCount = 0;");
//}