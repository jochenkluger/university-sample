using AutoMapper;
using UniversitySample.Students.Domain.Dto;
using UniversitySample.Students.Service.Model;

namespace UniversitySample.Students.Service.Mapping
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDetailsDto, Student>();
            CreateMap<Student, StudentDetailsDto>();
        }
    }
}
