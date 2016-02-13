using System;

namespace CdpLibrary.RestApi
{
    public interface IRestApiFileOps
    {
        IRestApiType GetFiles();
        byte[] DownloadFile(string path);
    }
}
