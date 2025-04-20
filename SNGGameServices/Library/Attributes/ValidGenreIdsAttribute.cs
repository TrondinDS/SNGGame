using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class ValidGenreIdsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Проверяем, является ли значение списком целых чисел
        if (value is List<int> genreIds)
        {
            // Проверяем, что все элементы находятся в диапазоне от 1 до int.MaxValue
            if (genreIds.All(id => id >= 1 && id <= int.MaxValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Все значения в ListGenreId должны быть в диапазоне от 1 до максимального значения int.");
        }

        // Если значение не является List<int>, но допустимо null
        if (value == null)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("ListGenreId должен быть списком целых чисел.");
    }
}