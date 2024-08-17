using AutoMapper;
using GermanVocabularyAPI.Models;
using GermanVocabularyAPI.DTOs;
using GermanVocabularyAPI.Models.Interface;
using GermanVocabularyAPI.DTOs.Interface;

namespace GermanVocabularyAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CardBase, CardDTOBase>()
                    .Include<Noun , NounDTO>()
                    .Include<Verb, VerbDTO>()
                    .Include<OtherNoun, OtherNounDTO>()
                    .Include<Adjective, AdjectiveDTO>().ReverseMap();


            CreateMap<Noun, NounDTO>().ReverseMap();
            CreateMap<Verb, VerbDTO>().ReverseMap();
            CreateMap<OtherNoun, OtherNounDTO>().ReverseMap();
            CreateMap<Adjective, AdjectiveDTO>().ReverseMap();

            CreateMap<Deck, DeckDTO>().ReverseMap();
        }
    }
}
