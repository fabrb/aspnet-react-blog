using System.Runtime.InteropServices;

namespace fabarblog.DTO;
public class UserResponse
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string? Role { get; set; }
	public DateTime CreationDate { get; set; }

}
