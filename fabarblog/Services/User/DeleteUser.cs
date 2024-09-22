using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class DeleteUser(UserRepository usersRepository)
{
	private readonly UserRepository _usersRepository = usersRepository;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(int id, int userId, bool isAdmin)
	{
		var resultUser = await _usersRepository.SearchUser(id);

		if (resultUser is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not deleted", Details = new Exception("User not found") });

		if (resultUser.Id != userId && !isAdmin)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not deleted", Details = new Exception("Not authorized") });

		await _usersRepository.Removeuser(resultUser);

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "User deleted", Details = new { Id = id } });
	}
}
