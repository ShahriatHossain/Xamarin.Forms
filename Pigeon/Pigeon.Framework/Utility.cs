using System;
using System.Collections.Generic;
using System.Text;

namespace Pigeon.Framework
{
    public static class Utility
    {
        public static bool IsValidDomainName(string name)
        {
            return Uri.CheckHostName(name) != UriHostNameType.Unknown;
        }
    }
}
