using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain.Interfaces;

public interface ISongsRepository
{
    Task<SpotifySongModel?> GetTrackById(string id);
    Task AddTrackToLickedAsync(SpotifySongModel song);
    Task RemoveTrackFromLickedAsync(SpotifySongModel song);
    Task<List<SpotifySongModel>> GetAllLikedTracksAsync();
}