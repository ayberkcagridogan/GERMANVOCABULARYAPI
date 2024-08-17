using System.Text.Json.Serialization;
using GermanVocabularyAPI.Models.Enums;
using GermanVocabularyAPI.Models.Interface;

namespace GermanVocabularyAPI.Models
{
    public class Verb : CardBase
    {
        public string Ich { get; set; } = string.Empty;
        public string Du { get; set; } = string.Empty;
        public string EsSieEr { get; set; } = string.Empty;
        public string Wir { get; set; } = string.Empty;
        public string Ihr { get; set; } = string.Empty;
        public string sieSie { get; set; } = string.Empty;

        public Verb()
        {
            WordType = WordType.Verb;
        } 
    }
}
