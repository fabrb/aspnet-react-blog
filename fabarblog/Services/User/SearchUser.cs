using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class SearchUser(UserRepository usersRepository)
{
	private readonly UserRepository _usersRepository = usersRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(int id)
	{
		var resultUsers = await _usersRepository.SearchUser(id);

		if (resultUsers is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not listed", Details = new Exception("Not found") });

		UserResponse user = new()
		{
			Id = resultUsers.Id,
			Email = resultUsers.Email,
			Username = resultUsers.Username,
			Role = resultUsers.Role.ToString(),
			CreationDate = resultUsers.CreatedAt
		};

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall
		{
			Message = "User listed",
			Details = user
		});
	}
}
