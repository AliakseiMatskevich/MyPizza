using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed partial class AppartmentNumberAttribute : ValidationAttribute
    {
        public AppartmentNumberAttribute()
        {
            ErrorMessage = "Appartment number must be like \"101\" or \"101a\"";
        }

        public override bool IsValid(object? value)
        {
            Regex regex = MyRegex();
            string? appartmentNumber = value as string;

            if (appartmentNumber is null)
                return false;

            var match = regex.Match(appartmentNumber!);
            if (match.Success)
                return match.Value.Equals(appartmentNumber);
            return false;
        }

        [GeneratedRegex("\\d+[a-z]*")]
        private static partial Regex MyRegex();
    }
}
