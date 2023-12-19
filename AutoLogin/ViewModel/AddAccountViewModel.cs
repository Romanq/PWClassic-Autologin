using AutoLogin.Accounts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoLogin.Utils;

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
                        SaveConfig(obj);
                    }));
            }
        }

        public CollectionView _Hero_classes { get; set; }
        public CollectionView Hero_classes
        {
            get { return _Hero_classes; }
            set
            {
                _Hero_classes = value;
                OnPropertyChanged("Hero_classes");
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

        private string _SelectedClass { get; set; }
        public string SelectedClass
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
            string[] PW_classes = { 
                
                "Blademaster", "Wizard", "Barbarian", "Venomancer", "Archer",
                "Cleric", "Psychic", "Assassin", "Mystic", "Seeker",
                "Stormbringer", "Duskblade", "Technician", "Wildwalker", "Edgerunner",
                "None"

            };

            Hero_classes = new CollectionView(PW_classes);
            CreateAccount = new Account();
            CreateAccount.Name = "";
            CreateAccount.Login = "";
            CreateAccount.Password = "";
            CreateAccount.ClassImage = Helper.SetImageByName("None");
            
        }

        void SaveConfig(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            //Sanity checks

            string fileImage = Helper.SetImageByName(SelectedClass);


            //Can't save empty password, so
            //fileImage by default can't be empty, anyways
            if (password is "" || fileImage is "")
                return;

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
