using Calculator.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calculator
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }
        public DbSet<HistoryItem> HistoryItems { get; set; }
    }
}
