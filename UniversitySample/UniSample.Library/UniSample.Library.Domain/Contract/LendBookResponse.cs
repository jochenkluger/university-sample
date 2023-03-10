using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSample.Common.Communication;
using UniSample.Library.Domain.Dto;

namespace UniSample.Library.Domain.Contract
{
    public class LendBookResponse: Response
    {
        [Newtonsoft.Json.JsonProperty("lendingDto", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public LendingDto LendingDto { get; set; }
    }
}
