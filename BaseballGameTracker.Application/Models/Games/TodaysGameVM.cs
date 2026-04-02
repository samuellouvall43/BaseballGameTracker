using System;
using System.Collections.Generic;
using System.Text;

namespace BaseballGameTracker.Application.Models.Games
{
    public class TodaysGameVM
    {

        public string Status;
        public string Cardinals; 
        public string Opponent;
        public int CardinalsRuns; 
        public int OpponentRuns;
        public DateOnly TodaysDate;
        public bool IsGameToday; 



    }
}
