using Microsoft.AspNetCore.Mvc;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Web.Services;

namespace TrifoyHaxball.Web.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayerApiService _playerApiService;

        public PlayersController(PlayerApiService playerApiService)
        {
            _playerApiService = playerApiService;
        }

        public async Task<IActionResult> Index()
        {
            var p = await _playerApiService.GetAllWithPlayerRankAsync();
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(PlayerSaveDto playerSaveDto)
        {
            if (ModelState.IsValid)
            {
                await _playerApiService.RegisterAsync(playerSaveDto);
            }
            return View(playerSaveDto);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PlayerChangePasswordDto playerChangePasswordDto)
        {
            if (ModelState.IsValid)
            {
                await _playerApiService.ChangePasswordAsync(playerChangePasswordDto);
            }
            return View(playerChangePasswordDto);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _playerApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
