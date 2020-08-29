using System.Threading.Tasks;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Сервис для работы с почтой.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отрпавка сообщения.
        /// </summary>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
