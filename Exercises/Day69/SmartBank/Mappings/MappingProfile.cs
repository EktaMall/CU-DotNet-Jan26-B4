using AutoMapper;
using SmartBank.DTOs;
using SmartBank.Models;
namespace SmartBank.Mappings

{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
