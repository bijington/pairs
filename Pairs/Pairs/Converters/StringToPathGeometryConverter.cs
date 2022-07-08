
using System.Globalization;
using Microsoft.Maui.Controls.Shapes;

namespace Pairs.Converters;

public class StringToPathGeometryConverter : IValueConverter
{
    private readonly PathGeometryConverter converter;

    public StringToPathGeometryConverter()
    {
        converter = new PathGeometryConverter();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is string stringValue ? converter.ConvertFromInvariantString(stringValue) : value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException($"BindingMode.OneWay is only supported by {nameof(StringToPathGeometryConverter)}");
}