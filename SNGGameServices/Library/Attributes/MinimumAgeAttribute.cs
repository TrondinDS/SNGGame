using System;
using System.ComponentModel.DataAnnotations;

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !(value is DateTime))
        {
            return new ValidationResult("Дата рождения должна быть указана.");
        }

        DateTime dateOfBirth = (DateTime)value;
        DateTime currentDate = DateTime.Today;

        int age = currentDate.Year - dateOfBirth.Year;
        if (dateOfBirth > currentDate.AddYears(-age))
        {
            age--;
        }

        if (age < _minimumAge)
        {
            return new ValidationResult($"Вы должны быть старше {_minimumAge} лет.");
        }

        return ValidationResult.Success;
    }
}
