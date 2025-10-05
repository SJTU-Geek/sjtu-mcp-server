using SJTUGeek.MCP.Server.Models;
using System.Runtime.InteropServices;

namespace SJTUGeek.MCP.Server
{
    public static class Export
    {
        [UnmanagedCallersOnly(EntryPoint = "set_options")]
        public static void ProcessStrings(IntPtr args, int argCount)
        {
            IntPtr[] pointers = new IntPtr[argCount];
            string[] argsArray = new string[argCount];
            Marshal.Copy(args, pointers, 0, argCount);

            for (int i = 0; i < argCount; i++)
            {
                IntPtr stringPtr = pointers[i];
                string managedString = Marshal.PtrToStringUTF8(stringPtr);
                argsArray[i] = managedString ?? string.Empty;
            }
            Program.ParseArgs(argsArray);
        }

        [UnmanagedCallersOnly(EntryPoint = "run_app")]
        public static int RunApp()
        {
            if (AppCmdOption.Default == null)
            {
                Console.Error.WriteLine("AppCmdOption.Default is null. Please set options before running the app.");
                return -1;
            }

            Task.Run(() =>
            {
                if (AppCmdOption.Default.EnableStdio)
                    Program.RunStdioApp(AppCmdOption.Default);
                else
                    Program.RunHttpApp(AppCmdOption.Default);
            });
            return 0;
        }
    }
}
