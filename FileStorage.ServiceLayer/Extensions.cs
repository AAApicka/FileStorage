using System;
using System.Collections.Generic;
using System.Text;

namespace Elinkx.FileStorage.ServiceLayer
{
   static class Extensions
    {
        internal static string GetRootMessage(this Exception e)
        {
            if (e.InnerException == null)
            {
                return e.Message;
            }
            return e.InnerException.GetRootMessage();
        }
    }
}
