using AutoMapper;
using BaseballGameTracker.Data;
using BaseballGameTracker.Models.Games;

namespace BaseballGameTracker.MappingProfiles
{
    public class GameAutoMapperProfile : AutoMapper.Profile
    {

        public GameAutoMapperProfile() {

            CreateMap<Game, GameReadOnlyVM>();
            CreateMap<GameCreateVM, Game>(); 
            CreateMap<GameEditVM, Game>().ReverseMap();
        
        }
    }
}
