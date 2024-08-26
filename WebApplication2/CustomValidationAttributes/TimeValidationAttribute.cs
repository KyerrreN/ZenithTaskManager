using System.ComponentModel.DataAnnotations;

namespace WebApplication2.CustomValidationAttributes
{
    public class TimeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime timeToValidate))
            {
                return DateTime.Now.AddHours(1) < timeToValidate;
            }

            return false;
        }
    }
}
