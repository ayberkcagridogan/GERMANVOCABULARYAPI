namespace GermanVocabularyAPI.DTOs
{
    public class VerbDTO
    {
        public int Id { get; set; }
        public string Verb { get; set; }
        public string TurkishMeaning { get; set; }
        public string Ich { get; set; }
        public string Du { get; set; }
        public string EsSieEr { get; set; }
        public string Wir { get; set; }
        public string Ihr { get; set; }
        public string sieSie { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Note { get; set; }
        public int CardId { get; set; }
    }
}
