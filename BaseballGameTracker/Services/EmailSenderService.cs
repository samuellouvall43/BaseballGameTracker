using BaseballGameTracker.Data;
using BaseballGameTracker.Models.Games;
using MailKit;
using MailKit.Net;
using MailKit.Net.Smtp;
using MimeKit;

namespace BaseballGameTracker.Services
{
    public class EmailSenderService(ApplicationDbContext _context) : IEmailSenderService
    {
        public void SendEmail(GameCreateVM game)
        {

            //Get all emails. 

            var emails = _context.Email.ToList();


              
            // Set up email message
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Baseball Tracker", "baseballtracker03@gmail.com"));
          
            message.Subject = "Score of game";


            var msgString = "";

            if (game.CardinalsScore > game.OpposingTeamScore)
            {
                msgString = "Cardinals Win against " + game.OpponentName + " with a score of " + game.CardinalsScore + " - " + game.OpposingTeamScore;
            }
            else
            {
                msgString = "Cardinals Lost to the " + game.OpposingTeamScore + " with a score of " + game.OpposingTeamScore + " - " + game.CardinalsScore;
            }

            // Need to add code to implement to make sure they add correct emails. 
            foreach (var email in emails) {
                message.Bcc.Add(MailboxAddress.Parse(email.EmailAddress));
               
            }
            
            message.Body = new TextPart("plain")
            {
                Text = msgString

            }; 
            
            SmtpClient smtpClient = new SmtpClient();

            try
            {
               
                //Connect to smtp server using port 465 with SSL enabled 
                smtpClient.Connect("smtp.gmail.com", 465, true);
                //Google Apps password need to find a way to hide this securly. 
                //nymi ccqh wlfy ehuz
                smtpClient.Authenticate("baseballtracker03@gmail.com", "nymi ccqh wlfy ehuz");

                smtpClient.Send(message);

                Console.WriteLine("Email Sent!");


            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Disconnect
                smtpClient.Disconnect(true);
                //Dispose
                smtpClient.Dispose();
            }



        }
    }
}
