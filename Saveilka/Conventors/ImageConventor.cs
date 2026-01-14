using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Saveilka.Conventors
{
    internal class ImageConventor : IValueConverter
    {
        // це хуйня,щоб конвентировать фото
        // в формат,який БД може записать
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 


        {
            if (value is not byte[] bytes || bytes.Length == 0)
                return null;

            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }


        //а це обратно в фото
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
