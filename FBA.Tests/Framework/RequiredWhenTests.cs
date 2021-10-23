using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FBA.CrossCutting.Contract.Attributes;
using Xunit;

namespace FBA.Tests.Framework
{
    public class RequiredWhenTests
    {
        [Fact(DisplayName = "Проверка атрибута валидации с условием для строк")]
        public void CorrectSecondFieldWithEmptyFirstTest()
        {
            // arrange
            var dto = new RequiredWhenTestDto
            {
                Second = "lol kek"
            };
            
            // act
            var result = IsValid(dto);
            
            // assert
            Assert.False(result);
        }
        
        [Fact(DisplayName = "Проверка атрибута валидации с условием для строк")]
        public void CorrectSecondFieldWithNonEmptyFirstTest()
        {
            // arrange
            var dto = new RequiredWhenTestDto
            {
                First = "a",
                Second = "lol kek"
            };
            
            // act
            var result = IsValid(dto);
            
            // assert
            Assert.True(result);
        }
        
        [Fact(DisplayName = "Проверка атрибута валидации с условием для строк")]
        public void EmptySecondFieldWitEmptyFirstTest()
        {
            // arrange
            var dto = new RequiredWhenTestDto
            {
                First = null,
                Second = null
            };
            
            // act
            var result = IsValid(dto);
            
            // assert
            Assert.True(result);
        }

        private bool IsValid(object value)
        {
            var validationContext = new ValidationContext(value);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(value, validationContext, results);
        }
    }

    public class RequiredWhenTestDto
    {
        [RequiredWhen(nameof(Second), "lol kek")]
        public string First { get; set; }
        public string Second { get; set; }
    }
}