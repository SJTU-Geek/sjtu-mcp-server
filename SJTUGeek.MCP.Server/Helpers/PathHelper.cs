﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SJTUGeek.MCP.Server.Helpers
{
    public static class PathHelper
    {
        public static string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetExecutingAssembly().GetName().Name);

        public static string AppPath => AppDomain.CurrentDomain.BaseDirectory;
    }
}
