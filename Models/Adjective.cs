using System.Text.Json.Serialization;
using GermanVocabularyAPI.Models.Enums;
using GermanVocabularyAPI.Models.Interface;

namespace GermanVocabularyAPI.Models;


public class Adjective : CardBase
{
    public string Komparativ { get; set; } = string.Empty;
    public string Superlativ { get; set; } = string.Empty;

    public Adjective()
    {
         WordType = WordType.Adjective;
    }
} 