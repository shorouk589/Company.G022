using AutoMapper;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;

namespace Company.G02.PL.Mapping
{
    //CLR
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {


            CreateMap<Employee, CreateEmployeeDto>()
                  .ReverseMap()
                  .ForMember(d => d.Name, o => o.MapFrom(s => s.EmpName));



      
        }
    }
}
