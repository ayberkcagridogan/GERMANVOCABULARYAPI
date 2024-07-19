namespace GermanVocabularyAPI.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int SuccessRate { get; set; }
        public bool IsFinished { get; set; }
        public ICollection<Noun> Nouns { get; set; }
        public ICollection<Verb> Verbs { get; set; }
        public ICollection<OtherNoun> OtherNouns { get; set; }
        public ICollection<Adjective> Adjectives { get; set; }
    }
}
