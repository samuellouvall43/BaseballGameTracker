using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseballGameTracker.Data
{
    public class Email
    {

        public int Id { get; set; }


        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
    }
}
