using System;
using System.Reflection;

namespace CdpLibrary.Helpers
{
    public sealed class ExceptionHelper
    {
        public static string ExtractAll(Exception _ex)
        {
            string msg = _ex.Message;
            if (_ex.InnerException != null)
            {
                msg = msg + "::" + ExtractAll(_ex.InnerException);
            }

            return msg;
        }

        public static void PreserveStackTrace(Exception _exception)
        {
            MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
                BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(_exception, null);
        }
    }

}
