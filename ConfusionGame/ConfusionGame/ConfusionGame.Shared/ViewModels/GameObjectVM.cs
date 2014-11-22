using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace ConfusionGame.ViewModels
{
    public class GameObjectVM: BaseVM
    {
        private double x;
        private double y;
        private double maxX;
        private double maxY;
        private double width;
        private double height;

       // private BitmapImage imageSource;

       

         public GameObjectVM(double x, double y,double height, double width, string source)
        {
            this.Left = x;
            this.Top = y;
            this.Source = new BitmapImage();
            this.Source.UriSource = new Uri(source, UriKind.Absolute);
            this.Height = height;
            this.Width = width;

        }

      

        public BitmapImage Source { get; set; }

        public double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                this.OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
                this.OnPropertyChanged("Height");
            }
        }

        public double Left 
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
                this.OnPropertyChanged("Left");
            }
        }

        public double Top
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
                this.OnPropertyChanged("Top");
            }
        }

        public double MaxX
        {
            get
            {
                return this.maxX;
            }
            set
            {
                this.maxX = value;
                this.OnPropertyChanged("X");
            }
        }

        public double MaxY
        {
            get
            {
                return this.maxY;
            }
            set
            {
                this.maxY = value;
                this.OnPropertyChanged("Y");
            }
        }
        


        public bool HasColided { get; set; }
    }
}
