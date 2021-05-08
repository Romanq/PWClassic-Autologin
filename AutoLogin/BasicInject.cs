using System;
using System.Diagnostics;
using System.IO;
using Reloaded.Injector;
using System.Threading;
using System.Threading.Tasks;

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
                    processInfo.WorkingDirectory = Path.GetDirectoryName(GamePath);
                    Process proc = Process.Start(processInfo);

                    while (proc.MainWindowHandle == IntPtr.Zero)
                    {
                        Thread.Sleep(150);
                    }

                    //SetWindowPos(handle, 0, 0, 0, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);

                    Injector injector = new Injector(proc);
                    
                    var dll = Directory.GetCurrentDirectory() + "\\libs\\PW_Classic.dll";
                    var result = injector.Inject(dll);

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
