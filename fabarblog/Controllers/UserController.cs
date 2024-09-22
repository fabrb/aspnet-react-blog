using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using System.Security.Claims;

namespace fabarblog.Controller;

[ApiController]
[Route("api/user")]
public class UserController(ListUsers listUsersService, SearchUser searchUserService, CreateUser createUserService, EditUser editUserService, DeleteUser deleteUserService) : ControllerBase
{
	private readonly ListUsers _listUsersService = listUsersService;
	private readonly SearchUser _searchUserService = searchUserService;
	private readonly CreateUser _createUserService = createUserService;
	private readonly EditUser _editUserService = editUserService;
	private readonly DeleteUser _deleteUserService = deleteUserService;

	[Authorize]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> ListAll()
	{
		var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase));

		if (!isAdmin)
			return Unauthorized();

		var result = await _listUsersService.Execute();

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize]
	[HttpGet("{id}")]
	public async Task<ActionResult<IEnumerable<User>>> Search(int id)
	{
		var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase));

		if (!isAdmin)
			return Unauthorized();

		var result = await _searchUserService.Execute(id);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult<Guid>> Create([FromBody] UserRequest user)
	{
		if (user == null)
			return BadRequest("Invalid user data");

		var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase));

		if (!isAdmin)
			user.Role = "basic";

		var result = await _createUserService.Execute(user);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpPut("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Edit([FromBody] UserRequest user, int id)
	{
		user.Id = id;

		var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase));
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
		if (userIdClaim == null)
			return Unauthorized();

		var userId = int.Parse(userIdClaim.Value);

		var result = await _editUserService.Execute(user, userId, isAdmin);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Delete(int id)
	{
		var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase));
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
		if (userIdClaim == null)
			return Unauthorized();

		var userId = int.Parse(userIdClaim.Value);

		var result = await _deleteUserService.Execute(id, userId, isAdmin);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}
}
