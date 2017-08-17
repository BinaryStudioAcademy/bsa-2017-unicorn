using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.services.interfaces
{
    interface IRequestContent
    {
        HttpContent GetContent();
    }
}
