using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionGame.ViewModels
{
    public class WallViewModel :BaseVM
    {
        const int DefaultSize = 10;
        private Random rand;

        public WallViewModel()
            : this(DefaultSize)
        { 
        }

        public WallViewModel(int count) 
        {
            this.Count = count;
            this.rand = new Random(count);
            this.Wall = new WallPiece[count];
        }

        public int Count { get; set; }
        public WallPiece[] Wall { get; set; }

        public void buildWall() 
        {
            double x = 50;
            double y = 50;

            //TODO
            for (int i = 0; i < this.Count; i++)
            {
                WallPiece wall = new WallPiece();
                wall.Height = x;
                wall.Width = y;
                if (i != 0)
                {           
                    wall.Top = y + this.Wall[i - 1].Top; 
                }
                this.Wall[i] = wall;
                
            }
        }
       
    }
}
