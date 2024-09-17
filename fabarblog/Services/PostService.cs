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

	public Either<ErrorCall, SuccessCall> CreateNewPost(PostDTO post)
	{
		if (post.Title is null)
		{
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not created", Details = new Exception("Invalid title"), StatusCode = 400 });
		}

		if (post.Content is null)
		{
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not created", Details = new Exception("Invalid content"), StatusCode = 400 });
		}

		Post newPost = new Post
		{
			Id = Guid.NewGuid(),
			Title = "",
			Content = "post.Content",
			CreatedAt = DateTime.Now
		};

		// _postsRepository.IncludeNewPost(newPost);

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Post created", Details = newPost, StatusCode = 201 });
	}
}
