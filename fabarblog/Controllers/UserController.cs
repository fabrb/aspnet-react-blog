using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace fabarblog.Controller;

[ApiController]
[Route("api/user")]
public class UserController(UserService usersService, IConfiguration configuration) : ControllerBase
{
	private readonly UserService _usersService = usersService;
	private readonly IConfiguration _configuration = configuration;

	[Authorize(Roles = "admin")]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> ListAll()
	{
		var users = await _usersService.GetAllUsers();
		return Ok(users);
	}

	[Authorize]
	[HttpPost]
	public async Task<ActionResult<Guid>> Create([FromBody] UserDTO user, [FromHeader] string? autenthication)
	{
		if (user == null)
			return BadRequest("Invalid user data");

		var result = await _usersService.CreateNewUser(user);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<ActionResult<Guid>> Authenticate([FromBody] UserDTO user, [FromHeader] string? autenthication)
	{
		var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new(ClaimTypes.NameIdentifier, user.Email),
				new(ClaimTypes.Name, user.Username),
				new(ClaimTypes.Role, "admin")
			]),

			Expires = DateTime.UtcNow.AddHours(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		var tokenString = tokenHandler.WriteToken(token);

		return Ok(new { Token = tokenString });
	}

	[Authorize]
	[HttpPut("logout")]
	public async Task<ActionResult<Guid>> Unauthenticate([FromHeader] string? autenthication)
	{
		return Ok();
	}
}
