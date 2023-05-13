using AuthenticationService.BLL.Models;
using AuthenticationService.DAL.Entities;
using AutoMapper;

namespace AuthenticationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserViewModel>()
                .ConstructUsing(v => new UserViewModel(v));
        }
    }
}
