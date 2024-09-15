using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
	private readonly PostService _postsService;

	public PostController(PostService postsService)
	{
		_postsService = postsService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
	{
		var posts = await _postsService.GetAllPosts();
		return Ok(posts);
	}
}
