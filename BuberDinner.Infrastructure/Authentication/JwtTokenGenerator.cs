using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BuberDinner.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly JwtSettings _jwtSettings;
	private readonly IDateTimeProvider _dateTimeProvider;

	public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
	{
		_dateTimeProvider = dateTimeProvider;
		_jwtSettings = jwtOptions.Value;
	}

	public string GenerateToken(Guid userId, string firstName, string lastName)
	{
		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
			SecurityAlgorithms.HmacSha256
		);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, firstName + " " + lastName),
			new Claim(JwtRegisteredClaimNames.GivenName, firstName),
			new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var securityToken = new JwtSecurityToken(
			issuer: _jwtSettings.Issuer,
			audience: _jwtSettings.Audience,
			expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
			claims: claims,
			signingCredentials: signingCredentials);

		return new JwtSecurityTokenHandler().WriteToken(securityToken);
	}
}