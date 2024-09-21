using System.Security.Claims;
using System.Text;
using fabarblog.DTO;
using fabarblog.Models;
using fabarblog.Repository;
using fabarblog.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace fabarblog.Services;
public class AuthenticateUser(UserRepository usersRepository, VerifyAuthentication verifyAuthenticationService, GenerateAuthenticationToken generateAuthenticationTokenService, IConfiguration configuration)
{
	private readonly UserRepository _usersRepository = usersRepository;
	private readonly VerifyAuthentication _verifyAuthenticationService = verifyAuthenticationService;
	private readonly GenerateAuthenticationToken _generateAuthenticationTokenService = generateAuthenticationTokenService;

	public async Task<Either<ErrorCall, SuccessCall>> Execute(AuthenticationRequest user)
	{
		if (user.Email is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not authenticated", Details = new Exception("Invalid email") });

		if (user.Password is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not authenticated", Details = new Exception("Invalid password") });

		var resultUser = await _usersRepository.SearchUserByEmail(user.Email);
		if (resultUser is null)
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not authenticated", Details = new Exception("User not found") });

		var resultVerify = await _verifyAuthenticationService.Execute(resultUser, user.Password);
		if (resultVerify.IsLeft())
			return Either.Instanciate<ErrorCall, SuccessCall>(new ErrorCall { Message = "User not authenticated", Details = new Exception("User not verified") });

		var resultGenerate = await _generateAuthenticationTokenService.Execute(resultUser);
		return resultGenerate;
	}
}
