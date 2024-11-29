﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GuessingGame.Domain.Abstractions.Auth;
using GuessingGame.Domain.Constants;
using GuessingGame.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuessingGame.Infrastructure.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
	private readonly JwtOptions _options = options.Value;
	
	public string GenerateToken(GameUserModel user)
	{
		Claim[] claims = [
			new(GameConstants.UserClaimType, user.Id.ToString())
		];
		
		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
			SecurityAlgorithms.HmacSha256);
		
		var token = new JwtSecurityToken(
			issuer: _options.Issuer,
			claims: claims,
			signingCredentials: signingCredentials,
			expires: DateTime.UtcNow.AddHours(_options.ExpiryHours));
		
		var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
		
		return tokenHandler;
	}
}