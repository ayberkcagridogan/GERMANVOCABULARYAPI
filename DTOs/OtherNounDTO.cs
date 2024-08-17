using GermanVocabularyAPI.DTOs.Interface;
using GermanVocabularyAPI.Models.Enums;

namespace GermanVocabularyAPI.DTOs
{
    public record OtherNounDTO : CardDTOBase
    {
        public OtherNounDTO()
        {
            WordType = WordType.OtherNoun;
        }
    }
}
