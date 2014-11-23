using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;

namespace ConfusionGame.ViewModels
{
    public class WallPiece: GameObjectVM
    {
        private const double DefaultX = 300;
        private const double DefaultY = 0;
        private const double defaultWidth = 175;
        private const double defaultHeight = 175;

        private const string source = "ms-appx:///Assets/Turquoise-stone.png";
        public WallPiece()
            :base(DefaultX,DefaultY,defaultHeight, defaultWidth,source) 
        {
        
        }

       
    }
}
