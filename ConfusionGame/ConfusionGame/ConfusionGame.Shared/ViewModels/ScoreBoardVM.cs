using ConfusionGame.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using ConfusionGame.Common;

namespace ConfusionGame.ViewModels
{
    public class ScoreBoardVM:BaseVM
    {
        public ObservableCollection<ScoreVM> scores;
        private ICommand refreshCommand;

        public IEnumerable<ScoreVM> Scores
        {
            get
            {
                if (this.scores == null)
                {
                    this.scores = new ObservableCollection<ScoreVM>();
                }
                return this.scores;
            }
            set
            {
                if (this.scores == null)
                {
                    this.scores = new ObservableCollection<ScoreVM>();
                }
                this.scores.Clear();
                foreach (var item in value)
                {
                    this.scores.Add(item);
                }
            }
        }

        public ScoreBoardVM()
        {
            this.FetchData();
        }

        public ICommand Refresh
        {
            get
            {
                if (this.refreshCommand == null)
                {
                    this.refreshCommand = new RelayCommand(this.PerformRefresh);
                }
                return this.refreshCommand;
            }
        }

        private void PerformRefresh()
        {
            this.FetchData();
        }

        //TODO parse object

        private async Task FetchData()
        {
            /*var query = from gameScore in ParseObject.GetQuery("GameScore")
                        orderby ("score") descending
                        select gameScore;
            query = query.Limit(10);*/

            var scores = await new ParseQuery<GameScore>()
                .FindAsync(
                CancellationToken.None);

            this.Scores = scores.AsQueryable().OrderByDescending(x => x.Score).Take(10)
                .Select(ScoreVM.FromModel);
           
        }
    }
}
