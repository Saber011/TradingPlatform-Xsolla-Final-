using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlatform.App.General;
using TradingPlatform.Common;
using TradingPlatform.Common.Users;
using TradingPlatform.Common.Users.Dto;
using TradingPlatform.Common.Users.Dto.Request;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Cервис для работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Поиск по id
        /// </summary>
        Task<UsersDto> GetUserAsync(int id);

        /// <summary>
        /// Создание пользователя
        /// </summary>
        Task<bool> CreateUserAsync(LoginRequest request);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        Task<bool> DeleteUserAsync(int id);


        /// <summary>
        /// Изменить информацию о пользователе.
        /// </summary>
        Task<bool> UpdateUserAsync(UpdateRequest request);

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        Task<List<UsersDto>> GetAllUsersAsync();

        /// <summary>
        /// Получить токен
        /// </summary>
        Task<Authentication> LoginAsync(LoginRequest request);
    }
}
