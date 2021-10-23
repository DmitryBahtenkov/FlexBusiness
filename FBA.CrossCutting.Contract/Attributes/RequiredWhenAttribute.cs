using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FBA.CrossCutting.Contract.Attributes
{
    public class RequiredWhenAttribute : RequiredAttribute
    {
        private readonly string _targetField;
        private readonly object _targetValue;

        public RequiredWhenAttribute(string targetField, object targetValue)
        {
            _targetField = targetField;
            _targetValue = targetValue;
        }


        /*public override bool IsValid(object? value)
        {
            
        }*/
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = validationContext.ObjectInstance.GetType().GetProperties();
            var currentProperty = properties.FirstOrDefault(x => x.Name == _targetField);
            
            if (currentProperty is null)
            {
                return ValidationResult.Success;
            }

            var currentValue = currentProperty.GetValue(validationContext.ObjectInstance);

            // если поле найдено и его значение соответсвует указанному, поле обязательно
            if (currentValue?.Equals(_targetValue) is true)
            {
                return base.IsValid(value, validationContext);
            }

            return ValidationResult.Success;
        }
    }
}