using System;
using System.Collections.Generic;
using System.IO;
namespace SJTUGeek.MCP.Server.Helpers
{
    public static class PathHelper
    {
        public static string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SJTUGeek.MCP.Server");

        public static string AppPath => AppDomain.CurrentDomain.BaseDirectory;
    }
}
