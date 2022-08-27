using System;
using System.Globalization;
using System.Windows.Data;

namespace DiscordRichPrecence.Services
{
    public class SelectedIndexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (int)value >= 0)
                    return true;
                else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;


    }
}
