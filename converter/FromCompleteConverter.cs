using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Private.logik_aussagen.converter
{
    public class FromCompleteConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string fristname = values[0]?.ToString() ?? string.Empty;
            string lastname = values[1]?.ToString() ?? string.Empty;
            string eMail = values[2]?.ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(fristname)
                && !string.IsNullOrEmpty(lastname)
                && !string.IsNullOrEmpty(eMail))
            {
                return "Unültige Eingabe!"
;            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
