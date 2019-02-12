using EX.Model.DbLayer;
using EX.Model.DTO;
using EX.Model.Repositories.Administration;
using EX.ViewModel.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EX.ViewModel
{

    public class Administrator : INotifyPropertyChanged
    {
        UserRepositoryDTO userRepository;
        RoleRepositoryDTO roleRepository;
        UserInRoleRepositoryDTO userInRoleRepository;
        CommandRepositoryDTO commandRepository;
        TabRepositoryDTO tabRepository;

        ObservableCollection<UserDTO> users;
        ObservableCollection<RoleDTO> roles;
        ObservableCollection<CommandDTO> commands;
        ObservableCollection<TabDTO> tabs;
        ObservableCollection<SubTabDTO> subTabs;

        UserDTO selectedUser;
        UserDTO regisrationUser;
        UserDTO authorizedUser;
        RoleDTO selectedRole;
        RoleDTO defaultRole;
        RoleDTO addedRole;
//        TabDTO selectedTab;

        string statusRegistration;
        string statusAuthorisation;

        public UserDTO SelectedUser { get { return selectedUser; } set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); } }
        public UserDTO RegistrationUser { get { return regisrationUser; } set { regisrationUser = value; OnPropertyChanged(nameof(RegistrationUser)); } }
        public UserDTO AuthrizedUser { get { return authorizedUser; } set { authorizedUser = value; OnPropertyChanged(nameof(AuthrizedUser)); } }
        public RoleDTO SelectedRole { get { return selectedRole; } set { selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); } }
        public RoleDTO DefaultRole { get { return defaultRole; } set { defaultRole = value; OnPropertyChanged(nameof(DefaultRole)); } }
        public RoleDTO AddedRole { get { return addedRole; } set { addedRole = value; OnPropertyChanged(nameof(AddedRole)); } }
//        public TabDTO SelectedTab { get { return selectedTab; } set { selectedTab = value; OnPropertyChanged(nameof(SelectedTab)); } }

        public string StatusRegistration { get { return statusRegistration; } set { statusRegistration = value; OnPropertyChanged(nameof(StatusRegistration)); } }
        public string StatusAuthorisation { get { return statusAuthorisation; } set { statusAuthorisation = value; OnPropertyChanged(nameof(StatusAuthorisation)); } }

        public ObservableCollection<UserDTO> Users { get { return users; } set { users = value;  OnPropertyChanged(nameof(Users)); } }
        public ObservableCollection<RoleDTO> Roles { get { return roles; } set { roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<CommandDTO> Commands { get { return commands; } set { commands = value; OnPropertyChanged(nameof(Commands)); } }
        public ObservableCollection<TabDTO> Tabs { get { return tabs; } set { tabs = value; OnPropertyChanged(nameof(Tabs)); } }
        public ObservableCollection<SubTabDTO> SubTabs { get { return subTabs; } set { subTabs = value; OnPropertyChanged(nameof(SubTabs)); } }

        RelayCommand addUser;
        public RelayCommand AddUser { get { return addUser; } }

        RelayCommand delUser;
        public RelayCommand DelUser { get { return delUser; } }

        RelayCommand userChanged;
        public RelayCommand UserChanged { get { return userChanged; } }

        RelayCommand addRole;
        public RelayCommand AddRole { get { return addRole; } }

        RelayCommand delRole;
        public RelayCommand DelRole { get { return delRole; } }

        RelayCommand roleChanged;
        public RelayCommand RoleChanged { get { return roleChanged; } }

        RelayCommand saveChanges;
        public RelayCommand SaveChanges { get { return saveChanges; } }

        RelayCommand setUserRole;
        public RelayCommand SetUserRole { get { return setUserRole; } }

        RelayCommand setDefaultRole;
        public RelayCommand SetDefaulRole { get { return setDefaultRole; } }


        public Administrator()
        {
            userRepository = new UserRepositoryDTO();
            roleRepository = new RoleRepositoryDTO();
            userInRoleRepository = new UserInRoleRepositoryDTO();
            tabRepository = new TabRepositoryDTO();
            commandRepository = new CommandRepositoryDTO();

            StatusRegistration = "Пройдите регистрацию";
            regisrationUser = new UserDTO();

            var checkUser = userRepository.GetUserDTOs().FirstOrDefault();
            if (checkUser == null)
            {
                var newUser = new UserDTO
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Login = "admin",
                    Password = "admin".GetHashCode().ToString(),
                    IsSelected = true
                };
                checkUser = userRepository.AddOrUpdate(newUser);
            }

            addedRole = new RoleDTO { Name = "NewRole1", IsDefault = false, IsSelected = false };
            var checkRole = roleRepository.GetAllRoles().FirstOrDefault();
            if (checkRole == null)
            {
                checkRole = roleRepository.AddOrUpdate(new RoleDTO { Name = "admin", IsSelected = true, IsDefault = true });
            }
            else
            {
                CheckAndCorrectAddedRoleIfNeed(addedRole);
            }

            var checkUserInRole = userInRoleRepository.GetAllUserInRolesDTOs().FirstOrDefault();
            if (checkUserInRole == null)
            {
                UserInRoleDTO userInRoleDTO = new UserInRoleDTO
                {
                    UserId = checkUser.Id,
                    RoleId = checkRole.Id
                };
                userInRoleRepository.AddOrUpdate(userInRoleDTO);
            }

            Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
            Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
            SelectedUser = users.Where(u => u.IsSelected == true).FirstOrDefault();
            SelectedRole = roles.Where(r => r.IsSelected == true).FirstOrDefault();
            DefaultRole = roles.Where(r => r.IsDefault == true).FirstOrDefault();
            Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(c => c.RoleId == selectedRole.Id));
            Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));


            addUser = new RelayCommand(c =>
            {
                System.Windows.Controls.PasswordBox p = (System.Windows.Controls.PasswordBox)c;
                var isLoginExist = userRepository.GetUserDTOs().Where(u => u.Login == regisrationUser.Login).Count() != 0;
                if (isLoginExist) StatusRegistration = "Пользователь с логином " + regisrationUser.Login + " уже зарегистрирован";
                else
                {                    
                    regisrationUser.Password = p.Password.GetHashCode().ToString();
                    regisrationUser.IsSelected = true;
                    var _selectedUser = userRepository.GetUserDTOs().Where(u => u.IsSelected == true).FirstOrDefault();
                    _selectedUser.IsSelected = false;
                    userRepository.AddOrUpdate(_selectedUser);
                  /*  var _registrationUser = */userRepository.AddOrUpdate(regisrationUser);
 //                   Users.Add(_registrationUser);
 //                   Users.Where(u => u.Id == _selectedUser.Id).FirstOrDefault().IsSelected = false;
                    Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                    SelectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                    userInRoleRepository.AddOrUpdate(new UserInRoleDTO {
                        UserId = selectedUser.Id, RoleId = defaultRole.Id });
                    StatusRegistration = "Вы успешно прошли регистрацию\n" +
                    "Ваши регистрационные данные:\n" +
                    "Имя - " + SelectedUser.FirstName + "\n" +
                    "Фамилия - " + SelectedUser.LastName + "\n" +
                    "Логин - " + SelectedUser.Login + "\n" +
                    "Идентификатор - " + SelectedUser.Id + "\n";
                    RegistrationUser.FirstName = "";
                    RegistrationUser.LastName = "";
                    RegistrationUser.Login = "";
                    p.Clear();
                    var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                    oldSelectedRole.IsSelected = false;
                    roleRepository.AddOrUpdate(oldSelectedRole);
                    var newSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsDefault == true).FirstOrDefault();
                    newSelectedRole.IsSelected = true;
                    roleRepository.AddOrUpdate(newSelectedRole);
                    Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                    SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
                }
            });

            delUser = new RelayCommand(c =>
            {
                var delUserInRole = userInRoleRepository.GetAllUserInRolesDTOs().
                      Where(ur => ur.UserId == selectedUser.Id).FirstOrDefault();
                userInRoleRepository.RemoveUserInRoleDTO(delUserInRole);
                userRepository.RemoveUserDTO(selectedUser);
                if (selectedUser.IsSelected == true)
                {
                    var newSelectedUser = userRepository.GetUserDTOs().FirstOrDefault();
                    newSelectedUser.IsSelected = true;
                    var nsuid = userRepository.AddOrUpdate(newSelectedUser).Id;
                    users.Where(u => u.Id == nsuid).FirstOrDefault().IsSelected = true;
                }
         //       var delUser = selectedUser;
                //                Users.Remove(delUser);
                Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                SelectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                var oldSelectedRole = roleRepository.GetAllRoles().
                 Where(r => r.IsSelected == true).FirstOrDefault();
                oldSelectedRole.IsSelected = false;
                roleRepository.AddOrUpdate(oldSelectedRole);
                var newSelectedRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where
                    (ur => ur.UserId == selectedUser.Id).Select(r=>r.Id).FirstOrDefault();
                var newSelectedRole = roleRepository.GetAllRoles().Where
                       (r => r.Id == newSelectedRoleId).FirstOrDefault();
                newSelectedRole.IsSelected = true;
                roleRepository.AddOrUpdate(newSelectedRole);
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });

            addRole = new RelayCommand(c =>
            {
                var isRoleExist = roleRepository.GetAllRoles().Where(r => r.Name == addedRole.Name).Count() > 0;
                if (!isRoleExist)
                {
                    roleRepository.AddOrUpdate(addedRole);
                    Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                    CheckAndCorrectAddedRoleIfNeed(addedRole);
                } else{ CheckAndCorrectAddedRoleIfNeed(addedRole); }
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });

            delRole = new RelayCommand(c =>
            {
                var isUseSelectedRole = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.RoleId == SelectedRole.Id).Count()>0;
                if (isUseSelectedRole)
                {
                    MessageBox.Show("Данная роль не может быть удалена, так как предназначена как мимнимум одному пользователю");
                }
                else if(selectedRole.IsDefault == true)
                {
                    MessageBox.Show("Данная роль не может быть удалена так как определена как роль по умолчанию");
                }
                else
                {
                    roleRepository.RemoveRoleDTO(selectedRole);
                    tabRepository.RomoveCurrentTabRepository(selectedRole.Id);
                    commandRepository.RemoveCommandRepository(selectedRole.Id);
                }
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });

            setDefaultRole = new RelayCommand(c =>
            {
                var oldDefaultRole = roleRepository.GetAllRoles().Where(r => r.IsDefault == true).FirstOrDefault();
                oldDefaultRole.IsDefault = false;
                roleRepository.AddOrUpdate(oldDefaultRole);
                var newDefaultRole = roleRepository.GetAllRoles().Where(r => r.Id == selectedRole.Id).FirstOrDefault();
                newDefaultRole.IsDefault = true;
                roleRepository.AddOrUpdate(newDefaultRole);
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                DefaultRole = Roles.Where(r => r.IsDefault == true).FirstOrDefault();
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });

            setUserRole = new RelayCommand(c =>
            {
                var oldSelectedUser = userRepository.GetUserDTOs().Where(u => u.IsSelected == true).FirstOrDefault();
                if(SelectedUser.Id != oldSelectedUser.Id)
                {
                    oldSelectedUser.IsSelected = false;
                    var newSelectedUser = userRepository.GetUserDTOs().Where(u => u.Id == SelectedUser.Id).FirstOrDefault();
                    newSelectedUser.IsSelected = true;
                    userRepository.AddOrUpdate(oldSelectedUser);
                    userRepository.AddOrUpdate(newSelectedUser);
                }
                var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                if(SelectedRole.Id != oldSelectedRole.Id)
                {
                    oldSelectedRole.IsSelected = false;
                    var newSelectedRole = roleRepository.GetAllRoles().Where(r => r.Id == SelectedRole.Id).FirstOrDefault();
                    newSelectedRole.IsSelected = true;
                    roleRepository.AddOrUpdate(oldSelectedRole);
                    roleRepository.AddOrUpdate(newSelectedRole);
                }
                var correctedUserInRole = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.UserId == SelectedUser.Id).FirstOrDefault();
                correctedUserInRole.RoleId = SelectedRole.Id;
                userInRoleRepository.AddOrUpdate(correctedUserInRole);
                users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
                selectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co=>co.RoleId == selectedRole.Id));
                Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));
            });

            roleChanged = new RelayCommand(c =>
            {
                int selectedRoleId;
                if (SelectedRole != null) selectedRoleId = SelectedRole.Id;
                else selectedRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.UserId == selectedUser.Id).Select(s => s.RoleId).FirstOrDefault();
                Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co => co.RoleId == selectedRoleId));
                Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRoleId));
            });

            saveChanges = new RelayCommand(с =>
            {
                commandRepository.UpdateCommandRepository(commands);
                tabRepository.UpdateTabRepository(tabs);
            });

            userChanged = new RelayCommand(c =>
            {
                if (selectedUser != null)
                {
                    var isSelectuserHaveRole = userInRoleRepository.GetAllUserInRolesDTOs().Select(s => s.UserId).Contains(selectedUser.Id);
                    if (isSelectuserHaveRole)
                    {
                        var oldSelectedUser = userRepository.GetUserDTOs().Where(u => u.IsSelected == true).FirstOrDefault();
                        oldSelectedUser.IsSelected = false;
                        var newSelectedUser = userRepository.GetUserDTOs().Where(u => u.Id == selectedUser.Id).FirstOrDefault();
                        newSelectedUser.IsSelected = true;
                        userRepository.AddOrUpdate(oldSelectedUser);
                        userRepository.AddOrUpdate(newSelectedUser);


                        var newRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where(u => u.UserId == SelectedUser.Id).FirstOrDefault().RoleId;
                        var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                        if (oldSelectedRole.Id != newRoleId)
                        {
                            oldSelectedRole.IsSelected = false;
                            var newSelectedRole = roleRepository.GetAllRoles().Where(r => r.Id == newRoleId).FirstOrDefault();
                            newSelectedRole.IsSelected = true;
                            roleRepository.AddOrUpdate(oldSelectedRole);
                            roleRepository.AddOrUpdate(newSelectedRole);
                            Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                            SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
                            Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co => co.RoleId == selectedRole.Id));
                            Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));
                        }
                        users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                        SelectedUser = users.Where(u => u.IsSelected == true).FirstOrDefault();
                    }
                }
            });
        }

        private void CheckAndCorrectAddedRoleIfNeed(RoleDTO _addedRole)
        {
            if (roleRepository.GetAllRoles().Where(r => r.Name == _addedRole.Name).Count() > 0)
            {
                addedRole.Name = "NewRole1";
                while (roleRepository.GetAllRoles().Where(r => r.Name == addedRole.Name).Count() > 0)
                {
                    addedRole.Name = addedRole.Name.Trim(new char[] { 'N', 'e', 'w', 'R', 'o', 'l', 'e' });
                    int d = int.Parse(addedRole.Name) + 1;
                    addedRole.Name = "NewRole" + d.ToString();
                }
            }
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
