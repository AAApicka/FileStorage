using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Elinkx.FileStorage.DataLayer {
    public class DataContext : DbContext {
        string comSetup;

        public DataContext(string comSetup) {
            this.comSetup = comSetup;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            Console.WriteLine("connected");
            optionsBuilder.UseSqlServer(comSetup);
        }

        //all tables in database:
        public DbSet<DbMetadata> dbMetadata { get; set; }
    }
}
