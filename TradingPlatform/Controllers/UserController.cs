using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TradingPlatform.App.General;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Common.Users.Dto;
using TradingPlatform.Common.Users.Dto.Request;
using TradingPlatform.Infrastructure;

namespace TradingPlatform.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IExecuteService _executeService;

        public UserController(IUserService userService, IExecuteService executeService)
        {
            _userService = userService;
            _executeService = executeService;
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ServiceResponse<List<UsersDto>>> GetUsers()
        {
            return await _executeService.TryExecuteWithCacheAsync(() => _userService.GetAllUsersAsync(), "AllUsers");
        }

        /// <summary>
        /// Добавить пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<bool>> AddUser(LoginRequest request)
        {
            return await _executeService.TryExecute(() => _userService.CreateUserAsync(request));
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            return await _executeService.TryExecute(() => _userService.DeleteUserAsync(id));
        }

        /// <summary>
        /// Изменить информацию о пользователе.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPut]
        [Authorize]
        public async Task<ServiceResponse<bool>> UpdateUser(UpdateRequest request)
        {
            return await _executeService.TryExecute(() => _userService.UpdateUserAsync(request));
        }

        /// <summary>
        /// Получить пользователя по id.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ServiceResponse<UsersDto>> GetById(int id)
        {
            return await _executeService.TryExecuteWithCacheAsync(async () => await _userService.GetUserAsync(id), id);
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ServiceResponse<Authentication>> Login(LoginRequest request)
        {
            return await _executeService.TryExecute(() => _userService.LoginAsync(request));
        }
    }
}
