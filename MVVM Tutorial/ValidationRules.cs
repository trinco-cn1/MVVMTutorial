using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVMTutorial
{
    public class NumberRangeRule : ValidationRule
    {
        int max;
        int min;
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value,out number))
            {
                return new ValidationResult(false, "Invalid number format!");
            }
            if (number < Min || number > Max)
            {
                return new ValidationResult(false, string.Format("Number out of range({0}-{1})", Min, Max));
            }
            return ValidationResult.ValidResult;
        }
    }
}
