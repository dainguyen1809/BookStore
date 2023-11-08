using Book_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Repository.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
	}
}
