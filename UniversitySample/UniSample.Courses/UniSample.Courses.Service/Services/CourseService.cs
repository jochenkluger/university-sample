using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UniSample.Common.Exceptions;
using UniSample.Courses.Domain.Dto;
using UniSample.Courses.Domain.Validations;
using UniSample.Courses.Service.DataAccess;
using UniSample.Courses.Service.Model;

namespace UniSample.Courses.Service.Services
{
    public class CourseService
    {
        private readonly ILogger<CourseService> _logger;
        private readonly CourseDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<CourseDto> _courseValidator;

        public CourseService(CourseDbContext dbContext, IMapper mapper, IValidator<CourseDto> courseValidator, ILogger<CourseService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _courseValidator = courseValidator;
            _logger = logger;
        }

        public async Task<List<CourseDto>> GetCourses()
        {
            var courseModels = await _dbContext.Courses.Include(x => x.Students).ToListAsync();
            var courseDtos = _mapper.Map<List<CourseDto>>(courseModels);
            return courseDtos;
        }

        public async Task<CourseDto> GetCourseById(Guid id)
        {
            var courseModel = await _dbContext.Courses.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);
            var courseDto = _mapper.Map<CourseDto>(courseModel);
            return courseDto;
        }

        public async Task DeleteCourse(Guid id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw new EntityNotFoundException(id.ToString());
            }

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCourse(CourseDto courseDto)
        {
            await _courseValidator.ValidateAndThrowAsync(courseDto);
            var courseModel = _mapper.Map<Course>(courseDto);

            var existing =
                await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseModel.Id || x.Name == courseModel.Name);
            if (existing != null)
            {
                throw new EntityAlreadyExistsException();
            }

            await _dbContext.Courses.AddAsync(courseModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpsertCourse(CourseDto courseDto)
        {
            await _courseValidator.ValidateAndThrowAsync(courseDto);
            var courseModel = _mapper.Map<Course>(courseDto);

            var existing =
                await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseModel.Id);
            if (existing != null)
            {
                _dbContext.Courses.Update(courseModel);
            }
            else
            {
                await _dbContext.Courses.AddAsync(courseModel);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task EnrollCourse(CourseStudentDto courseStudentDto)
        {
            //TODO: Validations
            var courseStudentModel = _mapper.Map<CourseStudent>(courseStudentDto);
            var courseFromDb = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseStudentDto.CourseId);
            if (courseFromDb == null)
            {
                throw new ArgumentNullException(nameof(courseFromDb));
            }

            await _dbContext.CourseStudents.AddAsync(courseStudentModel);
            //courseFromDb.Students.Add(courseStudentModel);
            courseFromDb.StudentsCount = courseFromDb.StudentsCount + 1;
            _dbContext.Courses.Update(courseFromDb);
            await _dbContext.SaveChangesAsync();
        }
    }
}
