﻿using ConfusionGame.Common;
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
        private NavigationHelper navigationHelper;
        

        public GamePage()
        {
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
   
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            
            this.game = new GameViewModel();
          //  this.player = new PlayerViewModel(x,y);

            this.DataContext = this.game;
            
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            CompositionTarget.Rendering += this.OnUpdate;
            
            this.game.Dificulty = 0;
            this.game.StartGame();

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
   
            if (this.game.GameStatusCode != GameStatus.Won)
            {
                
                this.game.initObjects();
                GenerateObsticles();
                BuildGameUIElement(this.game.Player);

            }
            else
            {
                this.game.GameStatusCode = GameStatus.On;
            }
            this.game.StartGame();
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

            var heightBinding = new Binding()
            {
                Path = new PropertyPath("Height"),
                Source = obj,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var widthBinding = new Binding()
            {
                Path = new PropertyPath("Width"),
                Source = obj,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };


            Rectangle rect = new Rectangle();
            
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
            rect.SetBinding(WidthProperty, widthBinding);
            rect.SetBinding(HeightProperty, heightBinding);
            rect.Fill = this.brush;
            

            return rect;
            
        }

        private void GameStatus_Click(object sender, RoutedEventArgs e)
        {
            if (this.game.GameStatusCode == GameStatus.Won)
            {
                CompositionTarget.Rendering -= this.OnUpdate;
                //PhoneApplicationService.Current.State["MyObject"] = this.game.Score;
                
                this.Frame.Navigate(typeof(WinGamePage),this.game.Score);
            }
            else 
            {
                this.game.PauseGame();
            }
        }


        
    }
}
