using Microsoft.AspNetCore.Mvc;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using System.Diagnostics;
using fabarblog.Utils;

namespace fabarblog.Controller;

[ApiController]
[Route("api/user")]
public class UserController(UserService usersService) : ControllerBase
{
	private readonly UserService _usersService = usersService;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> ListAll()
	{
		var users = await _usersService.GetAllUsers();
		return Ok(users);
	}

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
}
