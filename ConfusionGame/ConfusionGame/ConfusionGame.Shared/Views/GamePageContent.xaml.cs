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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ConfusionGame.Views
{
    public sealed partial class GamePageContent : UserControl
    {
        private const int NumberOfColumns = 4;

        private const int NumberOfFrames = 12;

        private const int FrameWidth = 175;

        private const int FrameHeight = 175;

        public static readonly TimeSpan TimePerFrame = TimeSpan.FromSeconds(2.0);

        private int currentFrame;

        private TimeSpan timeTillNextFrame;

        public GamePageContent()
        {
            this.InitializeComponent();
            
        }

        private void OnUpdate(object sender, object e)
        {
            this.timeTillNextFrame += TimeSpan.FromSeconds(1 / 60f);
            if (this.timeTillNextFrame > TimePerFrame)
            {
                this.currentFrame = (this.currentFrame + 1 + NumberOfFrames) % NumberOfFrames;
                var column = this.currentFrame % NumberOfColumns;
                var row = this.currentFrame / NumberOfColumns;

                this.SpriteSheetOffset.X = -column * FrameWidth;
                this.SpriteSheetOffset.Y = -row * FrameHeight;
            }
        }

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var rect = (sender as Rectangle);
            var delta = e.Delta;

            TranslateElipse(rect, delta);
           
        }

        private void TranslateElipse(Rectangle rect, Windows.UI.Input.ManipulationDelta delta)
        {
            var translration = delta.Translation;
            var left = Canvas.GetLeft(rect) + translration.X;
            var top = Canvas.GetTop(rect) + translration.Y;

            Canvas.SetLeft(rect, left);

            Canvas.SetTop(rect, top);
        }

        private void Rectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            CompositionTarget.Rendering -= this.OnUpdate;
        }

        private void Rectangle_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            CompositionTarget.Rendering += this.OnUpdate;
        }
    }
}
