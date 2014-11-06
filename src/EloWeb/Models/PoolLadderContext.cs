using System.Data.Entity;

namespace EloWeb.Models
{
    public class PoolLadderContext : DbContext
    {
        public PoolLadderContext() : base("PoolLadderDatabase") { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasRequired<Player>(g => g.Winner)
                .WithMany(p => p.Wins)
                .HasForeignKey(g => g.WinnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired<Player>(g => g.Loser)
                .WithMany(p => p.Losses)
                .HasForeignKey(g => g.LoserId)
                .WillCascadeOnDelete(false);   
        } 
    }
}