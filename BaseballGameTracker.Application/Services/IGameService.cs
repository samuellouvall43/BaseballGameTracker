using BaseballGameTracker.Application.Models;
using BaseballGameTracker.Models.Games;
using Microsoft.AspNetCore.Mvc;

namespace BaseballGameTracker.Services
{
    public interface IGameService
    {
        Task Create(GameCreateVM model);
        Task Edit(GameEditVM model);

        Task<CompositeVM> GetTodaysGame(int wins, int loses); 

        Task<T?> Get<T>(int id) where T : class; 
        bool GameExist(int id);
        Task<List<GameReadOnlyVM>> GetAllAsync();
        Task Remove(int id);
    }
}