using ConfusionGame.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ConfusionGame.ViewModels
{
   public class ScoreVM
    {
       public static Expression<Func<GameScore, ScoreVM>> FromModel
       {
           get
           {
               return model =>
                   new ScoreVM()
                   {
                       Name = model.PlayerName,
                       Score = model.Score 
                   };
           }
       }
        public string Name { get; set; }
        public string Score { get; set; }
    }
}
