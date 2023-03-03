using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySample.Library.Domain.Dto
{
    public class BookDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Abstract { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ListingDate { get; set; }
        public bool Available { get; set; }
        public Guid LenderStudentId { get; set; }
    }
}
