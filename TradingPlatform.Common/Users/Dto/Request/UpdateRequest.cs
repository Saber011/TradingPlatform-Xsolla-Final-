using System.ComponentModel.DataAnnotations;

namespace TradingPlatform.Common.Users.Dto.Request
{
    /// <summary>
    /// Изменение информации о пользователе.
    /// </summary>
    public sealed class UpdateRequest
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}
