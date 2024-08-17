using System.ComponentModel;
using GermanVocabularyAPI.DTOs.Interface;

using Swashbuckle.AspNetCore.Annotations;

namespace GermanVocabularyAPI.DTOs
{
    public record DeckDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }= string.Empty;
        public DateTime CreateDate { get; init; }
        public DateTime ModifiedDate { get; init; }

        [DefaultValue(0)]
         public int SuccessRate { get; init; }

        [DefaultValue(false)]
        public bool IsFinished { get; init; }

        [SwaggerSchema(ReadOnly = true)]
        public ICollection<CardDTOBase>? Cards { get; init; }
    }
}
