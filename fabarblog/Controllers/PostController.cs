using Microsoft.AspNetCore.Mvc;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fabarblog.Controller;

[ApiController]
[Route("api/post")]
public class PostController(ListPosts listPostsService, SearchPost searchPostService, CreatePost createPostService, EditPost editPostService, DeletePost deletePostService) : ControllerBase
{
	private readonly ListPosts _listPostsService = listPostsService;
	private readonly SearchPost _searchPostService = searchPostService;
	private readonly CreatePost _createPostService = createPostService;
	private readonly EditPost _editPostService = editPostService;
	private readonly DeletePost _deletePostService = deletePostService;

	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<IEnumerable<Post>>> ListAll()
	{
		var result = await _listPostsService.Execute();

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpGet("{id}")]
	[AllowAnonymous]
	public async Task<ActionResult<Post>> Search(int id)
	{
		var result = await _searchPostService.Execute(id);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<Guid>> Create([FromBody] PostRequest post)
	{
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
		if (userIdClaim == null)
			return Unauthorized();

		var userId = int.Parse(userIdClaim.Value);
		var result = await _createPostService.Execute(post, userId);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpPut("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Edit([FromBody] PostRequest post, int id)
	{
		post.Id = id;

		var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase));
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
		if (userIdClaim == null)
			return Unauthorized();

		var userId = int.Parse(userIdClaim.Value);

		var result = await _editPostService.Execute(post, userId, isAdmin);

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

		var result = await _deletePostService.Execute(id, userId, isAdmin);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}
}
