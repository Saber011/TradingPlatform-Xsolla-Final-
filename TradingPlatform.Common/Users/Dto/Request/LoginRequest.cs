using System.ComponentModel.DataAnnotations;

namespace TradingPlatform.Common.Users.Dto.Request
{
    /// <summary>
    /// Запрос на авторизацию.
    /// </summary>
    public sealed class LoginRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
