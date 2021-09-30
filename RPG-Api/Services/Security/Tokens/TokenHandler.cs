﻿using Microsoft.Extensions.Options;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Services.Security;
using RPG.Api.Domain.Services.Security.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPG.Api.Services.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();           //  TO DO

        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IPasswordHasher _passwordHaser;

        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHaser)
        {
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfigurations = signingConfigurations;
            _passwordHaser = passwordHaser;
        }

        public AccessToken CreateAccessToken(Account account)
        {
            //var refreshToken = BuildRefreshToken(account);
            var accessToken = BuildAccessToken(account);
            /*_refreshTokens.Add(refreshToken);

            foreach (RefreshToken refresh in _refreshTokens)
            {
                Console.WriteLine("take ref " + refresh.Token);
            }*/

            return accessToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                _refreshTokens.Remove(refreshToken);

            foreach (RefreshToken refresh in _refreshTokens)
            {
                Console.WriteLine("take ref " + refresh.Token);
            }
            return refreshToken;
        }



        private RefreshToken BuildRefreshToken(Account account)
        {
            var refreshToken = new RefreshToken
            (
                token: _passwordHaser.HashPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
            );

            return refreshToken;
        }

        private AccessToken BuildAccessToken(Account account)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            Console.WriteLine(_tokenOptions.Issuer, _tokenOptions.Audience);

            var securityToken = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(account),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks);
        }

        private IEnumerable<Claim> GetClaims(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.login),
                new Claim(JwtRegisteredClaimNames.UniqueName, account.Id.ToString())
            };

            foreach (var userRole in account.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            return claims;
        }
    }
}
