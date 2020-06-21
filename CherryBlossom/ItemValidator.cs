using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace CherryBlossom
{
    public class ItemValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var model = (value as BindingGroup).Items[0] as ItemModel;
            throw new NotImplementedException();
        }
    }
}
