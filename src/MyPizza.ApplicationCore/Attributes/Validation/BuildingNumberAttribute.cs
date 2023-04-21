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
    public sealed class BuildingNumberAttribute : ValidationAttribute
    {
        public BuildingNumberAttribute()
        {
            ErrorMessage = "Building number must be like \"25\", \"25a\" or \"25/1\"";
        }
        public override bool IsValid(object? value)
        {
            Regex regex = new(@"\d+[a-z]*/*\d*");
            string? buildingNumber = value as string;
            if (buildingNumber is null)
                return false;

            var match = regex.Match(buildingNumber!);
            if (match.Success)
                return match.Value.Equals(buildingNumber);
            return false;
        }
    }
}
