using CoreWCF;
using UniversitySample.Courses.Domain.Dto;

namespace UniversitySample.Courses.Service.Interfaces
{
    [ServiceContract]
    public interface ICourseService
    {
        [OperationContract]
        List<CourseDetails> Get();
        [OperationContract]
        CourseDetails GetById(Guid id);
        [OperationContract]
        CourseDetails GetByName(string name);
        [OperationContract]
        void Add(CourseDetails courseDetails);
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void Update(CourseDetails courseDetails);
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void Delete(Guid id);
    }
}
