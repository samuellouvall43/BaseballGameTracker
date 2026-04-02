using Quartz;
using Newtonsoft.Json;

namespace BaseballGameTracker.Application.Services
{
    public class JobService: IJob
    {
 
        public async Task Execute(IJobExecutionContext job)
        {
            
            HttpClient client = new HttpClient();

            try
            {

                string data = await client.GetStringAsync("https://api.sportsblaze.com/mlb/v1/boxscores/daily/2026-04-01.json?key=sb1m6fs3e2xfx8sctso07cm&team=St.%20Louis%20Cardinals");

                dynamic json = JsonConvert.DeserializeObject<dynamic>(data);


                if (json.games[0].Count != 0)
                {
                    var game = json.games[0];
                }

               

           
                //Console.WriteLine("Game " + game);
                //Console.WriteLine("Status " + game.status);
                //Console.WriteLine("Home " + game.teams.home);
                //Console.WriteLine("Away " + game.teams.away);
                //Console.WriteLine("Score Home " + game.scores.total.home.runs);
                //Console.WriteLine("Score Away " + game.scores.total.away.runs);



            }
            catch
            {
                Console.WriteLine("API DIDNT WORK"); 
            }


        }
    }
}
