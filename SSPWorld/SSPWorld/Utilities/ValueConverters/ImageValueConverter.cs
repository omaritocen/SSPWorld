using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SSPWorld.Utilities.ValueConverters
{
    public class ImageValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as string;
            if (string.IsNullOrEmpty(source))
                return null;

            var imagePath = UriImageSource.FromUri(new Uri(value.ToString()));
            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
