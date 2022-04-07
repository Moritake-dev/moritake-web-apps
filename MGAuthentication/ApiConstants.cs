using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication
{
    public class ApiConstants
    {
        public static class ApiScopes
        {
            public const string Read = "read";
            public const string Write = "write";
            public const string ApiFeature = "ApiFeature";
        }

        public static class Clients
        {
            public const string MGSolutions = "mgSolutions";
        }
    }
}