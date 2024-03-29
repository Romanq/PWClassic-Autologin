﻿using AutoLogin.Accounts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutoLogin.ViewModel
{
    class MainViewModel : BindableBase
    {
        private RelayCommand _ShowAccountForm;
        public RelayCommand ShowAccountForm
        {
            get
            {
                return _ShowAccountForm ??
                    (_ShowAccountForm = new RelayCommand(obj =>
                    {
                        AddAccount form = new AddAccount();
                        form.Show();
                    }));
            }
        }

        private IList _SelectedAccount { get; set; }
        public IList SelectedAccount
        {
            get { return _SelectedAccount; }
            set
            {
                _SelectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }

        public enum DialogResult : uint
        {
            Ok = 1,
            Cancel = 2,
            Abort = 3,
            Retry = 4,
            Ignore = 5,
            Yes = 6,
            No = 7,
            Close = 8,
            Help = 9,
            TryAgain = 10,
            Continue = 11,
            Timeout = 32000
        }

        public MainViewModel()
        {
            Accounts = new ObservableCollection<Account>();

            if (File.Exists("Config.ini"))
                Logic.ini.Load("Config.ini");

            if (Logic.ini.Sections.Contains("GameSettings"))
                Logic.GamePath = Logic.ini.Sections["GameSettings"].Keys["Path"].Value;
            else
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "elementclient (elementclient.exe)|elementclient.exe";
                DialogResult result = (DialogResult)ofd.ShowDialog();
                if (result == DialogResult.Ok)
                {
                    var filePath = ofd.FileName;
                    Logic.ini.Sections.Add("GameSettings").Keys.Add("Path").Value = filePath;
                }
                else return;
            }

            var sections = Logic.ini.Sections;

            foreach (var section in sections)
            {

                if (section.Name.Contains("GameSettings"))
                    continue;

                Accounts.Add(new Account()
                {
                    Login = section.Keys["Login"].Value,
                    Password = section.Keys["Password"].Value,
                    Name = section.Name,
                    ClassImage = section.Keys["ClassIamge"].Value
                });

            }
        }

        public void LoginSelectedAccount(IList selectedAccounts)
        {
            foreach (var item in selectedAccounts)
            {
                var converted = item as Account;
                if (converted.Login == "" || converted.Password == ""
                    || converted.Login is null || converted.Password is null)
                    return;


            }
        }


        //default params: startbypatcher game:cpw console:1 user:* pwd:* role:*
        // if nickname != ingame character name, then it stops on character selection, otherwise loggining in
        // by default taking role as account note        public int StartGameProcess(string user, string pwd, string role)
        public bool StartGameProcess(string user, string password, string role)
        {
            string GamePath = Logic.ini.Sections["GameSettings"].Keys["Path"].Value;

            if (GamePath is null || GamePath == "")
                return false;

            Process proc = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = GamePath,
                    WorkingDirectory = System.IO.Path.GetDirectoryName(GamePath),
                    UseShellExecute = false,
                    Arguments = $"startbypatcher game:cpw console:1 user:{user} pwd:{password} role:{role}"
                }
            };

            return proc.Start();
        }

        public void DeleteSelectedAccount(IList selectedAccounts)
        {
            var userResult = MessageBox.Show("Delete confirmation", "Perfect World Autologin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (userResult == System.Windows.Forms.DialogResult.No)
                return;

            try
            {
                Account[] Accs = new Account[selectedAccounts.Count];
                selectedAccounts.CopyTo(Accs, 0);

                for (int i = 0; i < Accs.Length; i++)
                {
                    var converted = Accs[i];

                    Accounts.Remove(converted);

                    var section = Logic.ini.Sections.Where(x => x.Name == converted.Name && x.Keys["Login"].Value == converted.Login &&
                    x.Keys["Password"].Value == converted.Password).FirstOrDefault();

                    Logic.ini.Sections.Remove(section);

                    Logic.ini.Save("Config.ini");
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception($"Account wasn't found, message: {ex.Message}");
            }
        }

        public void StartSelectedAccounts(IList selectedAccounts)
        {
            Account[] Accs = new Account[selectedAccounts.Count];
            selectedAccounts.CopyTo(Accs, 0);

            for (int i = 0; i < Accs.Length; i++)
            {
                if (Accs[i].Login == "" || Accs[i].Password == "")
                    return;

                StartGameProcess(Accs[i].Login, Accs[i].Password, Accs[i].Name);
            }

        }
    }
}
