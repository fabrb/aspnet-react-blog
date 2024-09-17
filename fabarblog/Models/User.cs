namespace fabarblog.Models;
public class User
{
	public required Guid Id { get; set; }
	public required string Username { get; set; }
	public required string Email { get; set; }
	public required string PasswordHash { get; set; }

	public ICollection<Post>? Posts { get; set; }

	public DateTime CreatedAt { get; set; }

}
