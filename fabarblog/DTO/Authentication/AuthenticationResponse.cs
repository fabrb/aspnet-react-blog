namespace fabarblog.DTO;
class AuthenticationResponse
{
	public string Token { get; set; }
	public DateTime Expiration { get; set; }
}