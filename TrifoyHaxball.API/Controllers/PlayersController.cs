using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrifoyHaxball.API.Filters;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Core.Services;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.API.Controllers
{

    public class PlayersController : CustomBaseController
    {
        private readonly IPlayerService _service;
        public PlayersController(IPlayerService service)
        {
            _service = service;
        }

        [EnableCors("AllowEveryone")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() => CreateActionResult(CustomResponseDto<List<PlayerDto>>.Success(200, (await _service.GetAllAsync()).ToList()));

        [EnableCors("AllowEveryone")]
        [HttpGet("GetAllWithPlayerRankAsync")]
        public async Task<IActionResult> GetAllWithPlayerRankAsync() => CreateActionResult(CustomResponseDto<List<PlayerWithRankDto>>.Success(200, (await _service.GetPlayersWithPlayerRankAsync()).ToList()));

        [EnableCors("AllowEveryone")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(CustomResponseDto<PlayerDto>.Success(200, await _service.GetByIdAsync(id)));

        [EnableCors("AllowEveryone")]
        [HttpGet("GetByIdWithPlayerRankAsync/{id}")]
        public async Task<IActionResult> GetByIdWithPlayerRankAsync(int id) => CreateActionResult(CustomResponseDto<PlayerWithRankDto>.Success(200, await _service.GetByIdWithPlayerRankAsync(id)));

        [ServiceFilter(typeof(CheckWhiteListFilter))]
        [EnableCors("AllowOwner")]
        [HttpPut("LoginAsync")]
        public async Task<IActionResult> LoginAsync(PlayerLoginDto playerLoginDto)
        {
            await _service.LoginAsync(playerLoginDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [ServiceFilter(typeof(CheckWhiteListFilter))]
        [EnableCors("AllowOwner")]
        [HttpPost]
        public async Task<IActionResult> SaveAsync(Player player) => CreateActionResult(CustomResponseDto<Player>.Success(201, await _service.AddAsync(player)));

        [EnableCors("AllowOwner")]
        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync(PlayerSaveDto playerSaveDto) => CreateActionResult(CustomResponseDto<PlayerDto>.Success(201, await _service.RegisterAsync(playerSaveDto)));

        [ServiceFilter(typeof(CheckWhiteListFilter))]
        [EnableCors("AllowOwner")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Player player)
        {
            await _service.UpdateAsync(player);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [ServiceFilter(typeof(CheckWhiteListFilter))]
        [EnableCors("AllowOwner")]
        [HttpPut("UpdatePlayerInformationsAsync")]
        public async Task<IActionResult> UpdatePlayerInfosAsync(PlayerWithRankDto playerWithRankDto)
        {
            await _service.UpdatePlayerInformationsAsync(playerWithRankDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [EnableCors("AllowOwner")]
        [ServiceFilter(typeof(CheckWhiteListFilter))]
        [HttpPut("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync(PlayerChangePasswordDto playerChangePasswordDto)
        {
            await _service.ChangePasswordAsync(playerChangePasswordDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [ServiceFilter(typeof(CheckWhiteListFilter))]
        [EnableCors("AllowOwner")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _service.RemoveAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
