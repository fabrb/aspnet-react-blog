using System.Security.Claims;
using System.Text;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace fabarblog.Services;
public class VerifyAuthentication(IConfiguration configuration)
{
	private readonly PasswordHasher<User> _passwordHasher = new();

	public async Task<Either<ErrorCall, SuccessCall>> Execute(User user, string password)
	{
		var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

		if (result == PasswordVerificationResult.Failed)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not verified" });

		return Either.Instanciate<ErrorCall, SuccessCall>(new SuccessCall { Message = "User verified" });
	}
}
