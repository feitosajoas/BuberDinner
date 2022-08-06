using BuberDinner.Application.Common.Errors;
using OneOf;

namespace BuberDinner.Application.Services.Authentication;

public interface IAuthService 
{
	OneOf<AuthResult, DuplicateEmailError> Register(string firstName, string lastName, string email, string password);
	AuthResult Login(string email, string password);
}