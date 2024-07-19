namespace GermanVocabularyAPI.DTOs
{
    public class DeckDTO
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsFinished { get; set; }
    }
}
