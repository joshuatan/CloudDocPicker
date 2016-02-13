using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDocPicker.Models.RestApi
{
    public static class RestApiFactory
    {
        private static Dictionary<RestApiBase.ApiProvider, RestApiBase> List = 
            new Dictionary<RestApiBase.ApiProvider, RestApiBase>(); 

        static RestApiFactory()
        {
            List.Add(RestApiBase.ApiProvider.ExactOnline, new ExactApi());
            List.Add(RestApiBase.ApiProvider.Dropbox, new DropboxApi());
            List.Add(RestApiBase.ApiProvider.Box, new BoxApi());
            List.Add(RestApiBase.ApiProvider.OneNote, new OneNoteApi());
        }

        public static RestApiBase Create(RestApiBase.ApiProvider provider)
        {
            return List[provider];
        }
    }
}
