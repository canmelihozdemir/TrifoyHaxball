using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Core.Repositories;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Repository.Repositories
{
    public class PlayerRepository : GenericRepository<Player>,IPlayerRepository
    {
        public PlayerRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<List<Player>> GetPlayersWithRanksAsync()
        {
            var players = await _context.Players.Include(x => x.PlayerRank).ToListAsync();
            return players;
        }

        public async Task<Player> GetByIdWithPlayerRankAsync(int id)
        {
            var playerWithRank = await _context.Players.Where(p=>p.Id==id).Include(x => x.PlayerRank).FirstOrDefaultAsync();
            return playerWithRank;
        }

        public async Task<PlayerRank> AddPlayerRankAsync(int id)
        {
           var playerRank=await _context.PlayerRanks.AddAsync(new PlayerRank() { Name="Yeni",Point=0,PlayerId=id});
            return playerRank.Entity;
        }

        public async Task UpdatePlayerRankAsync(PlayerRankDto playerRankDto)
        {
            var player = await _context.PlayerRanks.FirstOrDefaultAsync(p => p.PlayerId == playerRankDto.Id);
            player.Name = playerRankDto.Name;
            player.Point = playerRankDto.Point;
            _context.PlayerRanks.Update(player);
        }

        public async Task<bool> CheckIfPlayerHasRankAsync(int id)
        {
            var player = await _context.PlayerRanks.FirstOrDefaultAsync(p => p.PlayerId == id);
            if (player == null)
            {
                player = await AddPlayerRankAsync(id);
                return true;
            }
            return false;
        }
    }
}
