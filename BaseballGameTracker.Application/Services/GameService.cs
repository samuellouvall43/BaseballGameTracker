using AutoMapper;
using BaseballGameTracker.Application.Models;
using BaseballGameTracker.Application.Models.Games;
using BaseballGameTracker.Data;
using BaseballGameTracker.Models;
using BaseballGameTracker.Models.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;


namespace BaseballGameTracker.Services
{
    public class GameService(ApplicationDbContext _context, IMapper _mapper, IEmailSenderService _emailSender) : IGameService
    {

        public async Task<List<GameReadOnlyVM>> GetAllAsync()
        {
            var data = await _context.Game.ToListAsync();
            var viewData = _mapper.Map<List<GameReadOnlyVM>>(data);

            return viewData;
        }


        public async Task<CompositeVM> GetTodaysGame(int wins, int loses)
        {
            HttpClient client = new HttpClient();
           

            try
            {

                var today = DateOnly.FromDateTime(DateTime.Now);
              

                var todayString = DateTime.Now.ToString("yyyy-MM-dd");
              

                var api = "https://api.sportsblaze.com/mlb/v1/boxscores/daily/" + todayString + ".json?key=sb1m6fs3e2xfx8sctso07cm&team=St.%20Louis%20Cardinals";
                //var api = "https://api.sportsblaze.com/mlb/v1/boxscores/daily/2026-04-02.json?key=sb1m6fs3e2xfx8sctso07cm&team=St.%20Louis%20Cardinals";

                string data = await client.GetStringAsync(api);

                dynamic json = JsonConvert.DeserializeObject<dynamic>(data);
     ;

                var IsGameToday = false;

                if (json.games.Count != 0)
                {
                    IsGameToday = true;


                    var cardsScore = 0;
                    var oppScore = 0;
                    var opponent = "";

                    var game = json.games[0];

                    if (game.teams.home.name == "St. Louis Cardinals")
                    {


                        cardsScore = game.scores.total.home.runs;
                        oppScore = game.scores.total.away.runs;
                        opponent = game.teams.away.name;
                    }
                    else
                    {
                        oppScore = game.scores.total.home.runs;
                        cardsScore = game.scores.total.away.runs;
                        opponent = game.teams.home.name;

                    }

                    var TodaysGame = new TodaysGameVM
                    {
                        Status = game.status,
                        Cardinals = "St. Louis Cardinals",
                        Opponent = opponent,
                        CardinalsRuns = cardsScore,
                        OpponentRuns = oppScore,
                        TodaysDate = today,
                        IsGameToday = IsGameToday
                    };


                    var recordAsOfToday = new RecordVM
                    {
                        Wins = wins,
                        Loses = loses
                    };

                    var compositeVM = new CompositeVM
                    {
                        TodaysGame = TodaysGame,
                        Record = recordAsOfToday

                    };
                    return compositeVM;

                }
                else
                {

                    // No game today 
                    var NoGame = new TodaysGameVM
                    {
                        Status = "",
                        Cardinals = "St. Louis Cardinals",
                        Opponent = "",
                        CardinalsRuns = 0,
                        OpponentRuns = 0,
                        TodaysDate = today,
                        IsGameToday = IsGameToday
                    };


                    var record = new RecordVM
                    {
                        Wins = wins,
                        Loses = loses
                    };

                    var NoGameComposite = new CompositeVM
                    {
                        TodaysGame = NoGame,
                        Record = record

                    };
                 
                    return NoGameComposite;
                }


            }
            catch
            {

                /// API didn't work. 
               

            }


            // No game today 
            var notWorking = new TodaysGameVM
            {
                Status = "",
                Cardinals = "St. Louis Cardinals",
                Opponent = "",
                CardinalsRuns = 0,
                OpponentRuns = 0,
                TodaysDate = DateOnly.FromDateTime(DateTime.Now),
                IsGameToday = false
            };


            var rec = new RecordVM
            {
                Wins = wins,
                Loses = loses
            };

            var notWorkingWM = new CompositeVM
            {
                TodaysGame = notWorking,
                Record = rec

            };
            Console.WriteLine("NOT WORKING !"); 
            return notWorkingWM;

        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var data = await _context.Game.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task Remove(int id)
        {
            var data = await _context.Game.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(GameEditVM model)
        {
            var game = _mapper.Map<Game>(model);
            _context.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task Create(GameCreateVM model)
        {

            //Adding some test logic so when we create a game it sends an email. 
            

            Console.WriteLine("Sending EMAIL!!!");
             _emailSender.SendEmail(model); 

            var game = _mapper.Map<Game>(model);
            _context.Add(game);
            await _context.SaveChangesAsync();
        }

        public bool GameExist(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }

}
