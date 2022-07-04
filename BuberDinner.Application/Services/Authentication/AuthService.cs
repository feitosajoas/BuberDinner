using System;
namespace BuberDinner.Application.Services.Authentication;

public class AuthService : IAuthService
{
	public AuthResult Register(string firstName, string lastName, string email, string password)
	{
		return new AuthResult(
			Guid.NewGuid(), 
			firstName, 
			lastName, 
			email, 
			"token");
	}

	public AuthResult Login(string email, string password)
	{
		return new AuthResult(
			Guid.NewGuid(), 
			"firstName", 
			"lastName", 
			email, 
			"token");
	}
}