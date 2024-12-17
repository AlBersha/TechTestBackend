using Microsoft.EntityFrameworkCore;
using TechTestBackend.Domain.Models;

namespace TechTestBackend.Persistence;

public class SongStorageContext(DbContextOptions<SongStorageContext> options) : DbContext(options)
{
    public DbSet<SpotifySongModel> Songs { get; set; }
}