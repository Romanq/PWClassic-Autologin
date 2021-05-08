using AutoLogin.Accounts;
using MadMilkman.Ini;

namespace AutoLogin
{
    public static class Logic
    {
        public static AccountContainer wrapper  { get; set; }

        public static IniFile ini = new IniFile(new IniOptions { EncryptionPassword = "qwerty]'/", Encoding = System.Text.Encoding.UTF8 });

        public static string GamePath { get; set; }
    }
}
