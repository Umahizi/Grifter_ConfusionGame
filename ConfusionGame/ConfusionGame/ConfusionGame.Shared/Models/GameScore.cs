using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionGame.Models
{
    [ParseClassName("GameScore")]
    public class GameScore: ParseObject
    {
        [ParseFieldName("playerName")]
        public string PlayerName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("score")]
        public string Score
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}
