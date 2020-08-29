using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlatform.App.Entities;
using TradingPlatform.Common;
using TradingPlatform.Common.Users;
using TradingPlatform.Common.Users.Dto;
using TradingPlatform.Common.Users.Dto.Request;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с пользователем.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        Task<List<UsersDto>> GetUsersAsync();

        /// <summary>
        /// Получить пользователя.
        /// </summary>
        Task<UsersDto> GetUserAsync(int id);

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        Task CreateUserAsync(LoginRequest user);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        Task DeleteUserAsync(int id);

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        Task UpdateUserAsync(UpdateRequest user);

        /// <summary>
        /// Получить пользователя по логину.
        /// </summary>
        Task<UserEntity> GetUserByLoginAsync(string login);
    }
}
