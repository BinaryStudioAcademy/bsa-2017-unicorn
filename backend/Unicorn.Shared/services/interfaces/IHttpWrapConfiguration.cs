﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.services.interfaces
{
    public interface IHttpWrapConfiguration
    {
        string BasePath { get; }
        ISerializer Serializer { get; }
        //Credentials Credentials { get; set; }
        HttpClient GetHttpClient();
    }
}
