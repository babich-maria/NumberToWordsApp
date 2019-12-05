using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NumberToWordApp.Models
{
    public class NumberViewModel
    {
        [Required]
        [DisplayName("Number")]
        [RegularExpression(@"^\d{0,9}[\,]{0,1}\d{0,2}$", ErrorMessage = "Please enter up to 9 digits and up to 2 digits after comma")]
        public string Value { get; set; }

        public double ValueDouble
        {
            get
            {
                return Double.Parse(Value.Replace(',', '.'));
            }
        }
    }
}