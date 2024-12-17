using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain.Interfaces;

public interface ISongsRepository
{
    Task<SpotifySongModel?> GetTrackById(string id);
    Task<List<SpotifySongModel>> GetAllLikedTracksAsync();
    Task AddTrackToLikedAsync(SpotifySongModel song);
    Task RemoveTrackFromLikedAsync(SpotifySongModel song);
}