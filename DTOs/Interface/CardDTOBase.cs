using System.ComponentModel;
using System.Text.Json.Serialization;
using GermanVocabularyAPI.Models.Enums;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Annotations;

namespace GermanVocabularyAPI.DTOs.Interface;

public abstract record CardDTOBase 
{
    public int Id { get; init; }
    public string GermanWord { get; init; } = string.Empty;
    public string TurkishMeaning { get; init; } = string.Empty;

    [JsonConverter(typeof(StringEnumConverter))]
    [SwaggerSchema(ReadOnly = true)]
    public WordType WordType { get; protected set; }

    [DefaultValue(false)]
    public bool IsRemember { get; init; } = false;
    public DateTime CreateDate { get; init; }
    public DateTime ModifiedDate { get; init; }
    public string Note { get; init; } = string.Empty;
    public int DeckId { get; init; }  
}