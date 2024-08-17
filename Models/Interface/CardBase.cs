
using GermanVocabularyAPI.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GermanVocabularyAPI.Models.Interface;

public abstract class CardBase 
{
    public int Id { get; set; }
    public string GermanWord { get; set; } = string.Empty;
    public string TurkishMeaning { get; set; } = string.Empty;
    
   [JsonConverter(typeof(StringEnumConverter))]
    public WordType WordType { get; protected set;}
    public bool IsRemember { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Note { get; set; } = string.Empty;
    public int DeckId { get; set; }  

    [JsonIgnore]
    public Deck Deck { get; set; }
}