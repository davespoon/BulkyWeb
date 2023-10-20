﻿using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, DisplayOrder = 1, Name = "SciFi" },
            new Category { Id = 2, DisplayOrder = 2, Name = "Fantasy" },
            new Category { Id = 3, DisplayOrder = 3, Name = "Thriller" });
    }
}