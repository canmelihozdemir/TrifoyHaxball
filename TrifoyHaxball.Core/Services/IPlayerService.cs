using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Core.Services
{
    public interface IPlayerService:IService<Player,PlayerDto>
    {
        Task<Player> GetByNameAsync(string name);
        Task ChangePasswordAsync(PlayerChangePasswordDto playerChangePasswordDt);
        Task<PlayerDto> RegisterAsync(PlayerSaveDto playerSaveDto);
        Task<List<PlayerWithRankDto>> GetPlayersWithPlayerRankAsync();
        Task<PlayerWithRankDto> GetByIdWithPlayerRankAsync(int id);
        Task UpdatePlayerInformationsAsync(PlayerWithRankDto playerWithRankDto);
        Task LoginAsync(PlayerLoginDto playerLoginDto);
    }
}
