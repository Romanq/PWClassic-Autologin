using System;
//using System.Threading.Tasks;
using System.Windows;
using MadMilkman.Ini;

namespace AutoLogin
{
    /// <summary>
    /// Логика взаимодействия для AdaAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var section = Logic.ini.Sections.Add(tbName.Text);
            section.Keys.Add("Login", tbLogin.Text);
            section.Keys.Add("Password", tbPass.Password);

            Logic.ini.Save("Config.ini");


            this.Close();
        }
    }
}
