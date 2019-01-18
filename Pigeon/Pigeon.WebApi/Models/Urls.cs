using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Models
{
    public static class Urls
    {
        private const string Root = "api";
        public static class V1
        {
            public const string Name = "v1";
            public const string ApiBase = Root + "/" + Name + "/";
        }
    }
}
