using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class ListPosts(PostRepository postsRepository)
{
	private readonly PostRepository _postsRepository = postsRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute()
	{
		var resultPosts = await _postsRepository.SearchAllPosts();

		if (resultPosts is null || !resultPosts.Any())
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Posts not listed", Details = new Exception("No posts were created") });

		List<PostResponse> posts = [];
		foreach (var post in resultPosts)
		{
			posts.Add(new PostResponse
			{
				Id = post.Id,
				Title = post.Title,
				Content = post.Content,
				Author = post.User.Username,
				CreationDate = post.CreatedAt
			});
		}

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Posts listed", Details = posts });
	}
}
