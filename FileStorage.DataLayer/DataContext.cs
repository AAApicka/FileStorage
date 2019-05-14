using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Elinkx.FileStorage.DataLayer {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) {
        }
        //all tables in database:
        public DbSet<DbMetadata> dbMetadata { get; set; }
    }
}
