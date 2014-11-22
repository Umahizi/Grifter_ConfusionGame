using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ConfusionGame.Converters
{
    public class ImageToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            if (targetType != typeof(ImageSource))
            {
                return null;
            }

            BitmapImage image = (value as BitmapImage);

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = image;
            /*int age = int.Parse(value.ToString());
            Brush brush;
            if (age > 25)
            {
                brush = new SolidColorBrush { Color = Colors.Red };
            }
            else
            {
                brush = new SolidColorBrush { Color = Colors.Yellow };
            }*/

            return brush.ImageSource;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
