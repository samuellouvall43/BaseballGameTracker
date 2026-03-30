using System.ComponentModel.DataAnnotations;

namespace BaseballGameTracker.Models.Games
{
    public class GameReadOnlyVM : BaseGameVM
    {

        [Display(Name = "Cardinals Score")]
        public int CardinalsScore { get; set; }


        [Display(Name = "Opponent Score")]
        public int OpposingTeamScore { get; set; }


        [Display(Name = "Opponent Name")]
        public string OpponentName { get; set; }
    }
}
