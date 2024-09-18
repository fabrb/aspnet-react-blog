using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;

namespace fabarblog.Services;
public class UserService(UserRepository usersRepository)
{
	private readonly UserRepository _usersRepository = usersRepository;

	public async Task<IEnumerable<User>> GetAllUsers()
	{
		return await _usersRepository.SearchAllUsers();
	}

	public async Task<Either<ErrorCall, SuccessCall>> CreateNewUser(UserDTO User)
	{
		if (User.Username is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not created", Details = new Exception("Invalid username") });

		if (User.Email is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not created", Details = new Exception("Invalid email") });

		if (User.PasswordHash is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not created", Details = new Exception("Invalid password") });

		User newUser = new()
		{
			Username = User.Username,
			Email = User.Email,
			PasswordHash = User.PasswordHash,
			CreatedAt = DateTime.UtcNow
		};

		await _usersRepository.IncludeNewUser(newUser);

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "User created", Details = newUser });
	}
}
