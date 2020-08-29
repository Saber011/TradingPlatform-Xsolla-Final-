namespace TradingPlatform.Common
{
    /// <summary>
    /// Оповещение о продаже.
    /// </summary>
    public sealed class Notification
    {
        /// <summary>
        /// Ключ продукта.
        /// </summary>
        public string ProductKey { get; set; }

        /// <summary>
        /// Наимнование продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Имя пользваотеля.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// SessionId.
        /// </summary>
        public string SessionId { get; set; }
    }
}
