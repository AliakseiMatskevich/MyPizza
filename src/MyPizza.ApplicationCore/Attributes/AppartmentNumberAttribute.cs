using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AppartmentNumberAttribute : ValidationAttribute
    {
        public AppartmentNumberAttribute()
        {
            ErrorMessage = "Appartment number must be like \"101\" or \"101a\"";
        }

        public override bool IsValid(object? value)
        {
            Regex regex = new(@"\d+[a-z]*");
            string? appartmentNumber = value as string;
            var match = regex.Match(appartmentNumber!);
            if (match.Success)
                return match.Value.Equals(appartmentNumber);
            return false;
        }
    }
}
