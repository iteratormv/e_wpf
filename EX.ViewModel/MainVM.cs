using EX.Model.DbLayer.Settings;
using EX.Model.DTO;
using EX.Model.DTO.Setting;
using EX.Model.Repositories.Administration;
using EX.Model.Repositories.Setting;
using EX.ViewModel.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace EX.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        #region context for Administration
        #region Visibte Tab Settigs
        int visibleManageUserRole;


        public int VisibleManageUserRole
        {
            get { return visibleManageUserRole; }
            set { visibleManageUserRole = value; OnPropertyChanged(nameof(VisibleManageUserRole)); }
        }
        #endregion
        #region Repositories for Administration
        UserRepositoryDTO userRepository;
        RoleRepositoryDTO roleRepository;
        UserInRoleRepositoryDTO userInRoleRepository;
        CommandRepositoryDTO commandRepository;
        TabRepositoryDTO tabRepository;
        #endregion
        #region Collection for Administration
        ObservableCollection<UserDTO> users;
        ObservableCollection<RoleDTO> roles;
        ObservableCollection<CommandDTO> commands;
        ObservableCollection<TabDTO> tabs;
        ObservableCollection<SubTabDTO> subTabs;

        public ObservableCollection<UserDTO> Users { get { return users; } set { users = value; OnPropertyChanged(nameof(Users)); } }
        public ObservableCollection<RoleDTO> Roles { get { return roles; } set { roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<CommandDTO> Commands { get { return commands; } set { commands = value; OnPropertyChanged(nameof(Commands)); } }
        public ObservableCollection<TabDTO> Tabs { get { return tabs; } set { tabs = value; OnPropertyChanged(nameof(Tabs)); } }
        public ObservableCollection<SubTabDTO> SubTabs { get { return subTabs; } set { subTabs = value; OnPropertyChanged(nameof(SubTabs)); } }
        #endregion
        #region Fields for Administration
        UserDTO selectedUser;
        UserDTO regisrationUser;
        UserDTO authorizedUser;
        UserDTO defaultUser;
        RoleDTO selectedRole;
        RoleDTO defaultRole;
        RoleDTO addedRole;

        public UserDTO SelectedUser { get { return selectedUser; } set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); } }
        public UserDTO RegistrationUser { get { return regisrationUser; } set { regisrationUser = value; OnPropertyChanged(nameof(RegistrationUser)); } }
        public UserDTO AuthorizedUser { get { return authorizedUser; } set { authorizedUser = value; OnPropertyChanged(nameof(AuthorizedUser)); } }
        public UserDTO DefaultUser { get { return defaultUser; } set { defaultUser = value; OnPropertyChanged(nameof(DefaultUser)); } }
        public RoleDTO SelectedRole { get { return selectedRole; } set { selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); } }
        public RoleDTO DefaultRole { get { return defaultRole; } set { defaultRole = value; OnPropertyChanged(nameof(DefaultRole)); } }
        public RoleDTO AddedRole { get { return addedRole; } set { addedRole = value; OnPropertyChanged(nameof(AddedRole)); } }
        #endregion
        #region Service fields for Administration
        string statusRegistration;
        string statusAuthorisation;
        string loginInUser;

        public string StatusRegistration { get { return statusRegistration; } set { statusRegistration = value; OnPropertyChanged(nameof(StatusRegistration)); } }
        public string StatusAuthorisation { get { return statusAuthorisation; } set { statusAuthorisation = value; OnPropertyChanged(nameof(StatusAuthorisation)); } }
        public string LoginInUser { get { return loginInUser; } set { loginInUser = value; OnPropertyChanged(nameof(LoginInUser)); } }
        #endregion
        #region Commands for Administration
        RelayCommand addUser;
        public RelayCommand AddUser { get { return addUser; } }

        RelayCommand delUser;
        public RelayCommand DelUser { get { return delUser; } }

        RelayCommand setDefaultUser;
        public RelayCommand SetDefaultUser { get { return setDefaultUser; } }

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

        RelayCommand authorisationUser;
        public RelayCommand AutorisitionUser { get { return authorisationUser; } }
        #endregion
        #endregion
        #region Context for Settings
        #region Repositories for Settings
        DisplaySettingDTORepository displaySettingDTORepository;
        DSCollumnSettingDTORepository dSCollumnSettingDTORepository;
        public DisplaySettingDTORepository DisplaySettingDTORepository
        {
            get { return displaySettingDTORepository;  }
            set { displaySettingDTORepository = value;
                OnPropertyChanged(nameof(DisplaySettingDTORepository));
            }
        }
        public DSCollumnSettingDTORepository DSCollumnSettingDTORepository
        {
            get { return dSCollumnSettingDTORepository;}
            set { dSCollumnSettingDTORepository = value;
                OnPropertyChanged(nameof(DSCollumnSettingDTORepository));
            }
        }
        #endregion
        #region Collection for Settings
        ObservableCollection<DisplaySettingDTO> displaySettings;
        ObservableCollection<DSCollumnSettingDTO> dSCollumnSettings;
        public ObservableCollection<DisplaySettingDTO> DisplaySettings
        {
            get { return displaySettings; }
            set { displaySettings = value;
                OnPropertyChanged(nameof(DisplaySettings));
            }
        }
        public ObservableCollection<DSCollumnSettingDTO> DSCollumnSettings
        {
            get { return dSCollumnSettings; }
            set { dSCollumnSettings = value;
                OnPropertyChanged(nameof(DSCollumnSettings));
            }
        }
        #endregion
        #region Fields for Settings
        DisplaySettingDTO selectedDisplaySetting;
        DSCollumnSettingDTO selectedCollumnSetting;
        public DisplaySettingDTO SelectedDisplaySetting
        {
            get { return selectedDisplaySetting; }
            set { selectedDisplaySetting = value;
                OnPropertyChanged(nameof(SelectedDisplaySetting)); }
        }
        public DSCollumnSettingDTO SelectedCollumnSetting
        {
            get { return selectedCollumnSetting; }
            set { selectedCollumnSetting = value;
                OnPropertyChanged(nameof(SelectedCollumnSetting)); }
        }
        #endregion
        #region Commands for Setings
        RelayCommand addCollumn;
        public RelayCommand AddCollumn { get { return addCollumn; } }

        RelayCommand removeCollumn;
        public RelayCommand RemoveCollumn { get { return removeCollumn; } }

        RelayCommand saveColumn;
        public RelayCommand SeveCollumn { get { return saveColumn; } }

        RelayCommand addSetting;
        public RelayCommand AddSetting { get { return addSetting; } }

        RelayCommand delSetting;
        public RelayCommand DelSetting { get { return delSetting; } }

        RelayCommand saveSettingChanges;
        public RelayCommand SaveSettingChanges { get { return saveSettingChanges; } }

        RelayCommand changeDisplaySettingDefault;
        public RelayCommand ChangeDisplaySettingDefault { get { return changeDisplaySettingDefault; } }
        #endregion
        #endregion

        public MainVM()
        {
            #region Init value for Administration
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
                    IsSelected = true,
                    IsDefault = true
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
            DefaultUser = userRepository.GetUserDTOs().Where(u => u.IsDefault == true).FirstOrDefault();
            Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(c => c.RoleId == selectedRole.Id));
            Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));

            SetComandAndTabSettings(defaultUser);

            StatusAuthorisation = "Вы авторизированы как - " +
                defaultUser.Login + "(" + defaultUser.FirstName + " " + defaultUser.LastName + ")";
            AuthorizedUser = new UserDTO();
            #endregion
            #region init value for Settings
            displaySettingDTORepository = new DisplaySettingDTORepository();
            dSCollumnSettingDTORepository = new DSCollumnSettingDTORepository();

            displaySettings = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs());
            DisplaySettingDTO defaultDisplayDesktopSetting;
            if (displaySettings.Count() == 0)
            {
                defaultDisplayDesktopSetting = new DisplaySettingDTO
                {
                    Name = "default",
                    IsSelected = true,
                    Intendant = "raport"
                };
                //DisplaySettingDTO defaultDisplayRaportSetting = new DisplaySettingDTO
                //{
                //    Name = "dafault",
                //    IsSelected = true,
                //    Intendant = "raport"
                //};
            }
            else defaultDisplayDesktopSetting = displaySettingDTORepository.
                  GetAllDisplaySettingDTOs().Where(s => s.IsSelected == true).
                  FirstOrDefault();
            var defaultDisplayDesktopSettingId = displaySettingDTORepository.
                AddOrUpdate(defaultDisplayDesktopSetting).Id;
            //var defaultDisplayRaportSettingId = displaySettingDTORepository.
            //    AddOrUpdate(defaultDisplayRaportSetting).Id;
            dSCollumnSettings = new ObservableCollection<DSCollumnSettingDTO>                (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs());

            if (dSCollumnSettings.Count() == 0)
            {
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Id",
                    Alias = "№",
                    Width = 100,
                    Visible = true,
                    IsSelected = true,
                    DisplaySettingId = defaultDisplayDesktopSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "FirstName",
                    Alias = "Имя",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    DisplaySettingId = defaultDisplayDesktopSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "LastName",
                    Alias = "Фамилия",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    DisplaySettingId = defaultDisplayDesktopSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Сompany",
                    Alias = "Компания",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    DisplaySettingId = defaultDisplayDesktopSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Jobtitle",
                    Alias = "Должность",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    DisplaySettingId = defaultDisplayDesktopSettingId
                });
            }
            updateAllSettings();
            #endregion

            #region Implementation cammands for Administration
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
                    userRepository.AddOrUpdate(regisrationUser);
                    Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                    SelectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                    userInRoleRepository.AddOrUpdate(new UserInRoleDTO
                    {
                        UserId = selectedUser.Id,
                        RoleId = defaultRole.Id
                    });
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
                Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                SelectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                var oldSelectedRole = roleRepository.GetAllRoles().
                 Where(r => r.IsSelected == true).FirstOrDefault();
                oldSelectedRole.IsSelected = false;
                roleRepository.AddOrUpdate(oldSelectedRole);
                var newSelectedRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where
                    (ur => ur.UserId == selectedUser.Id).Select(r => r.Id).FirstOrDefault();
                var newSelectedRole = roleRepository.GetAllRoles().Where
                       (r => r.Id == newSelectedRoleId).FirstOrDefault();
                newSelectedRole.IsSelected = true;
                roleRepository.AddOrUpdate(newSelectedRole);
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });
            setDefaultUser = new RelayCommand(c =>
            {
                var oldDefaultUser = userRepository.GetUserDTOs().Where(u => u.IsDefault == true).FirstOrDefault();
                if (selectedUser.Id != oldDefaultUser.Id)
                {
                    oldDefaultUser.IsDefault = false;
                    var newDefaultUser = userRepository.GetUserDTOs().Where(u => u.Id == selectedUser.Id).FirstOrDefault();
                    newDefaultUser.IsDefault = true;
                    userRepository.AddOrUpdate(oldDefaultUser);
                    userRepository.AddOrUpdate(newDefaultUser);

                    DefaultUser = userRepository.GetUserDTOs().Where(u => u.IsDefault == true).FirstOrDefault();
                }
            });
            addRole = new RelayCommand(c =>
            {
                var isRoleExist = roleRepository.GetAllRoles().Where(r => r.Name == addedRole.Name).Count() > 0;
                if (!isRoleExist)
                {
                    roleRepository.AddOrUpdate(addedRole);
                    Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                    CheckAndCorrectAddedRoleIfNeed(addedRole);
                }
                else { CheckAndCorrectAddedRoleIfNeed(addedRole); }
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });
            delRole = new RelayCommand(c =>
            {
                var isUseSelectedRole = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.RoleId == SelectedRole.Id).Count() > 0;
                if (isUseSelectedRole)
                {
                    MessageBox.Show("Данная роль не может быть удалена, так как предназначена как мимнимум одному пользователю");
                }
                else if (selectedRole.IsDefault == true)
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
                if (SelectedUser.Id != oldSelectedUser.Id)
                {
                    oldSelectedUser.IsSelected = false;
                    var newSelectedUser = userRepository.GetUserDTOs().Where(u => u.Id == SelectedUser.Id).FirstOrDefault();
                    newSelectedUser.IsSelected = true;
                    userRepository.AddOrUpdate(oldSelectedUser);
                    userRepository.AddOrUpdate(newSelectedUser);
                }
                var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                if (SelectedRole.Id != oldSelectedRole.Id)
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
                Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co => co.RoleId == selectedRole.Id));
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
            authorisationUser = new RelayCommand(c =>
            {
                System.Windows.Controls.PasswordBox p = (System.Windows.Controls.PasswordBox)c;
                var checkAuthUser = userRepository.GetUserDTOs().Where(u => u.Login == loginInUser).FirstOrDefault();
                if (checkAuthUser != null)
                {
                    bool checkPass = checkAuthUser.Password == p.Password.GetHashCode().ToString();
                    if (checkPass)
                    {
                        AuthorizedUser = checkAuthUser;
                        StatusAuthorisation = "Вы авторизированы как  - " +
                         authorizedUser.Login + " (" + authorizedUser.FirstName + " " + authorizedUser.LastName + ")";
                        SetComandAndTabSettings(authorizedUser);
                    }
                    else StatusAuthorisation = "Пароль не совпадает с оригиналом";
                }
                else StatusAuthorisation = "Такой логин не зарегистрирован в базе";

                LoginInUser = "";
                p.Clear();
            });
            #endregion
            #region Emplementation command for Settings
            addSetting = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var cur_set = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.IsSelected == true && s.Intendant == _intendant).
                Select(s => s).FirstOrDefault();
                var osid = cur_set.Id;
                string new_set_name = "NewSetting1";
                while (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Name == new_set_name).Count() > 0)
                {
                    string d_name = new_set_name.Trim(
                        new char[] { 'N', 'e', 'w', 'S', 't', 'i', 'n', 'g' });
                    int d = int.Parse(d_name) + 1;
                    new_set_name = "NewSetting" + d.ToString();
                }
                var _n_set = new DisplaySettingDTO()
                {
                    Name = new_set_name,
                    IsSelected = false,
                    Intendant = _intendant
                };
                var n_set = displaySettingDTORepository.AddOrUpdate(_n_set);
                addNewCollumn(_intendant, n_set.Id, osid);
                if (cur_set != null)
                {
                    cur_set.IsSelected = false;
                    displaySettingDTORepository.AddOrUpdate(cur_set);
                }
                n_set.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(n_set);
                updateAllSettings();
            });
            delSetting = new RelayCommand(c =>
            {
                var _intendant = c as string;
                DisplaySettingDTO s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.IsSelected == true && s.Intendant == _intendant).FirstOrDefault();
                var del_cs = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.DisplaySettingId == selectedDisplaySetting.Id);
                foreach (var dc in del_cs)
                {
                    dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(dc);
                }
                if (selectedDisplaySetting.IsSelected == true)
                {
                    s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().FirstOrDefault();
                    s_ds.IsSelected = true;
                    displaySettingDTORepository.AddOrUpdate(s_ds);
                }
                displaySettingDTORepository.RemoveDisplaySettingDTO(selectedDisplaySetting);
                updateAllSettings();
            });
            changeDisplaySettingDefault = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var oldSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.IsSelected == true && s.Intendant == _intendant).FirstOrDefault();
                var oldSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.IsSelected == true && s.DisplaySettingId == oldSelectedDisplaySetting.Id).
                FirstOrDefault();
                oldSelectedDisplaySetting.IsSelected = false;
                oldSelectedCollumnSetting.IsSelected = false;
                var newSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Id == selectedDisplaySetting.Id && s.Intendant == _intendant).FirstOrDefault();
                var newSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.DisplaySettingId == newSelectedDisplaySetting.Id).FirstOrDefault();
                newSelectedDisplaySetting.IsSelected = true;
                newSelectedCollumnSetting.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(oldSelectedDisplaySetting);
                displaySettingDTORepository.AddOrUpdate(newSelectedDisplaySetting);
                dSCollumnSettingDTORepository.AddOrUpdate(newSelectedCollumnSetting);
                dSCollumnSettingDTORepository.AddOrUpdate(oldSelectedCollumnSetting);
                updateAllSettings();
            });
            addCollumn = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var dsid = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.IsSelected == true && s.Intendant == _intendant).
                    Select(s => s.Id).FirstOrDefault();
                addNewCollumn(_intendant, dsid, dsid);
                updateAllSettings();
            });
            saveSettingChanges = new RelayCommand(c =>
            {
                var _intendant = c as string;
                foreach (var d in DisplaySettings)
                {
                    displaySettingDTORepository.AddOrUpdate(d);
                }
                foreach (var dc in DSCollumnSettings)
                {
                    dSCollumnSettingDTORepository.AddOrUpdate(dc);
                }
            });
            removeCollumn = new RelayCommand(c =>
            {
                dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(selectedCollumnSetting);
                updateAllSettings();
            });
            #endregion
        }
        #region Implemetation methods
        private void addNewCollumn(string _intendant, int dsid, int osid)
        {
            string _name = "NewCollumn1";
            string _alias = "NewAlias1";
            while (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
            Where(s => s.Name == _name).Select(s => s).Count() > 0)
            {
                string d_name = _name.Trim(new char[] {
                        'N', 'e', 'w', 'C', 'o', 'l', 'u', 'm', 'n' });
                int d = int.Parse(d_name) + 1;
                _name = "NewCollumn" + d.ToString();
                _alias = "NewAlias" + d.ToString();
            }
            var oldSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
            Where(s => s.IsSelected == true && s.DisplaySettingId == osid).FirstOrDefault();
            oldSelectedCollumnSetting.IsSelected = false;
            var newSelectedCollumnSetting = dSCollumnSettingDTORepository.AddOrUpdate(new
                DSCollumnSettingDTO
            {
                Name = _name,
                Alias = _alias,
                Visible = true,
                DisplaySettingId = dsid,
                Width = 100,
                IsSelected = true
            });
            dSCollumnSettingDTORepository.AddOrUpdate(oldSelectedCollumnSetting);
            dSCollumnSettingDTORepository.AddOrUpdate(newSelectedCollumnSetting);
        }
        private void SetComandAndTabSettings(UserDTO user)
        {
            var _roleId = userInRoleRepository.GetAllUserInRolesDTOs().
                        Where(r => r.UserId == user.Id).FirstOrDefault().RoleId;
            var commands = commandRepository.GetAllCommands().Where(c => c.RoleId == _roleId);
            var tabs = tabRepository.GetTabDTOs().Where(t => t.RoleId == _roleId);
            VisibleManageUserRole = tabs.Where(t => t.Name == "Управление доступом (Администрирование)")
                .Select(s => s.IsChecked).FirstOrDefault() ? 30 : 0;
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
        private void updateAllSettings()
        {
            DisplaySettings = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs());
            SelectedDisplaySetting = DisplaySettings.
                Where(s => s.IsSelected == true).FirstOrDefault();
            DSCollumnSettings = new ObservableCollection<DSCollumnSettingDTO>
            (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
            Where(d=>d.DisplaySettingId == selectedDisplaySetting.Id));
            SelectedCollumnSetting = DSCollumnSettings.
            Where(s => s.IsSelected == true).FirstOrDefault();
        }
        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
