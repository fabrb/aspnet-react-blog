using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class DeletePost(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(int id, int userId, bool isAdmin)
	{
		var resultPost = await _postsRepository.SearchPost(id);

		if (resultPost is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not deleted", Details = new Exception("Post not found") });

		if (resultPost.UserId != userId && !isAdmin)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not deleted", Details = new Exception("Not authorized") });

		await _postsRepository.RemovePost(resultPost);

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Post deleted", Details = new { Id = id } });
	}
}
