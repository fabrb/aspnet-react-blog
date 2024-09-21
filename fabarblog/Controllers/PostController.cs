using Microsoft.AspNetCore.Mvc;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Services;
using Microsoft.AspNetCore.Authorization;

namespace fabarblog.Controller;

[ApiController]
[Route("api/post")]
public class PostController(ListPosts listPostsService, CreatePost createPostService, EditPost editPostService, DeletePost deletePostService) : ControllerBase
{
	private readonly ListPosts _listPostsService = listPostsService;
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

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<Guid>> Create([FromBody] PostRequest post, [FromHeader] string autenthication)
	{
		var result = await _createPostService.Execute(post);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpPut("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Edit([FromBody] PostRequest post, int id, [FromHeader] string autenthication)
	{
		Console.WriteLine("id");
		Console.WriteLine(id);

		Console.WriteLine("post");
		Console.WriteLine(post);

		post.Id = id;

		var result = await _editPostService.Execute(post);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<ActionResult<Guid>> Delete(int id, [FromHeader] string autenthication)
	{
		var result = await _deletePostService.Execute(id);

		if (result.IsLeft())
			return BadRequest(result);

		return Ok(result);
	}
}
