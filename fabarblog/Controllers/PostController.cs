using Microsoft.AspNetCore.Mvc;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using System.Diagnostics;

namespace fabarblog.Controller;

[ApiController]
[Route("api/post")]
public class PostController(PostService postsService) : ControllerBase
{
	private readonly PostService _postsService = postsService;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Post>>> ListAll()
	{
		var posts = await _postsService.GetAllPosts();
		return Ok(posts);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create([FromBody] Object post, [FromHeader] string? autenthication)
	{
		if (post == null)
		{
			return BadRequest("Post data is invalid.");
		}
		return Ok();
	}
}
