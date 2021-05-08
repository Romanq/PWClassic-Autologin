using System;
using System.Diagnostics;
using System.IO;
using Reloaded.Injector;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using AutoLogin.Window_API;
using System.Windows.Shapes;

namespace AutoLogin
{
    class BasicInject
    {
        public BasicInject(string Login, string Password)
        {
            var task = Task.Run( () => {

                try
                {
                    string GamePath = Logic.ini.Sections["GameSettings"].Keys["Path"].Value;

                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.FileName = GamePath;
                    processInfo.Arguments = $"-{Login} -{Password}";
                    processInfo.ErrorDialog = true;
                    processInfo.UseShellExecute = false;
                    processInfo.RedirectStandardOutput = true;
                    processInfo.RedirectStandardError = true;
                    processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(GamePath);
                    Process proc = Process.Start(processInfo);

                    while (proc.MainWindowHandle == IntPtr.Zero)
                    {
                        Thread.Sleep(150);
                    }
                    
                   

                    Injector injector = new Injector(proc);
                    
                    var dll = Directory.GetCurrentDirectory() + "\\libs\\PW_Classic.dll";
                    var result = injector.Inject(dll);

                    Pinvoke.RECT rct;
                    Pinvoke.GetWindowRect(new HandleRef(this, proc.MainWindowHandle), out rct);

                    int width = rct.Right - rct.Left + 1;
                    int height = rct.Bottom - rct.Top + 1;
                    Pinvoke.MoveWindow(proc.MainWindowHandle, -5, 0, width, height, true);

                    //File.AppendAllText("Autologin.log", $"Путь до dll {dll} + result = {result}" + Environment.NewLine);
                }
                catch (Exception ex)
                {
                   //File.AppendAllText("Autologin.log", ex.Message + Environment.NewLine);
                }

            });
            
        }

        

    }
}
