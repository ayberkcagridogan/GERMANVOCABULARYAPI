using GermanVocabularyAPI.Models.Interface;

namespace GermanVocabularyAPI.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsFinished { get; set; }
        public ICollection<CardBase>? Cards { get; set; }
    }
}
