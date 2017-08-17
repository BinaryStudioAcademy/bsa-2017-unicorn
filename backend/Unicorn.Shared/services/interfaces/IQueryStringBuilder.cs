using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.services.interfaces
{
    internal interface IQueryStringBuilder
    {
        string BuildFrom<T>(T payload, string separator = ",");
    }
}
