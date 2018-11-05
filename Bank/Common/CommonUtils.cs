using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Common
{
    public static class CommonUtils
    {
        public static void AddDefaultHeaders(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
