using System.Collections.Generic;
using System.Threading.Tasks;

public class PostService
{
	private readonly PostRepository _postsRepository;

	public PostService(PostRepository postsRepository)
	{
		_postsRepository = postsRepository;
	}

	public async Task<IEnumerable<Post>> GetAllPosts()
	{
		return await _postsRepository.SearchAllPosts();
	}
}
