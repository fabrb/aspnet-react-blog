namespace fabarblog.Models;
public class User
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }

	public ICollection<Post>? Posts { get; set; }

	public DateTime CreatedAt { get; set; }

}
