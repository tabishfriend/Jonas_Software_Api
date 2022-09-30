using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.App_Start
{
    public static class LoggerAppConfiguration
    {
        public static void Init()
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("C:\\Projects\\myapp.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

         
        }
              

    }
}