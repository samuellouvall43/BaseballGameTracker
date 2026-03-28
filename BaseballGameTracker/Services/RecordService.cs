using BaseballGameTracker.Data;
using BaseballGameTracker.Models.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace BaseballGameTracker.Services
{
    public class RecordService(ApplicationDbContext _context) : IRecordService
    {

        public int CalulateWins()
        {
            var data = _context.Game.ToList();

            int wins = 0;

            foreach (var game in data)
            {
                if (game.CardinalsScore > game.OpposingTeamScore)
                {
                    wins++;
                }

            }



            return wins;

        }

        public int CalculateLoses()
        {
            var data = _context.Game.ToList();

            int loses = 0;

            foreach (var game in data)
            {
                if (game.CardinalsScore < game.OpposingTeamScore)
                {
                    loses++;
                }

            }

            return loses;
        }



    }
}
