using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GraphQL;

namespace UniversitySample.Courses.Domain.Dto
{
    [DataContract]
    public class CourseDetails
    {
        [DataMember]
        [Id]
        public Guid Id { get; set; }

        [DataMember] public string Name { get; set; } = string.Empty;
        [DataMember]
        public string Description { get; set; } = string.Empty;
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public string Professor { get; set; } = string.Empty;
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}
