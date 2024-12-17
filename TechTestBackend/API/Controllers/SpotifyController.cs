using Microsoft.AspNetCore.Mvc;
using TechTestBackend.Domain.Interfaces;

namespace TechTestBackend.API.Controllers;

[ApiController]
[Route("api/spotify")]
public class SpotifyController(ISongsService songsService) : ControllerBase
{
    [HttpGet]
    [Route("searchTracks")]
    public async Task<IActionResult> SearchTracks(string name)
    {
        var tracks = await songsService.GetTracksByNameAsync(name);
        return Ok(tracks);
    }

    [HttpPost]
    [Route("like")]
    public async Task<IActionResult> Like(string id)
    {
        await songsService.AddTrackToLikedAsync(id);
        return Ok();
    }
    
    [HttpPost]
    [Route("removeLike")]
    public async Task<IActionResult> RemoveLike(string id)
    {
        await songsService.RemoveTrackFromLikedAsync(id);
        return Ok();
    }
    
    [HttpGet]
    [Route("listLiked")]
    public async Task<IActionResult> ListLiked()
    {
        var tracks = await songsService.GetAllLikedTracksAsync();
        return Ok(tracks);
    }
}