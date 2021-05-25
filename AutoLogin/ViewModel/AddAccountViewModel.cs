using AutoLogin.Accounts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoLogin.ViewModel
{
    class AddAccountViewModel : BindableBase
    {
        private RelayCommand _AddAccountRow;
        public RelayCommand AddAccountRow
        {
            get
            {
                return _AddAccountRow ??
                    (_AddAccountRow = new RelayCommand(obj =>
                    {
                        SetPassword(obj);
                    }));
            }
        }
        private Account _CreateAccount { get; set; }
        public Account CreateAccount
        {
            get { return _CreateAccount; }
            set
            {
                _CreateAccount = value;
                OnPropertyChanged("CreateAccount");
            }
        }

        private ComboBoxItem _SelectedClass { get; set; }
        public ComboBoxItem SelectedClass
        {
            get { return _SelectedClass; }
            set
            {
                _SelectedClass = value;
                OnPropertyChanged("SelectedClass");
            }
        }

        public AddAccountViewModel()
        {
            CreateAccount = new Account();
            CreateAccount.Name = "Название записи";
            CreateAccount.Login = "Логин";
            CreateAccount.Password = "";
            CreateAccount.ClassImage = SetImageByName("Вар");
        }

        void SetPassword(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            string fileImage = SetImageByName(SelectedClass.Content.ToString());

            CreateAccount.Password = password;
            Accounts.Add(new Account()
            {
                Login = CreateAccount.Login,
                Name = CreateAccount.Name,
                Password = password,
                ClassImage = fileImage
            });

            //Save to config
            var section = Logic.ini.Sections.Add(CreateAccount.Name);
            section.Keys.Add("Login", CreateAccount.Login);
            section.Keys.Add("Password", password);
            section.Keys.Add("ClassIamge", fileImage);

            Logic.ini.Save("Config.ini");
        }
    }
}
