using System;
using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthService : IAuthService
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;

	public AuthService(IJwtTokenGenerator jwtTokenGenerator)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
	}

	public AuthResult Register(string firstName, string lastName, string email, string password)
	{
		// Check if user already exists

		// Create user (generate unique ID)

		// Create JWT token
		Guid userId = Guid.NewGuid();
		var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

		return new AuthResult(
			Guid.NewGuid(), 
			firstName, 
			lastName, 
			email, 
			token);
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