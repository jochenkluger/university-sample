using CoreWCF;
using UniversitySample.Courses.Domain.Dto;
using UniversitySample.Courses.Service.Controllers;
using UniversitySample.Courses.Service.Interfaces;
using UniversitySample.Courses.Service.InternalService;

namespace UniversitySample.Courses.Service.ApiServices
{
    public class SoapService: ICourseService
    {
        private readonly CourseProvider _provider;
        private readonly ILogger<CourseService> _logger;

        public SoapService(CourseProvider provider, ILogger<CourseService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public List<CourseDetails> Get()
        {
            return _provider.Get();
        }

        public CourseDetails GetById(Guid id)
        {
            return _provider.GetById(id);
        }

        public CourseDetails GetByName(string name)
        {
            return _provider.GetByName(name);
        }

        public void Add(CourseDetails courseDetails)
        {
            _provider.Add(courseDetails);
        }

        public void Update(CourseDetails courseDetails)
        {
            try
            {
                _provider.Update(courseDetails);
                return;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex, "Element not found");
                throw new FaultException<ServiceFault>(new ServiceFault() {ErrorMessage = "Element not found" });
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _provider.Delete(id);
                return;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex, "Element not found");
                throw new FaultException<ServiceFault>(new ServiceFault() { ErrorMessage = "Element not found" });
            }
        }
    }
}
