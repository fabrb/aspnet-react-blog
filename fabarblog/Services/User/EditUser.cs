using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;
using Microsoft.AspNetCore.Identity;

namespace fabarblog.Services;
public class EditUser(UserRepository usersRepository)
{
	private readonly UserRepository _usersRepository = usersRepository;
	private readonly PasswordHasher<User> _passwordHasher = new();

	public async Task<Either<ErrorCall, SuccessCall>> Execute(UserRequest user, int userId, bool isAdmin)
	{
		if (user.Email is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not edited", Details = new Exception("Invalid e-mail") });

		if (user.Username is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not edited", Details = new Exception("Invalid username") });

		if (user.Password is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not edited", Details = new Exception("Invalid password") });

		if (user.Id != userId && !isAdmin)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not edited", Details = new Exception("Not authorized") });

		var resultUser = await _usersRepository.SearchUser(user.Id);

		if (resultUser is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not edited", Details = new Exception("User not found") });

		resultUser.Username = user.Username;
		resultUser.Email = user.Email;
		resultUser.PasswordHash = _passwordHasher.HashPassword(resultUser, user.Password);

		if (user.Role is not null && Enum.TryParse(user.Role.ToUpper(), out RoleLevel role))
			resultUser.Role = role;

		var editedUser = await _usersRepository.EditUser(resultUser);

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "User edited", Details = editedUser });
	}
}
