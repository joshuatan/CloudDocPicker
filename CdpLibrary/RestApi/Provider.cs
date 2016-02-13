using System;
using System.ComponentModel;

namespace CdpLibrary.RestApi
{
    public enum Provider
    {
        [Description("Dropbox Online File Sharing")]
        Dropbox,
        [Description("Box Online File Sharing")]
        Box,
        [Description("Microsoft OneNote")]
        OneNote
    }
}
