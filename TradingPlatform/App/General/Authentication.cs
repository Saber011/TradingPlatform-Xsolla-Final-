namespace TradingPlatform.App.General
{
    /// <summary>
    /// Аунтификация.
    /// </summary>
    public sealed class Authentication
    {
        /// <summary>
        /// Токен.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
