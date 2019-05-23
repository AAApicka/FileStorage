using System;
using Microsoft.EntityFrameworkCore;
using Elinkx.FileStorage.DataLayer.Entities;

namespace Elinkx.FileStorage.DataLayer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
      : base(options)
        {

        }
        public DbSet<Metadata> Metadata { get; set; }
        public DbSet<FileVersion> FileVersion { get; set; }
        public DbSet<FileContent> FileContent { get; set; }
    }
}
