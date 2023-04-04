using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Core.Repositories
{
    public interface IPlayerRepository:IGenericRepository<Player>
    {
        Task<List<Player>> GetPlayersWithRanksAsync();
        Task<PlayerRank> AddPlayerRankAsync(int id);
        Task<Player> GetByIdWithPlayerRankAsync(int id);
        Task UpdatePlayerRankAsync(PlayerRankDto playerRankDto);
        Task<bool> CheckIfPlayerHasRankAsync(int id);

    }
}
