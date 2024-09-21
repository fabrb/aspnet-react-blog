using System.Security.Claims;
using System.Text;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Utils;
using Microsoft.IdentityModel.Tokens;

namespace fabarblog.Services;
public class GenerateAuthenticationToken(IConfiguration configuration)
{
	private readonly IConfiguration _configuration = configuration;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(User user)
	{
		var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(ClaimTypes.NameIdentifier, user.Email),
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			]),
			Expires = DateTime.UtcNow.AddHours(1),
			Audience = _configuration["Jwt:Audience"],
			Issuer = _configuration["Jwt:Issuer"],
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		var tokenString = tokenHandler.WriteToken(token);

		AuthenticationResponse response = new()
		{ Token = tokenString, Expiration = DateTime.Now.AddHours(1) };

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Token created", Details = response });
	}
}
