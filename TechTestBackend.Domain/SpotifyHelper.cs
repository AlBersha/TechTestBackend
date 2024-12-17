using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TechTestBackend.Domain.Interfaces;
using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain;

public class SpotifyHelper : ISpotifyHelper
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SpotifyHelper> _logger;
    
    private const string SpotifyApiUrl = "https://api.spotify.com/v1";
    private const string AccountsSpotifyApiUrl = "https://accounts.spotify.com/api";

    public SpotifyHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<SpotifyHelper> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _logger = logger;
    }
    
    public async Task<List<SpotifySongModel>> GetTracksByNameAsync(string name)
    {
        var password = await GetPassword();
        if (string.IsNullOrEmpty(password))
        {
            _logger.LogError("Fail to get tracks - password is empty.");
            return new List<SpotifySongModel>();
        }
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", password);
        
        var response = await _httpClient.GetAsync(SpotifyApiUrl + "/search?q=" + name + "&type=track");
        if (!response.IsSuccessStatusCode) return new List<SpotifySongModel>();
        
        var responseString = await response.Content.ReadAsStringAsync();
        dynamic songsObjects = JsonConvert.DeserializeObject(responseString);
        var songs = JsonConvert.DeserializeObject<List<SpotifySongModel>>(songsObjects.tracks.items.ToString());

        return songs;

    }

    private async Task<string?> GetPassword()
    {        
        var base64 = GetAuthorizationString();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);

        var passwordRequest = await _httpClient.PostAsync(AccountsSpotifyApiUrl + "/token", 
            new FormUrlEncodedContent(new [] { new KeyValuePair<string, string>("grant_type", "client_credentials") }));
        if (!passwordRequest.IsSuccessStatusCode) return string.Empty;
        
        dynamic password = JsonConvert.DeserializeObject(await passwordRequest.Content.ReadAsStringAsync());
        return password?.access_token.ToString();
    }
    
    private string GetAuthorizationString()
    {
        var clientId = _configuration["SpotifyApiKeys:ClientID"];
        var clientSecret = _configuration["SpotifyApiKeys:ClientSecret"];
        
        var encoded = Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}");
        return Convert.ToBase64String(encoded);
    }
    
    public async Task<SpotifySongModel> GetTrackById(string id)
    {
        var password = await GetPassword();
        if (string.IsNullOrEmpty(password))
        {
            _logger.LogError("Fail to get tracks - password is empty.");
            return new SpotifySongModel();
        }
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", password);

        var response = _httpClient.GetAsync(SpotifyApiUrl + "/tracks/" + id + "/").Result;
        dynamic objects = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

        var song = JsonConvert.DeserializeObject<SpotifySongModel>(objects.ToString());
        
        return song;
    }
}