using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using ConfusionGame.Common;
using System.Windows.Input;

namespace ConfusionGame.ViewModels
{
    public class PlayerViewModel : GameObjectVM
    {
        private const int DefaultX = 100;
        private const int DefaultY = 150;
        private const double defaultWidth = 175;
        private const double defaultHeight = 175;
        public const string spriteSheet = "ms-appx:///Assets/ghostSheet.PNG";

       

        public PlayerViewModel()
            : base(DefaultX, DefaultY, defaultHeight, defaultWidth, spriteSheet) 
        {
        }

        public PlayerViewModel(double x, double y) : base(x, y, defaultHeight, defaultWidth, spriteSheet) { }


      /* private ICommand commandMove;
      

        public ICommand Move
        {
            get
            {
                if (this.commandMove == null)
                {
                    this.commandMove =
                        new RelayCommandWithParameters(this.PerformMove);
                }
                return this.commandMove;
            }

        }

        private void PerformMove(object obj)
        {
            var coordinates = obj as string;
            double[] xy = new double[2];
            xy[0]=double.Parse(coordinates[0].ToString());
            xy[1] = double.Parse(coordinates[1].ToString());
            this.Left += xy[0];
            this.Top += xy[1];
        }*/

        //TODO char view representation

        public void Move(double x,double y)
        {
            this.Left += x;
            this.Top += y; 
        }
    }
}
