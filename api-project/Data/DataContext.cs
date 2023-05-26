using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace api_project.Data
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Journal> JournalEntries { get; set; }
    }
}
