using GermanVocabularyAPI.Models.Enums;
using GermanVocabularyAPI.Models.Interface;

namespace GermanVocabularyAPI.Models
{
    public class Noun : CardBase
    {
        public string Plural { get; set; } = string.Empty;

        public Noun()
        {
            WordType = WordType.Noun;
        }
    }
}
