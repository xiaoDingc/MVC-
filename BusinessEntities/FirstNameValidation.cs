using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("not Allowed null");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("First Name should not contain @");
                }
            }
            return ValidationResult.Success;
        }
    }
}