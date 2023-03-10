using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSample.Students.Domain.Dto;

namespace UniSample.Students.Domain.Contract
{
    public class StudentInitRequest
    {
        public StudentDto StudentDto { get; set; }
    }
}
