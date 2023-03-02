using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySample.Courses.Domain.Dto
{
    [DataContract]
    public class ServiceFault
    {
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
