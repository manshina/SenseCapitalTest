using AutoMapper;
using jwtauth.Models;

using SenseCapitalTest.Dtos.Account;


namespace RPG7
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegistrDto, Account>();
            CreateMap<LoginDto, Account>();

        }
    }
}
