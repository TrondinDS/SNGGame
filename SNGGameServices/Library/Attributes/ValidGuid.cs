using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Attributes
{
    public class ValidGuid : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            return Guid.TryParse(value.ToString(), out _);
        }
    }

}
