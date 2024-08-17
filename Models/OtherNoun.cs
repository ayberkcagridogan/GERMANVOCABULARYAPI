using System.Text.Json.Serialization;
using GermanVocabularyAPI.Models.Enums;
using GermanVocabularyAPI.Models.Interface;

namespace GermanVocabularyAPI.Models
{
    public class OtherNoun : CardBase
    {   
        public OtherNoun()
        {
            WordType = WordType.OtherNoun;
        }
    }
}
