using EducationalHelp.Core.Entities;
using EducationalHelp.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EducationalHelp.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Subject> Subjects { get; }

       

        public ApplicationContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EfMapManager.MappingAllData(modelBuilder);
            DevSeed.Seed(modelBuilder);
        }
    }
}