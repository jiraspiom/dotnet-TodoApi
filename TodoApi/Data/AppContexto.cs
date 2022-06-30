using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class AppContexto : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder.UseSqlite("DataSource=app.db")); 
            //=> base.OnConfiguring(optionsBuilder.UseSqlite("DataSource=app.db; Cache=Shared")); 
    }
}

/*
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{ 
    base.OnConfiguring(optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared"));
} 
*/