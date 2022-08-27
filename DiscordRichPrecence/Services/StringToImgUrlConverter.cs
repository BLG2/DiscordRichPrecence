using System;
using System.Globalization;
using System.Windows.Data;

namespace DiscordRichPrecence.Services
{
    public class StringToImgUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()) && value.ToString().EndsWith(".png") || value.ToString().EndsWith(".jpg") || value.ToString().EndsWith(".gif")) return value; else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

    }
}
