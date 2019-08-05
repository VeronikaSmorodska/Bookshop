using AutoMapper;
using BookshopAPI.Models;
using BookshopBLL.DTO;

namespace BookshopAPI.Automapper
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<BookDTO, BookViewModel>();
            CreateMap<BookViewModel, BookDTO>();

            CreateMap<AuthorDTO, AuthorViewModel>();
            CreateMap<AuthorViewModel, AuthorDTO>();

            CreateMap<UserDTO, UserViewModel>();
            CreateMap<UserViewModel, UserDTO>();

            CreateMap<UserDTO, RegisterViewModel >();
            CreateMap<RegisterViewModel, UserDTO>();
        }
    }
}
