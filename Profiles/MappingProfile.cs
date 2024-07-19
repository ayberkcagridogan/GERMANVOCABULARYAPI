using AutoMapper;
using GermanVocabularyAPI.Models;
using GermanVocabularyAPI.DTOs;

namespace GermanVocabularyAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Noun, NounDTO>().ReverseMap();
            CreateMap<Verb, VerbDTO>().ReverseMap();
            CreateMap<OtherNoun, OtherNounDTO>().ReverseMap();
        }
    }
}
