using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using ConfusionGame.ViewModels;

namespace ConfusionGame.Converters
{
    public class StatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
           object parameter, string language)
        {
            if (targetType != typeof(string))
            {
                return null;
            }

           
            GameStatus text = (GameStatus)Enum.Parse(typeof(GameStatus),value.ToString());

            string result;

            if (text == GameStatus.On)
            {
                result = "Game is On";
            }
            else if (text == GameStatus.Paused)
            {
                result = "Game is Poised";
            }
            else
            {
                result = "Game Won";
            }
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

            return result;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
