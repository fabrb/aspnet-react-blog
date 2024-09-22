using fabarblog.DTO;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class SearchPost(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(int id)
	{
		var resultPosts = await _postsRepository.SearchPost(id);

		if (resultPosts is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Post not listed", Details = new Exception("Not found") });

		PostResponse post = new()
		{
			Id = resultPosts.Id,
			Title = resultPosts.Title,
			Content = resultPosts.Content,
			Author = new()
			{
				Id = resultPosts.User.Id,
				Name = resultPosts.User.Username
			},
			CreationDate = resultPosts.CreatedAt
		};

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Post listed", Details = post });
	}
}
