using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionGame.ViewModels
{
    public class GameViewModel:BaseVM
    {
        private const int ObsticleCount =10;
        private int dificulty;
        private Random randomizer;
        private GameStatus gameStatus;
        private string gameState;
        

        public GameViewModel() : this(ObsticleCount)
        {
        
        }

        public GameViewModel(int count)
        {
            this.Player = new PlayerViewModel();
            this.Dificulty = 0;
            this.WallCount = count;
            this.Walls = new List<WallPiece>();
            this.randomizer= new Random();
            this.GameStatusCode = GameStatus.On;
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

        public int Score { get; set; }

        public GameStatus GameStatusCode
        {
            get 
            {
                return this.gameStatus;
            }
            set { 
            if(value==GameStatus.On)
            {
                this.gameStatus = GameStatus.On;
                this.GameState = "Game is On";
            }
            else if (value == GameStatus.Paused)
            {
                this.gameStatus = GameStatus.On;
                this.GameState = "Game is Poused";
            }
            else 
            {
                this.gameStatus = GameStatus.On;
                this.GameState = "Game Won";
            }
            }
        }

        public double GameFieldHeight { get; set; }

        public double GameFieldWidth { get; set; }

        public PlayerViewModel Player { get; private set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ICollection<WallPiece> Walls { get; set; }

        public String GameState
        {
            get
            {
                return this.gameState;
            }
            set
            {
                this.gameState = value;
                this.OnPropertyChanged("GameState");
            }
        }

        public void MovePlayer(double x, double y)
        {
            if (this.GameStatusCode==GameStatus.On)
            {
                switch (this.Dificulty)
                {
                    case 0: if(CheckIfMoveIsValid(x,y)){this.Player.Move(x, y);}
                        break;
                    case 1:  if(CheckIfMoveIsValid(y,x)){this.Player.Move(y, x);}
                        break;
                    case 2: if(CheckIfMoveIsValid(-x,-y)){this.Player.Move(-x, -y);}
                        break;
                    case 3: if (CheckIfMoveIsValid(-y, -x)) { this.Player.Move(-y, -x); }
                        break;
                    case 4: if (CheckIfMoveIsValid(y, -x)) { this.Player.Move(y, -x); }
                        break;
                    case 5: if (CheckIfMoveIsValid(-y, x)) { this.Player.Move(-y, x); }
                        break;
                    case 6: if (CheckIfMoveIsValid(-x, y)) { this.Player.Move(-x, y); }
                        break;
                    case 7: if (CheckIfMoveIsValid(x, -y)) { this.Player.Move(x, -y); }
                        break;
                    default: break;
                }

                if ((this.Player.Left+this.Player.Width) >= this.GameFieldWidth)
                {
                    WinGame();
                }
            }
        }

        

        public bool CheckIfMoveIsValid(double x,double y)
        {
            bool isValid = false;           
            var playX = this.Player.Left +Player.Width/2;
            var playY = this.Player.Top + Player.Height/2;

                if ((playX + x < 0) ||( (playY + y < 0) || (playY + y >= this.GameFieldHeight)))
            {
                return isValid;
            }

            foreach (var wall in this.Walls)
            {
                bool checkA = isPointInWall(playX+x, playY+y, wall);
               
                if (checkA)
                {
                    return isValid;
                }
            }

            isValid = true;
            return isValid;
        }

        public bool isPointInWall(double pointX, double pointY, WallPiece wall)
        {
            bool yes = false;
            if((pointX>wall.Left&&pointX<wall.Left+wall.Width)&&(pointY>wall.Top&&pointY<wall.Top+wall.Height))
            {
            yes=true;
            }
            return yes;
        }

        public void WinGame()
        {

            if (this.Dificulty < 7)
            {
                this.Dificulty++;
                StartGame();
            }
            else
            {
                this.GameStatusCode = GameStatus.Won;
            }
            
        }

        public void PauseGame()
        {
            this.GameStatusCode = GameStatus.Paused;
        }

        public void StartGame()
        {
            this.Player.Top = this.GameFieldHeight / 2 - this.Player.Height / 2;
            this.Player.Left = 0;
            foreach(var wall in this.Walls)
            {
                int maxX = (int)Math.Round(this.GameFieldWidth - wall.Width);
                int maxY = (int)Math.Round(this.GameFieldHeight - wall.Height);

                wall.Top = this.randomizer.Next(0, maxY);
                wall.Left = this.randomizer.Next(200, maxX);
                if (this.WallCount < 10)
                {
                    wall.Height = 70;
                    wall.Width = 70;
                }
            }
           
        }

        public void initObjects()
        {
            int count = ((int)Math.Round(this.GameFieldHeight) / 50) - 1;
            this.WallCount = count;



            for (int i = 0; i < this.WallCount; i++)
            {
                WallPiece wall = new WallPiece();

                
                this.Walls.Add(wall);
            }
        
        }

    }
}
