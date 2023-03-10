using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSample.Library.Domain.Dto;

namespace UniSample.Library.Domain.Contract
{
    public class AddBookRequest
    {
        public BookDto Book { get; set; }
    }
}
