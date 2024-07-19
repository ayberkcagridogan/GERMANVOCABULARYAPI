using GermanVocabularyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabularyAPI.Data
{
    public class GermanVocabularyContext : DbContext
    {
        public GermanVocabularyContext(DbContextOptions<GermanVocabularyContext> options) : base(options)
        {
        }

        public DbSet<Deck> Decks { get; set; }
        public DbSet<Noun> Nouns { get; set; }
        public DbSet<Verb> Verbs { get; set; }
        public DbSet<OtherNoun> OtherNouns { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Adjective> Adjectives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Noun>()
                .HasOne(n => n.Card)
                .WithMany(c => c.Nouns)
                .HasForeignKey(n => n.CardId);

            modelBuilder.Entity<Verb>()
                .HasOne(v => v.Card)
                .WithMany(c => c.Verbs)
                .HasForeignKey(v => v.CardId);

            modelBuilder.Entity<OtherNoun>()
                .HasOne(o => o.Card)
                .WithMany(c => c.OtherNouns)
                .HasForeignKey(o => o.CardId);

            modelBuilder.Entity<Adjective>()
                .HasOne(a => a.Card)
                .WithMany(c => c.Adjectives)
                .HasForeignKey(o => o.CardId);

        }
    }
}
