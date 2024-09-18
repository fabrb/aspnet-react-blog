using Microsoft.AspNetCore.Mvc;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using System.Diagnostics;
using fabarblog.Utils;

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
	public async Task<ActionResult<Guid>> Create([FromBody] PostDTO post, [FromHeader] string? autenthication)
	{
		var result = await _postsService.CreateNewPost(post);

		if (result.IsLeft())
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
}
