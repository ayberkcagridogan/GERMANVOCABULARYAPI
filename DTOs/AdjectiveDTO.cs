using GermanVocabularyAPI.DTOs.Interface;
using GermanVocabularyAPI.Models.Enums;

namespace GermanVocabularyAPI.DTOs;

public record AdjectiveDTO : CardDTOBase
{
    public string Komparativ { get; init; } = string.Empty;
    public string Superlativ { get; init; } = string.Empty;

    public AdjectiveDTO()
    {
        WordType = WordType.Adjective;
    }
}