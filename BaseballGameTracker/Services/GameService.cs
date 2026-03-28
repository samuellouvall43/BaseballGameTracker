using AutoMapper;
using BaseballGameTracker.Data;
using BaseballGameTracker.Models.Games;
using Microsoft.EntityFrameworkCore;

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
