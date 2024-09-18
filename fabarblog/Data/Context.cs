using fabarblog.Models;
using Microsoft.EntityFrameworkCore;

namespace fabarblog.Data;

public class Context : DbContext
{
	public Context(DbContextOptions<Context> options) : base(options) { }

	public DbSet<Post> Posts { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Post>().Property(p => p.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
	}
}
