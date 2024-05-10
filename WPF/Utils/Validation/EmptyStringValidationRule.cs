using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.Utils.Validation {
    public class EmptyStringValidationRule : ValidationRule {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo) {
            var s = value as string;

            if(s == null)
                return new System.Windows.Controls.ValidationResult(false, "Field required");

            if (string.IsNullOrWhiteSpace(s))
                return new System.Windows.Controls.ValidationResult(false,"Field required");
            
            return new System.Windows.Controls.ValidationResult(true,null);
        }
    }
}
