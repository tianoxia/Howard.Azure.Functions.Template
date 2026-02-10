using System;
using System.IO;

namespace Howard.FunctionApp.Services.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetCurrentDirectory()
        {
            string directory = "C:\\home\\site\\wwwroot";
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("local", StringComparison.OrdinalIgnoreCase))
            {
                directory = Directory.GetCurrentDirectory();
            }
            return directory;
        }
    }
}
