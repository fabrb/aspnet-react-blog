using System.ComponentModel.DataAnnotations;

namespace fabarblog.DTO;

public class AuthenticationRequest
{
	[EmailAddress]
	public string Email { get; set; }
	public string Password { get; set; }
}