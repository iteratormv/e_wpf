using EX.Model.DbLayer;
using EX.Model.Repositories.Administration;
using EX.VM.Infrostructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace EX.VM
{
    class Administrator : INotifyPropertyChanged
    {
        UserRepositoryDTO userRepository;
        RoleRepositoryDTO roleRepository;
        CommandRepositoryDTO commandRepository;

        ObservableCollection<User> users;
        ObservableCollection<Role> roles;
        ObservableCollection<string> commands;
        ObservableCollection<string> tabs;
        ObservableCollection<string> subTabs;
        ObservableCollection<string> allSubTabs;

        User selectedUser;
        Role selectsedRole;

        public ObservableCollection<User> Users { get { return users; } set { users = value; OnPropertyChanged(nameof(Users)); } }
        public ObservableCollection<Role> Roles { get { return roles; } set { roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<string> Commands { get { return commands; } set { commands = value; OnPropertyChanged(nameof(Commands)); } }
        public ObservableCollection<string> Tabs { get { return tabs; } set { tabs = value; OnPropertyChanged(nameof(Tabs)); } }
        public ObservableCollection<string> SubTabs { get { return subTabs; } set { subTabs = value; OnPropertyChanged(nameof(SubTabs)); } }
        public ObservableCollection<string> AllSubTabs { get { return allSubTabs; } set { subTabs = value; OnPropertyChanged(nameof(AllSubTabs)); } }

        RelayCommand addUser;
        public RelayCommand AddUser { get { return addUser; } }

        RelayCommand delUser;
        public RelayCommand DelUser { get { return delUser; } }

        RelayCommand addRole;
        public RelayCommand AddRole { get { return addRole; } }

        RelayCommand delRole;
        public RelayCommand DelRole { get { return delRole; } }

        RelayCommand saveChanges;
        public RelayCommand SaveChanges { get { return saveChanges; } }


        public Administrator()
        {
            userRepository = new UserRepositoryDTO();
            roleRepository = new RoleRepositoryDTO();

            addUser = new RelayCommand(c =>
            {

            });
        }


        private void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
