namespace fabarblog.Models;
public enum RoleLevel
{
	BASIC,
	ADMIN
}

public class User
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }
	public RoleLevel Role { get; set; } = RoleLevel.BASIC;

	public ICollection<Post>? Posts { get; set; }

	public DateTime CreatedAt { get; set; }

}
