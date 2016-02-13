using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdpLibrary.RestApi
{
    public class Files : IRestApiType 
    {
        private List<File> _files;

        public List<File> files
        {
            get
            {
                return _files;
            }
            set
            {
                _files = value;
            }
        }

        public Files()
        {
            _files = new List<File>();
        }
    }
}
