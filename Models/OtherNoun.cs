namespace GermanVocabularyAPI.Models
{
    public class OtherNoun
    {
        public int Id { get; set; }
        public string Noun { get; set; }
        public string TurkishMeaning { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Note { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
