using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UniSample.Common.Exceptions;
using UniSample.Library.Domain.Validations;
using UniSample.Students.Domain.Dto;
using UniSample.Students.Service.DataAccess;
using UniSample.Students.Service.Model;

namespace UniSample.Students.Service.Services
{
    public class StudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly StudentDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentDto> _studentValidator;

        public StudentService(StudentDbContext dbContext, IMapper mapper, IValidator<StudentDto> studentValidator, ILogger<StudentService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _studentValidator = studentValidator;
            _logger = logger;
        }

        public async Task<List<StudentDto>> GetStudents()
        {
            var studentModels = await _dbContext.Students.ToListAsync();
            var studentDtos = _mapper.Map<List<StudentDto>>(studentModels);
            return studentDtos;
        }

        public async Task<StudentDto> GetStudentById(Guid id)
        {
            var studentModel = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            var studentDto = _mapper.Map<StudentDto>(studentModel);
            return studentDto;
        }

        public async Task DeleteStudent(Guid id)
        {
            var course = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw new EntityNotFoundException(id.ToString());
            }

            _dbContext.Students.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddStudent(StudentDto studentDto)
        {
            await _studentValidator.ValidateAndThrowAsync(studentDto);
            var studentModel = _mapper.Map<Student>(studentDto);

            var existing =
                await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == studentModel.Id );
            if (existing != null)
            {
                throw new EntityAlreadyExistsException();
            }

            await _dbContext.Students.AddAsync(studentModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpsertStudent(StudentDto studentDto)
        {
            await _studentValidator.ValidateAndThrowAsync(studentDto);
            var studentModel = _mapper.Map<Student>(studentDto);

            var existing =
                await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == studentModel.Id);
            if (existing != null)
            {
                //ensure that LastLogin is not overwritten
                studentModel.LastLogin = existing.LastLogin.ToUniversalTime();
                _dbContext.Students.Update(studentModel);
            }
            else
            {
                await _dbContext.Students.AddAsync(studentModel);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<StudentDto> InitStudent(StudentDto studentDto, string identitySubject)
        {
            //init is executed for each login from the frontend            
            var existing =
                await _dbContext.Students.FirstOrDefaultAsync(x => x.Identity == studentDto.Identity);
            bool newCreated = false;

            if (existing != null)
            {
                existing.LastLogin = DateTime.Now.ToUniversalTime();
                _dbContext.Students.Update(existing);
            }
            else
            {
                //first login
                var studentModel = _mapper.Map<Student>(studentDto);
                studentModel.LastLogin = DateTime.Now.ToUniversalTime();
                studentModel.Identity = identitySubject;
                await _dbContext.Students.AddAsync(studentModel);
                existing = studentModel;
                newCreated = true;
            }

            await _dbContext.SaveChangesAsync();
            var returnDto = _mapper.Map<StudentDto>(existing);
            returnDto.NewCreated = newCreated;
            return returnDto;
        }
    }
}
