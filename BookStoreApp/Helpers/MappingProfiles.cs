using BookStoreApp.Dtos;

namespace BookStoreApp.Helpers{
    public class MappingProfiles:Profile{
        public MappingProfiles()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<Book,BookDto>();
            CreateMap<BookDto,Book>();
            CreateMap<Category,CategoryDto>();
            CreateMap<CategoryDto,Category>();
            CreateMap<Ordering,OrderingDto>();
            CreateMap<OrderingDto,Ordering>();
            
        }
    }
}