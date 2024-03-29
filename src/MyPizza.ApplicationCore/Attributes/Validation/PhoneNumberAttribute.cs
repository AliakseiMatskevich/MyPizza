﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed partial class PhoneNumberAttribute : ValidationAttribute
    {
        public PhoneNumberAttribute()
        {
            ErrorMessage = "Phone number must be like \"+375-33-333-33-33\"";
        }

        public override bool IsValid(object? value)
        {
            Regex regex = MyRegex();
            string? phoneNumber = value as string;

            if (phoneNumber is null)
                return false;

            var match = regex.Match(phoneNumber!);
            if (match.Success)
                return match.Value.Equals(phoneNumber);
            return false;
        }

        [GeneratedRegex("\\+\\d{3}[\\s\\-]*\\d{2}[\\s\\-]*\\d{3}[\\s\\-]*\\d{2}[\\s\\-]*\\d{2}")]
        private static partial Regex MyRegex();
    }
}
