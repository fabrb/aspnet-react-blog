using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fabarblog.DTO;
using fabarblog.Services;

namespace fabarblog.Controller;

[ApiController]
[Route("api/auth")]
public class AuthenticationController(AuthenticateUser authenticateUser) : ControllerBase
{
	private readonly AuthenticateUser _authService = authenticateUser;

	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult<Guid>> Authenticate([FromBody] AuthenticationRequest authentication)
	{
		var result = await _authService.Execute(authentication);

		return Ok(result);
	}
}
