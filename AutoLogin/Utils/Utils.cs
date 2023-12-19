using AutoLogin.Accounts;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace AutoLogin.Utils
{
    public static class Helper
    {
        static string s_regex = @"(user|pwd|role):[^\s]+";

        public static Account Parse(string filename)
        {
            Account account = new Account();

            try
            {
                if (filename == "" || filename is null)
                    return null;

                string content = File.ReadAllText(filename);

                Regex reg = new Regex(s_regex, RegexOptions.IgnoreCase);
                MatchCollection matches = reg.Matches(content);

                foreach (Match match in matches)
                {
                    string mstr = match.ToString();

                    if (mstr.Contains("user:"))
                        account.Login = mstr.Replace("user:", "");

                    if (mstr.Contains("pwd:"))
                        account.Password = mstr.Replace("pwd:", "");

                    if (mstr.Contains("role:"))
                        account.Name = mstr.Replace("role:", "");
                }


                // If we cant get account name, set it as a login
                if (account.Login is null || account.Password is null)
                    return null;

                if (account.Name is null)
                    account.Name = account.Login;

                account.ClassImage = SetImageByName("Batch");

                return account;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

        }

        public static bool Save_account(Account acc)
        {

            try
            {
                //Save to config
                var section = Logic.ini.Sections.Add(acc.Name);
                section.Keys.Add("Login", acc.Login);
                section.Keys.Add("Password", acc.Password);
                section.Keys.Add("ClassIamge", acc.ClassImage);

                Logic.ini.Save("Config.ini");
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public static string SetImageByName(string Name)
        {
            var directory = Directory.GetCurrentDirectory();

            switch (Name)
            {
                case "Blademaster":
                    return directory + @"\icons\ClassIcon_Blademaster.png";
                case "Wizard":
                    return directory + @"\icons\ClassIcon_Wizard.png";
                case "Barbarian":
                    return directory + @"\icons\ClassIcon_Barbarian.png";
                case "Venomancer":
                    return directory + @"\icons\ClassIcon_Venomancer.png";
                case "Archer":
                    return directory + @"\icons\ClassIcon_Archer.png";
                case "Cleric":
                    return directory + @"\icons\ClassIcon_Cleric.png";
                case "Psychic":
                    return directory + @"\icons\ClassIcon_Psychic.png";
                case "Assassin":
                    return directory + @"\icons\ClassIcon_Assassin.png";
                case "Mystic":
                    return directory + @"\icons\ClassIcon_Mystic.png";
                case "Seeker":
                    return directory + @"\icons\ClassIcon_Seeker.png";
                case "Stormbringer":
                    return directory + @"\icons\ClassIcon_Stormbringer.png";
                case "Duskblade":
                    return directory + @"\icons\ClassIcon_Duskblade.png";
                case "Technician":
                    return directory + @"\icons\ClassIcon_Technician.png";
                case "Wildwalker":
                    return directory + @"\icons\ClassIcon_Wildwalker.png";
                case "Edgerunner":
                    return directory + @"\icons\ClassIcon_Edgerunner.png";
                case "Batch":
                    return directory + @"\icons\batch_loading.png";
                default:
                    return directory + @"\icons\Classicon_None.png";
            }
        }

    }
}
