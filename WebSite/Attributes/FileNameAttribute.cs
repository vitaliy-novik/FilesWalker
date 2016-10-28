using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSite.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FileNameAttribute : ValidationAttribute
    {
        public FileNameAttribute(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();
                if (Path.GetInvalidFileNameChars().Any(s => stringValue.Contains(s)))
                {
                    return new ValidationResult(base.ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}