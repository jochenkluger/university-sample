﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSample.Common.Communication;
using UniSample.Library.Domain.Dto;

namespace UniSample.Library.Domain.Contract
{
    public class ListBookResponse: Response
    {
        public List<BookDto> Books { get; set; }
    }
}