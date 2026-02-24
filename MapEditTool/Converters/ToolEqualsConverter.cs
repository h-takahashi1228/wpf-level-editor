using System.Globalization;
using System.Windows.Data;

namespace MapEditTool.Converters
{
    public class ToolEqualsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string targetTypeName = parameter.ToString();
            return value.GetType().Name == targetTypeName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
