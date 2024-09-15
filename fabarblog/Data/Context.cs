using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
	public Context(DbContextOptions<Context> options) : base(options) { }

	public DbSet<Post> Posts { get; set; }
	public DbSet<User> Users { get; set; }
}
