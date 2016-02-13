using System;
using System.Collections.Generic;

namespace CdpLibrary.RestApi
{
    public interface IRestApiType
    {
        List<File> files { get; set; }
    }
}
