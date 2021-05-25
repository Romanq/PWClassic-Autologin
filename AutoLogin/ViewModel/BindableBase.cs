using AutoLogin.Accounts;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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
            switch (Name)
            {
                case "Вар":
                    return Directory.GetCurrentDirectory() + @"\icons\ClassIcon_Blademaster.png";
                case "Маг":
                    return Directory.GetCurrentDirectory() + @"\icons\ClassIcon_Wizard.png";
                case "Танк":
                    return Directory.GetCurrentDirectory() + @"\icons\ClassIcon_Barbarian.png";
                case "Друид":
                    return Directory.GetCurrentDirectory() + @"\icons\ClassIcon_Venomancer.png";
                case "Лучник":
                    return Directory.GetCurrentDirectory() + @"\icons\ClassIcon_Archer.png";
                case "Прист":
                    return Directory.GetCurrentDirectory() + @"\icons\ClassIcon_Cleric.png";
                default:
                    return Directory.GetCurrentDirectory() + @"\icons\Classicon_None.png";
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
