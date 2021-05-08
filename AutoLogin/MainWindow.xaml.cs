using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace AutoLogin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            auto_read();
        }

        Thread thread = null;

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

        private void auto_read()
        {
            if (File.Exists("Config.ini"))
                Logic.ini.Load("Config.ini");

            if (Logic.ini.Sections.Contains("GameSettings"))
                Logic.GamePath = Logic.ini.Sections["GameSettings"].Keys["Path"].Value;
            else {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "elementclient (elementclient.exe)|elementclient.exe";
                DialogResult result = (DialogResult)ofd.ShowDialog();
                if (result == DialogResult.Ok)
                {
                    var filePath = ofd.FileName;
                    Logic.ini.Sections.Add("GameSettings").Keys.Add("Path").Value = filePath;
                }
            }
        }

        void UpdateList()
        {
            while (true)
            {
                lstBox.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() => {
                    this.lstBox.Items.Clear();
                }));

                var sections = Logic.ini.Sections;

                foreach (var section in sections)
                {

                    if (section.Name.Contains("GameSettings"))
                        continue;

                lstBox.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => {
                        lstBox.Items.Add(section.Name);
                    }));
                    
                }

                Thread.Sleep(5000);
            }
        }

        private void AutoLogin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logic.ini.Save("Config.ini");
            thread.Abort();
        }

        private void AutoLogin_Loaded(object sender, RoutedEventArgs e)
        {
            thread = new Thread(new ThreadStart(UpdateList));
            thread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAccount accForm = new AddAccount();
            accForm.Show();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstBox.SelectedItem == null || lstBox.SelectedIndex < 0)
                return;

            

            string sectionname = lstBox.SelectedItem.ToString();
            
            if (!Logic.ini.Sections.Contains(sectionname))
                return;

            string Login = Logic.ini.Sections[sectionname].Keys["Login"].Value;
            string Password = Logic.ini.Sections[sectionname].Keys["Password"].Value;

            BasicInject inject = new BasicInject(Login, Password);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lstBox.SelectedItem == null || lstBox.SelectedIndex < 0)
                return;


            string sectionname = lstBox.SelectedItem.ToString();

            if (!Logic.ini.Sections.Contains(sectionname))
                return;

            lstBox.Items.RemoveAt(lstBox.SelectedIndex);
            Logic.ini.Sections.Remove(sectionname);

            Logic.ini.Save("Config.ini");
        }
    }
}
