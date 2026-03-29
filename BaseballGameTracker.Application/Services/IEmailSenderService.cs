using BaseballGameTracker.Models.Games;

namespace BaseballGameTracker.Services
{
    public interface IEmailSenderService
    {
        void SendEmail(GameCreateVM email);
    }
}