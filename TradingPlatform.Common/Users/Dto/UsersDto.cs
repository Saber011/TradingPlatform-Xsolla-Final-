using System;
using System.ComponentModel.DataAnnotations;

namespace TradingPlatform.Common.Users.Dto
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Serializable]
    public sealed class UsersDto
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

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }
    }
}
