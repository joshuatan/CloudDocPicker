using System;

namespace CloudDocPicker.Models.RestApi
{
    public interface IRestApiDocOps
    {
        string GetAllDoc();
        string GetDoc(string id);
        void PostDoc();
    }
}
