using System;
using System.Collections.Generic;
using CdpLibrary.RestApi.Providers;

namespace CdpLibrary.RestApi
{
    public static class Factory
    {
        static readonly Dictionary<Provider, IRestApi> List = 
            new Dictionary<Provider, IRestApi>(); 

        static Factory()
        {
            List.Add(Provider.Dropbox, new DropboxRestApi());
            List.Add(Provider.Box, new BoxRestApi());
            List.Add(Provider.OneNote, new OneNoteRestApi());
        }

        public static IRestApi Create(Provider provider)
        {
            return List[provider];
        }
    }
}
