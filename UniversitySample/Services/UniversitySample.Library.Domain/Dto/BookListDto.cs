using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySample.Library.Domain.Dto
{
    public class BookListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
    }
}
