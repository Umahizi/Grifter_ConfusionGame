using ConfusionGame.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConfusionGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private const int NumberOfColumns = 4;

        private const int NumberOfFrames = 12;

        private const int FrameWidth = 175;

        private const int FrameHeight = 175;

        public static readonly TimeSpan TimePerFrame = TimeSpan.FromSeconds(2.0);

        private int currentFrame;

        private TimeSpan timeTillNextFrame;

       // private PlayerViewModel player;

        private GameViewModel game;
        
        private Rectangle controll;

        private ImageBrush brush;
        

        public GamePage()
        {
   
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            
            this.game = new GameViewModel();
          //  this.player = new PlayerViewModel(x,y);
            
          
            
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            CompositionTarget.Rendering += this.OnUpdate;

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void OnUpdate(object sender, object e)
        {
            this.timeTillNextFrame += TimeSpan.FromSeconds(1 / 60f);
            if (this.timeTillNextFrame > TimePerFrame)
            {
                this.currentFrame = (this.currentFrame + 1 + NumberOfFrames) % NumberOfFrames;
                var column = this.currentFrame % NumberOfColumns;
                var row = this.currentFrame / NumberOfColumns;
                TranslateTransform x = new TranslateTransform();
                x.X = -column * FrameWidth;
                x.Y = -row * FrameHeight;

                this.brush.Transform = x;
                //this.SpriteSheetOffset.X = -column * FrameWidth;
                //this.SpriteSheetOffset.Y = -row * FrameHeight;
            }
        }

       /* private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var rect = (sender as Rectangle);
            var delta = e.Delta;

            TranslateElipse(rect, delta);
        }*/

        private void TranslateObject( Windows.UI.Input.ManipulationDelta delta)
        {
            var translration = delta.Translation;
            var left = Canvas.GetLeft(this.Rect) + translration.X;
            var top = Canvas.GetTop(this.Rect) + translration.Y;

                this.game.MovePlayer(translration.X, translration.Y);
            
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            this.game.GameFieldWidth = this.ActualWidth;
            this.game.GameFieldHeight = this.ActualHeight;

           // SetPlayerControll();
            this.game.StartGame();
            GenerateObsticles();
            BuildGameUIElement(this.game.Player);
            
           /* Button btn = new Button() { Content = "Button" };
            btn.Width = 130;
            btn.Height = 66;

            this.Grid.Children.Add(btn);*/
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += (snd, args) =>
            //{
            //    (this.DataContext as MainPageViewModel).StartGame();
            //};

            //timer.Interval = TimeSpan.FromSeconds(2);
            //timer.Start();
        }

        private void GenerateObsticles()
        { 
            foreach(var wall in this.game.Walls)
            {
                BuildGameUIElement(wall);
            }
        }
      

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var delta = e.Delta;

            TranslateObject( delta);
        }

        /*private void SetPlayerControll() 
        {
            var topBinding = new Binding()
            {
                Path = new PropertyPath("Top"),
                Source = this.game.Player,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var leftBinding = new Binding()
            {
                Path = new PropertyPath("Left"),
                Source = this.game.Player,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            TranslateTransform x = new TranslateTransform();
            x.X = -175;
            x.Y = -175;
            this.brush = new ImageBrush()
            {
                ImageSource = this.game.Player.Source,
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Transform = x
            };

            this.controll = this.Rect;
            this.controll.SetBinding(Canvas.TopProperty, topBinding);
            this.controll.SetBinding(Canvas.LeftProperty, leftBinding);
            this.controll.Width = this.game.Player.Width;
            this.controll.Height = this.game.Player.Height;

            this.controll.Fill = this.brush;
           
        
        }*/

        private Rectangle BuildGameUIElement(GameObjectVM obj)
        {
            var topBinding = new Binding()
            {
                Path = new PropertyPath("Top"),
                Source = obj,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var leftBinding = new Binding()
            {
                Path = new PropertyPath("Left"),
                Source = obj,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            Rectangle rect = new Rectangle();
            rect.Width = obj.Width;
            rect.Height = obj.Width;
            TranslateTransform x = new TranslateTransform();
            x.X = -obj.Width;
            x.Y = -obj.Height;

            this.brush = new ImageBrush()
            {
                ImageSource = obj.Source,
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Transform = x
            };

            this.Field.Children.Add(rect);
            rect.SetBinding(Canvas.TopProperty, topBinding);
            rect.SetBinding(Canvas.LeftProperty, leftBinding);
            rect.Width = this.game.Player.Width;
            rect.Height = this.game.Player.Height;

            rect.Fill = this.brush;
            

            return rect;
            
        }

        private void Dificulty_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (this.game.Dificulty < 3)
            {
                this.game.Dificulty++;
                switch (this.game.Dificulty)
                {
                    case 1: button.Content = "Harder"; break;
                    case 2: button.Content = "Mutch Harder"; break;
                    case 3: button.Content = "Insane"; break;
                    default: break;
                }
            }
        }

        
    }
}
