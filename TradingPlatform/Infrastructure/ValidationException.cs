namespace TradingPlatform.Infrastructure
{

    /// <summary>
    /// Ошибка валидации модели
    /// </summary>
    public class ValidationException : System.Exception
    {
        /// <summary>
        /// Флаг, определяющий, критическая ли ошибка произошла или данное поведение корректно для системы
        /// </summary>
        public bool IsCritical { get; }

        /// <summary>
        /// Список ошибок валидации
        /// </summary>
        public ErrorsList ErrorsList { get; }

        public ValidationException(bool isCritical, ErrorsList errorsList) : base("Ошибки валидации модели")
        {
            IsCritical = isCritical;
            ErrorsList = errorsList;
        }

        /// <summary>
        /// Конструктор для ошибки null argument
        /// </summary>
        public ValidationException(string nullArgName, bool isCritical = true) : base("Ошибки валидации модели")
        {
            this.IsCritical = isCritical;
            this.ErrorsList = new ErrorsList() { $"argument {nullArgName} must be not null" };
        }
    }
}
