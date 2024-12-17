using Microsoft.EntityFrameworkCore;
using TechTestBackend.Domain.Models;

namespace TechTestBackend.Persistence;

public class SongStorageContext : DbContext
{
    public SongStorageContext(DbContextOptions<SongStorageContext> options)
        : base(options)
    {
    }

    public DbSet<SpotifySongModel> Songs { get; set; }
}