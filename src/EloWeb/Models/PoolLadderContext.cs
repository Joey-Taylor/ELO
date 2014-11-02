using System.Data.Entity;

namespace EloWeb.Models
{
    public class PoolLadderContext : DbContext
    {
        public PoolLadderContext() : base("PoolLadderDatabase") { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}