using System;
using System.ComponentModel.DataAnnotations;

public class DateNotInPastAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue < DateTime.Today)
            {
                return new ValidationResult(ErrorMessage ?? "The date cannot be in the past.");
            }
        }

        return ValidationResult.Success;
    }
}