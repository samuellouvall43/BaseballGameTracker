using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseballGameTracker.Services;
using BaseballGameTracker.Models.Games;

namespace BaseballGameTracker.Controllers
{
    public class GamesController(IGameService _gameService) : Controller
    {
     
        // GET: Games
        public async Task<IActionResult> Index()
        {

            var viewData = await _gameService.GetAllAsync(); 
            return View(viewData);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameService.Get<GameReadOnlyVM>(id.Value); 

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateVM gameCreate)
        {
            if (ModelState.IsValid)
            {

                await _gameService.Create(gameCreate); 
                return RedirectToAction(nameof(Index));
            }
            return View(gameCreate);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameService.Get<GameEditVM>(id.Value); 
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,GameEditVM gameEdit)
        {
            if (id != gameEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _gameService.Edit(gameEdit); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_gameService.GameExist(gameEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameEdit);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameService.Get<GameReadOnlyVM>(id.Value); 
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _gameService.Remove(id); 
            return RedirectToAction(nameof(Index));
        }

    
    }
}
