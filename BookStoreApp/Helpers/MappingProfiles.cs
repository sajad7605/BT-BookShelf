using BookStoreApp.Dtos;

namespace BookStoreApp.Helpers{
    public class MappingProfiles:Profile{
        public MappingProfiles()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            
        }
    }
}