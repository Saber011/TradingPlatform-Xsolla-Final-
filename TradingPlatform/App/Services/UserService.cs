using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TradingPlatform.App.Entities;
using TradingPlatform.App.Exceptions;
using TradingPlatform.App.General;
using TradingPlatform.App.Interfaces;
using TradingPlatform.App.Validation;
using TradingPlatform.Common.Users.Dto;
using TradingPlatform.Common.Users.Dto.Request;
using TradingPlatform.Infrastructure;

namespace TradingPlatform.App.Services
{
    /// <inheritdoc/>
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateUserAsync(LoginRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var hasher = new PasswordHasher<LoginRequest>();
            var passwordHash = hasher.HashPassword(request, request.Password);
            request.Password = passwordHash;
            await _userRepository.CreateUserAsync(request);

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUserAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));

            if (await _userRepository.GetUserAsync(id) == null)
            {
                throw new AppException("Пользователь не найден");
            }

            await _userRepository.DeleteUserAsync(id);

            return true;
        }

        /// <inheritdoc/>
        public async Task<List<UsersDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        /// <inheritdoc/>
        public async Task<UsersDto> GetUserAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));

            return await _userRepository.GetUserAsync(id)
                ?? throw new AppException("Пользователь не найден");
        }


        /// <inheritdoc/>
        public async Task<Authentication> LoginAsync(LoginRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var identity = await GetIdentityAsync(request.Email, request.Password);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new Authentication
            {
                Token = encodedJwt,
                UserName = identity.Name,
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateUserAsync(UpdateRequest request)
        {
            Guard.NotNull(request, nameof(request));

            if (await _userRepository.GetUserAsync(request.Id) == null)
            {
                throw new AppException("Пользователь не найден");
            }

            await _userRepository.UpdateUserAsync(request);

            return true;
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string login, string password)
        {
            var person = await _userRepository.GetUserByLoginAsync(login);

            var hasher = new PasswordHasher<LoginRequest>();

            if (person == null || hasher.VerifyHashedPassword(MapToRequest(person), person.Password, password) == PasswordVerificationResult.Failed)
            {
                throw new AppException("Invalid username or password");
            }

            var claims = new List<Claim>
           {
               new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
               new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
           };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private LoginRequest MapToRequest(UserEntity entity)
        {
            return new LoginRequest
            {
                Email = entity.Email,
                Password = entity.Password
            };
        }

    }
}
