using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class ListUsers(UserRepository usersRepository)
{
	private readonly UserRepository _usersRepository = usersRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute()
	{
		var resultUsers = await _usersRepository.SearchAllUsers();

		if (resultUsers is null || !resultUsers.Any())
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "Users not listed", Details = new Exception("No users were created") });

		List<UserResponse> users = [];
		foreach (var user in resultUsers)
		{
			users.Add(new UserResponse
			{
				Id = user.Id,
				Email = user.Email,
				Username = user.Username
			});
		}

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "Users listed", Details = users });
	}
}
