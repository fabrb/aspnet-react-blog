using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;
using Microsoft.AspNetCore.Identity;

namespace fabarblog.Services;
public class CreateUser(UserRepository usersRepository)
{
	private readonly UserRepository _usersRepository = usersRepository;
	private readonly PasswordHasher<User> _passwordHasher = new();

	public async Task<Either<ErrorCall, SuccessCall>> Execute(UserRequest user)
	{
		if (user.Username is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not created", Details = new Exception("Invalid username") });

		if (user.Email is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not created", Details = new Exception("Invalid email") });

		if (user.Password is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not created", Details = new Exception("Invalid password") });

		if (user.Role is null || !Enum.TryParse(user.Role.ToUpper(), out RoleLevel role))
			role = RoleLevel.BASIC;

		User newUser = new()
		{
			Username = user.Username,
			Email = user.Email,
			CreatedAt = DateTime.UtcNow,
			Role = role
		};
		newUser.PasswordHash = _passwordHasher.HashPassword(newUser, user.Password);

		await _usersRepository.IncludeNewUser(newUser);

		UserResponse responseUser = new()
		{
			Username = user.Username,
			Email = user.Email,
			Id = user.Id,
			CreationDate = newUser.CreatedAt
		};

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "User created", Details = responseUser });
	}
}
