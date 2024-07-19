namespace GermanVocabularyAPI.Models;


public class Adjective 
{
    public int Id { get; set; }

    public string Positiv { get; set; } = string.Empty;

    public string Komparativ { get; set; } = string.Empty;

    public string Superlativ { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int CardId { get; set; }
    
    public Card Card { get; set; }

} 