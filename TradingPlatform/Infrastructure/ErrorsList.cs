using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradingPlatform.Infrastructure
{
    [Serializable]
    public class ErrorsList : List<ValidationResult>
    {
        public void Add(string errorMessage, string memberNames)
        {
            Add(new ValidationResult(errorMessage, new[] { memberNames }));
        }

        public void Add(string errorMessage)
        {
            Add(errorMessage, string.Empty);
        }

        public void AddRange(IEnumerable<string> errorMessages)
        {
            foreach (var errorMessage in errorMessages)
            {
                Add(errorMessage);
            }
        }

        public void Add(ErrorsList errorsList)
        {
            AddRange(errorsList);
        }

        public void Add(IEnumerable<string> errorsList)
        {
            if (errorsList == null)
            {
                return;
            }

            foreach (var error in errorsList)
            {
                Add(error, "");
            }
        }

        public bool IsValid => Count == 0;
    }
}
