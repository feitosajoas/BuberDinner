using System;
namespace BuberDinner.Application.Services.Authentication;

public record AuthResult(
	Guid Id,
	string FirstName,
	string LastName,
	string Email,
	string Token
);