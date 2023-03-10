using AutoMapper;
using UniSample.Students.Domain.Dto;
using UniSample.Students.Service.Model;

namespace UniSample.Students.Service.Mapping
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>()
                .ForSourceMember(x => x.NewCreated, opt => opt.DoNotValidate());
            CreateMap<Student, StudentDto>().ForMember(x => x.NewCreated, opt => opt.Ignore());
        }
    }
}
