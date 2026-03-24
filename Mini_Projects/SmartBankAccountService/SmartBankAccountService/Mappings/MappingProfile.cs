using AutoMapper;
using SmartBankAccountService.DTOs;
using SmartBankAccountService.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartBank.AccountServices.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}