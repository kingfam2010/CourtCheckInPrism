using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CourtCheckInPrism.Converters 
{
    public class RandomConverters: IValueConverter
    {
        public static RandomConverters Instance = new RandomConverters();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Int32.TryParse(value?.ToString(), out int res) ? res : value;
        }        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Double.TryParse(value?.ToString(), out double res) ? res : value;
        }

        
    }
    public class StringToDoubleConverter : IValueConverter
    {
        public static StringToDoubleConverter Instance = new StringToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Double.TryParse(value?.ToString(), out double res) ? res : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
    public class InverseBoolConverter : IValueConverter
    {
        public static InverseBoolConverter Instance = new InverseBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
