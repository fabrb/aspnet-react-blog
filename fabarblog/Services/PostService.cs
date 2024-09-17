using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;

namespace fabarblog.Services;
public class PostService(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<IEnumerable<Post>> GetAllPosts()
	{
		return await _postsRepository.SearchAllPosts();
	}

	public async void CreateNewPost(PostDTO post)
	{
		if (post.Title is null)
		{
			return;
		}

		if (post.Content is null)
		{
			return;
		}

		Post newPost = new Post
		{
			Id = Guid.NewGuid(),
			Title = post.Title,
			Content = post.Content,
			CreatedAt = DateTime.Now
		};

		_postsRepository.IncludeNewPost(newPost);

		return;
	}
}
