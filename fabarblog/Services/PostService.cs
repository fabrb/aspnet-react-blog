using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class PostService(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<IEnumerable<Post>> GetAllPosts()
	{
		return await _postsRepository.SearchAllPosts();
	}

	public async Task<Either<ErrorCall, SuccessCall>> CreateNewPost(PostDTO post)
	{
		if (post.Title is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not created", Details = new Exception("Invalid title") });

		if (post.Content is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not created", Details = new Exception("Invalid content") });

		Post newPost = new()
		{
			Title = post.Title,
			Content = post.Content,
			CreatedAt = DateTime.UtcNow,
			UserId = 2
		};

		await _postsRepository.IncludeNewPost(newPost);

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Post created", Details = newPost });
	}
}
