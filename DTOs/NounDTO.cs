using GermanVocabularyAPI.DTOs.Interface;
using GermanVocabularyAPI.Models.Enums;

namespace GermanVocabularyAPI.DTOs
{
    public record NounDTO : CardDTOBase
    {
        public string Plural { get; set; } = string.Empty;

        public NounDTO()
        {
            WordType = WordType.Noun;
        }
    }
}
