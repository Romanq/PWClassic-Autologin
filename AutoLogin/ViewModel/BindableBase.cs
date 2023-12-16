using AutoLogin.Accounts;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace AutoLogin.ViewModel
{
    public abstract class BindableBase : INotifyPropertyChanged
    {

        private static ObservableCollection<Account> _Accounts { get; set; }
        public ObservableCollection<Account> Accounts
        {
            get { return _Accounts; }
            set
            {
                _Accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BindableBase()
        {
            
        }


        public string SetImageByName(string Name)
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
                default:
                    return directory + @"\icons\Classicon_None.png";
            }
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals((object)storage, (object)value))
                return false;

            storage = value;

            this.OnPropertyChanged(propertyName);

            return true;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class BindableMultiSelectListView : ListView
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(BindableMultiSelectListView), new PropertyMetadata(default(IList)));

        public new IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { throw new Exception("This property is read-only. To bind to it you must use 'Mode=OneWayToSource'."); }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            SetValue(SelectedItemsProperty, base.SelectedItems);
        }
    }
}
