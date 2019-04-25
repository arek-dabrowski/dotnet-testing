using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ASPMVC.Models
{
    public class FirstCapitalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !char.IsUpper(value.ToString().First()))
            {
                return new ValidationResult(GetErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage(string Message)
        {
            return $"{Message} first letter have to be capital.";
        }
    }
}
