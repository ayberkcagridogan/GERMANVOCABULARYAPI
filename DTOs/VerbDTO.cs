using GermanVocabularyAPI.DTOs.Interface;
using GermanVocabularyAPI.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GermanVocabularyAPI.DTOs
{
    public record VerbDTO : CardDTOBase
    {
        public string Ich { get; set; } = string.Empty;
        public string Du { get; set; } = string.Empty;
        public string EsSieEr { get; set; } = string.Empty;
        public string Wir { get; set; } = string.Empty;
        public string Ihr { get; set; } = string.Empty;
        public string sieSie { get; set; } = string.Empty;
        public string  Perfekt { get; set; } = string.Empty;

        public VerbDTO()
        {
            WordType = WordType.Verb;
        }
    }
}
