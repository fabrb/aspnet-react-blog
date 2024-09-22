using fabarblog.DTO;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class EditPost(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(PostRequest post, int userId, bool isAdmin)
	{
		if (post.Title is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not edited", Details = new Exception("Invalid title") });

		if (post.Content is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not edited", Details = new Exception("Invalid content") });

		var resultPost = await _postsRepository.SearchPost(post.Id);

		if (resultPost is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not edited", Details = new Exception("Post not found") });

		if (resultPost.UserId != userId && !isAdmin)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not edited", Details = new Exception("Not authorized") });

		resultPost.Title = post.Title;
		resultPost.Content = post.Content;

		await _postsRepository.EditPost(resultPost);

		PostResponse response = new()
		{
			Id = resultPost.Id,
			Title = resultPost.Title,
			Content = resultPost.Content,

			Author = resultPost.User.Username,
			CreationDate = resultPost.CreatedAt
		};

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Post edited", Details = response });
	}
}
