using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Core.Repositories;
using TrifoyHaxball.Core.Services;
using TrifoyHaxball.Core.UnitOfWorks;
using TrifoyHaxball.Entity;
using TrifoyHaxball.Service.Exceptions;

namespace TrifoyHaxball.Service.Services
{
    public class PlayerService : Service<Player, PlayerDto>, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IGenericRepository<Player> repository, IUnitOfWork unitOfWork, IMapper mapper, IPlayerRepository playerRepository, ILogger<PlayerService> logger) : base(repository, unitOfWork, mapper)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }




        public async Task ChangePasswordAsync(PlayerChangePasswordDto playerChangePasswordDto)
        {
            var player=await GetByNameAsync(playerChangePasswordDto.Name);
            if (player!=null)
            {
                if (player.Password==playerChangePasswordDto.Password)
                {
                    player.Password = playerChangePasswordDto.NewPassword;
                    await UpdateAsync(player);
                    _logger.LogInformation($"İşlem: Şifre değişikliği   -   Oyuncu: {playerChangePasswordDto.Name}");
                    return;
                }
                else
                {
                    throw new ClientSideException("Girmiş olduğunuz eski şifre geçerli değildir!"); 
                }
            }
            throw new NotFoundException("Böyle bir kullanıcı adı bulunamadı!");
        }

        public async Task<Player> GetByNameAsync(string name)
        {
            return await _playerRepository.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<PlayerDto> RegisterAsync(PlayerSaveDto playerSaveDto)
        {
            var isPlayerAlreadyExist = await GetByNameAsync(playerSaveDto.Name);
            if (isPlayerAlreadyExist!=null)
            {
                throw new ClientSideException("Kullanıcı adı zaten kayıtlı durumdadır!");
            }

            var player = _mapper.Map<Player>(playerSaveDto);
            player.Role = "player";
            var newPlayer=await AddAsync(player);
            await _playerRepository.AddPlayerRankAsync(newPlayer.Id);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation($"İşlem: Yeni kayıt   -   Oyuncu: {playerSaveDto.Name}");
            return _mapper.Map<PlayerDto>(player);
        }


        public async Task<List<PlayerWithRankDto>> GetPlayersWithPlayerRankAsync()
        {
            var playersWithRank = await _playerRepository.GetPlayersWithRanksAsync();
            return _mapper.Map<List<PlayerWithRankDto>>(playersWithRank);
        }

        public async Task<PlayerWithRankDto> GetByIdWithPlayerRankAsync(int id)
        {
            var playerWithRank=await _playerRepository.GetByIdWithPlayerRankAsync(id);
            if (playerWithRank is null)
            {
                throw new NotFoundException($"{typeof(Player).Name}({id}) bulunamadı!");
            }
            return _mapper.Map<PlayerWithRankDto>(playerWithRank);
        }

        public async Task UpdatePlayerInformationsAsync(PlayerWithRankDto playerWithRankDto)
        {
            var player = await GetByNameAsync(playerWithRankDto.Name);
            if (player == null)
            {
                throw new NotFoundException("Böyle bir kullanıcı adı bulunamadı!");
            }

            playerWithRankDto.PlayerRank.Id=player.Id;
            var isPlayerHasRank = await _playerRepository.CheckIfPlayerHasRankAsync(player.Id);
            if (isPlayerHasRank)
            {
                await _unitOfWork.CommitAsync();
            }


            await _playerRepository.UpdatePlayerRankAsync(playerWithRankDto.PlayerRank);
            await _unitOfWork.CommitAsync();
            player.Coin = playerWithRankDto.Coin;
            player.PlayedGameCount = playerWithRankDto.PlayedGameCount;
            player.HighScore = playerWithRankDto.HighScore;
            player.Role = playerWithRankDto.Role;
            player.UpdatedDate = new DateTime();
            await UpdateAsync(player);

        }

        public async Task LoginAsync(PlayerLoginDto playerLoginDto)
        {
            var player = await GetByNameAsync(playerLoginDto.Name);
            if (player!=null)
            {
                if (player.Password!= playerLoginDto.Password)
                {
                    throw new ClientSideException("Girdiğiniz parola geçerli değildir!");
                }
                _logger.LogInformation($"İşlem: Sunucuya giriş   -   Oyuncu: {playerLoginDto.Name}");
                return;
            }
            throw new NotFoundException("Böyle bir kullanıcı bulunamadı!");
        }
    }
}
