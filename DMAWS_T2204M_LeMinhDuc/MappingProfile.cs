using AutoMapper;
using DMAWS_T2204M_TranHung.DTOs;
using DMAWS_T2204M_TranHung.Models;
using DMAWS_T2204M_TranHung.ViewModels;

namespace DMAWS_T2204M_TranHung
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<ProjectEmployee, ProjectEmployeeDTO>();

            CreateMap<ProjectCreateModel, Project>();
            CreateMap<EmployeeCreateModel, Employee>();
        }
    }
}
