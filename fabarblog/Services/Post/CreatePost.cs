using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class CreatePost(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(PostRequest post, int userId)
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
			UserId = userId
		};

		var addedPost = await _postsRepository.IncludeNewPost(newPost);
		var addedPostWithRefs = await _postsRepository.SearchPost(addedPost.Id);

		PostResponse response = new()
		{
			Id = addedPostWithRefs.Id,
			Title = addedPostWithRefs.Title,
			Content = addedPostWithRefs.Content,

			Author = new()
			{
				Id = addedPostWithRefs.User.Id,
				Name = addedPostWithRefs.User.Username
			},
			CreationDate = addedPostWithRefs.CreatedAt
		};

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Post created", Details = response });
	}
}
