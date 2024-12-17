using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechTestBackend.Domain.Interfaces;
using TechTestBackend.Domain.Models;

namespace TechTestBackend.Persistence;

public class SongsRepository(SongStorageContext dbContext, ILogger<SongsRepository> logger) : ISongsRepository
{
    public async Task<SpotifySongModel?> GetTrackById(string id)
    {
        return await dbContext.Songs.Where(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddTrackToLickedAsync(SpotifySongModel song)
    {
        var existingSong = dbContext.Songs.FirstOrDefault(s => s.Id == song.Id);
        if (existingSong != null)
        {
            logger.LogInformation("The song has already been liked.");
            return;
        }
        
        try
        {
            dbContext.Songs.Add(song);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }

    public async Task RemoveTrackFromLickedAsync(SpotifySongModel song)
    {
        try
        {
            dbContext.Songs.Remove(song);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }

    public Task<List<SpotifySongModel>> GetAllLikedTracksAsync()
    {
        return dbContext.Songs.ToListAsync();
    }
}