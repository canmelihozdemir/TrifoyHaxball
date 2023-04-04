using System.Net.Http.Json;
using TrifoyHaxball.Core.DTOs;

namespace TrifoyHaxball.Web.Services
{
    public class PlayerApiService
    {
        private readonly HttpClient _httpClient;

        public PlayerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlayerDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<PlayerDto>>>("Players");
            
            return response.Data;
        }

        public async Task<List<PlayerWithRankDto>> GetAllWithPlayerRankAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<PlayerWithRankDto>>>("Players/GetAllWithPlayerRankAsync");

            return response.Data;
        }

        public async Task<PlayerDto> RegisterAsync(PlayerSaveDto playerSaveDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Players/RegisterAsync", playerSaveDto);

            if (!response.IsSuccessStatusCode) return null;
            
            var responseBody=await response.Content.ReadFromJsonAsync<CustomResponseDto<PlayerDto>>();

            return responseBody.Data;
        }

        public async Task<PlayerDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<PlayerDto>>($"Players/{id}");
            return response.Data;
        }

        public async Task<bool> ChangePasswordAsync(PlayerChangePasswordDto playerChangePasswordDto)
        {
            var response = await _httpClient.PutAsJsonAsync("Players/ChangePasswordAsync", playerChangePasswordDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Players/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
