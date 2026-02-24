using System.Globalization;
using System.Windows.Data;

namespace MapEditTool.Converters
{
    public class IntEqualsConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && parameter is string p)
            {
                return intValue == int.Parse(p);
            }
            return false;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b && parameter is string p)
            {
                return int.Parse(p);
            }
            return Binding.DoNothing;
        }
    }
}