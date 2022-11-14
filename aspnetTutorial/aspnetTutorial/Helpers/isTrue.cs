using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Helpers
{
    public class isTrue : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value!=null)
            {
                var cadena = (bool)value;
                if (cadena==true)
                {
                    //Mi validacion aqui
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage?? "Error");
        }
    }
}
