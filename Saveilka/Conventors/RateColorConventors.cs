using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Saveilka.Conventors;

internal class RateColorConventors : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Colors.Transparent;

        float rate;

        // підтримка float, double, int
        if (value is float f)
            rate = f;
        else if (value is double d)
            rate = (float)d;
        else if (value is int i)
            rate = i;
        else
            return Colors.Transparent;

        if (rate < 5f)
            return Colors.LightCoral;

        if (rate < 8f)
            return Colors.LightYellow;

        return Colors.LightGreen;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
