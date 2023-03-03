using AutoMapper;
using FluentValidation;
using UniversitySample.Students.Domain.Dto;
using UniversitySample.Students.Service.DataAccess;
using UniversitySample.Students.Service.Model;

namespace UniversitySample.Students.Service.Services
{
    public class StudentService
    {
        private readonly StudentDbContext _dbContext;
        private readonly ILogger<StudentService> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentDetailsDto> _studentValidator;

        public StudentService(StudentDbContext dbContext, IMapper mapper, IValidator<StudentDetailsDto> validator, ILogger<StudentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _studentValidator = validator;
        }

        public List<StudentDetailsDto> Get()
        {
            var students = _dbContext.Students.ToList();
            var returnList = _mapper.Map<List<StudentDetailsDto>>(students);
            return returnList;
        }

        public StudentDetailsDto? GetById(Guid id)
        {
            var student = _dbContext.Students.FirstOrDefault(x => x.Id == id);
            var returnValue = _mapper.Map<StudentDetailsDto>(student);
            return returnValue;
        }

        public StudentDetailsDto? GetByName(string name)
        {
            var student = _dbContext.Students.FirstOrDefault(x => x.Username == name || x.LastName == name || x.FirstName == name);
            var returnValue = _mapper.Map<StudentDetailsDto>(student);
            return returnValue;
        }

        public void Add(StudentDetailsDto studentDto)
        {
            _studentValidator.Validate(studentDto);
            var studentModel = _mapper.Map<Student>(studentDto);
            _dbContext.Students.Add(studentModel);
            _dbContext.SaveChanges();
        }

        public void Update(StudentDetailsDto studentDto)
        {
            _studentValidator.Validate(studentDto);
            var studentModel = _mapper.Map<Student>(studentDto);

            var existingStudent = _dbContext.Students.FirstOrDefault(x => x.Id == studentModel.Id);
            if (existingStudent == null)
            {
                _logger.LogDebug("Element not found");
                throw new KeyNotFoundException("Element not found");
            }

            _dbContext.Update(studentModel);
            _dbContext.SaveChanges();
            return;
        }

        public void Delete(Guid id)
        {
            var existingStudent = _dbContext.Students.FirstOrDefault(x => x.Id == id);
            if (existingStudent == null)
            {
                _logger.LogDebug("Element not found");
                throw new KeyNotFoundException("Element not found");
            }

            _dbContext.Students.Remove(existingStudent);
            _dbContext.SaveChanges();
        }
    }
}