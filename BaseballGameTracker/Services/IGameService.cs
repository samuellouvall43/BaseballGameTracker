using BaseballGameTracker.Models.Games;

namespace BaseballGameTracker.Services
{
    public interface IGameService
    {
        Task Create(GameCreateVM model);
        Task Edit(GameEditVM model);

        Task<T?> Get<T>(int id) where T : class; 
        bool GameExist(int id);
        Task<List<GameReadOnlyVM>> GetAllAsync();
        Task Remove(int id);
    }
}