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
using Parse;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ConfusionGame.Views
{
    public sealed partial class WinGamePageContent : UserControl
    {
        
        public string currentScore;
        public WinGamePageContent()
        {
            this.InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Score.Text = this.currentScore;
            ParseObject gameScore = new ParseObject("GameScore");
            gameScore["score"] = this.currentScore;
            gameScore["playerName"] = this.userName.Text;
            gameScore.SaveAsync();
        }
    }
}
