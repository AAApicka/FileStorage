using System;
using Microsoft.EntityFrameworkCore;
using Elinkx.FileStorage.Models;

namespace Elinkx.FileStorage.DataLayer
{
    public class DataContext : DbContext
    {
        string comSetup;

        public DataContext(string comSetup)
        {
            this.comSetup = comSetup;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(comSetup);
        }

        public DbSet<Metadata> Metadata { get; set; }
        public DbSet<FileVersion> FileVersion { get; set; }
        public DbSet<FileContent> FileContent { get; set; }
    }
}
