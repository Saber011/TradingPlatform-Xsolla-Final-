using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TradingPlatform.Infrastructure
{
    /// <summary>
    /// Генерация токена.
    /// </summary>
    public sealed class AuthOptions
    {
        /// <summary>
        /// Издатель токена.
        /// </summary>
        public const string Issuer = "MyAuthServer";

        /// <summary>
        /// Потребитель токена.
        /// </summary>
        public const string Audience = "MyAuthClient";

        /// <summary>
        /// Ключ для шифрации.
        /// </summary>
        const string Key = "mysupersecret_secretkey!123";

        /// <summary>
        /// Время жизни токена - 5 минут.
        /// </summary>
        public const int LifeTime = 5;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
