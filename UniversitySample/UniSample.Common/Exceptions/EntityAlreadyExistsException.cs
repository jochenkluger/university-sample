using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSample.Common.Exceptions
{
    public class EntityAlreadyExistsException: Exception
    {
        public EntityAlreadyExistsException(): base("Das angegebene Element existiert bereits") {}
    }
}
