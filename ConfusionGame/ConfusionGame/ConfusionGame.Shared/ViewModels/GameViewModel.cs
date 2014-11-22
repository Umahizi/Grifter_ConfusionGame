using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionGame.ViewModels
{
    public class GameViewModel:BaseVM
    {
        private const int ObsticleCount =10;
        private int dificulty;
        private Random randomizer = new Random();

        public GameViewModel() : this(ObsticleCount)
        {
        
        }

        public GameViewModel(int count)
        {
            this.Player = new PlayerViewModel();
            this.Dificulty = 0;
            this.IsPoused = false;
            this.HasEnded = false;
            this.WallCount = count;
            this.Walls = new List<WallPiece>();
        }

        public GameViewModel(PlayerViewModel player)
        {
            this.Player = player;
        }

        public int WallCount { get; set; }

        public int Dificulty  
        {
            get
            {
                return this.dificulty;
            }
            set
            {
                this.dificulty = value;
                this.OnPropertyChanged("Dificulty");
            }
        }

        public bool IsPoused { get; set; }

        public bool HasEnded { get; set; }

        public double GameFieldHeight { get; set; }

        public double GameFieldWidth { get; set; }

        public PlayerViewModel Player { get; private set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ICollection<WallPiece> Walls { get; set; }

        public void MovePlayer(double x, double y)
        {
            if (!this.IsPoused)
            {
                switch (this.Dificulty)
                {
                    case 0: this.Player.Move(x, y); break;
                    case 1: this.Player.Move(y, x); break;
                    case 2: this.Player.Move(-x, -y); break;
                    case 3: this.Player.Move(-y, -x); break;
                    default: break;

                }
            }
        }

        public void StartGame()
        {
            this.Player.Top = this.GameFieldHeight / 2 - this.Player.Height / 2;
            for (int i = 0; i < this.WallCount; i++)
            {
                WallPiece wall = new WallPiece();
                wall.Top = this.GameFieldHeight - this.randomizer.Next(0, (int)this.GameFieldHeight);
                wall.Left = this.GameFieldWidth - this.randomizer.Next(0, (int)this.GameFieldWidth);
                this.Walls.Add(wall);
            }
        }
    }
}
