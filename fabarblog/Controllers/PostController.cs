using Microsoft.AspNetCore.Mvc;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using System.Diagnostics;
using fabarblog.Utils;
using Microsoft.AspNetCore.Authorization;

namespace fabarblog.Controller;

[ApiController]
[Route("api/post")]
public class PostController(PostService postsService) : ControllerBase
{
	private readonly PostService _postsService = postsService;

	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<IEnumerable<Post>>> ListAll()
	{
		var posts = await _postsService.GetAllPosts();
		return Ok(posts);
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<Guid>> Create([FromBody] PostDTO post, [FromHeader] string autenthication)
	{
		var result = await _postsService.CreateNewPost(post);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpPut("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Edit([FromBody] PostDTO post, int id, [FromHeader] string autenthication)
	{
		Console.WriteLine("id");
		Console.WriteLine(id);

		Console.WriteLine("post");
		Console.WriteLine(post);

		post.Id = id;

		var result = await _postsService.EditPost(post);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Delete(int id, [FromHeader] string autenthication)
	{
		var result = await _postsService.DeletePost(id);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}
}
