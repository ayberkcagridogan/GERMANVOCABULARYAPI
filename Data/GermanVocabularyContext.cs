using GermanVocabularyAPI.Models;
using GermanVocabularyAPI.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabularyAPI.Data
{
    public class GermanVocabularyContext : DbContext
    {
        public GermanVocabularyContext(DbContextOptions<GermanVocabularyContext> options) : base(options)
        {
        }

        public DbSet<CardBase> CardBases {get; set;}
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Noun> Nouns { get; set; }
        public DbSet<Verb> Verbs { get; set; }
        public DbSet<OtherNoun> OtherNouns { get; set; }
        public DbSet<Adjective> Adjectives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardBase>()
                        .HasDiscriminator<string>("CardType")
                        .HasValue<CardBase>("Base")
                        .HasValue<Noun>("Noun")
                        .HasValue<Verb>("Verb")
                        .HasValue<OtherNoun>("OtherNoun")
                        .HasValue<Adjective>("Adjective");
                        
            modelBuilder.Entity<CardBase>()
                        .HasIndex(a => a.GermanWord)
                        .IsUnique();

            modelBuilder.Entity<Deck>()
                        .HasKey(d => d.Id);
            
            modelBuilder.Entity<Deck>()
                        .HasMany(d => d.Cards)
                        .WithOne(c => c.Deck)
                        .HasForeignKey(c => c.DeckId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
