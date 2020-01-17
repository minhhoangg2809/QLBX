using QLBX.Models.Custom;
using QLBX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using QLBX.Resource;

namespace QLBX.ViewModels
{
    public class User_ViewModel : BaseViewModel
    {
        private ObservableCollection<UserCustom> _List;

        public ObservableCollection<UserCustom> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _ListRole;

        public ObservableCollection<string> ListRole
        {
            get { return _ListRole; }
            set { _ListRole = value; OnPropertyChanged(); }
        }


        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }

        private string _Account;

        public string Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged(); }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; OnPropertyChanged(); }
        }

        private UserCustom _SelectedItem;

        public UserCustom SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        private string _SelectedRole;

        public string SelectedRole
        {
            get { return _SelectedRole; }
            set { _SelectedRole = value; OnPropertyChanged(); }
        }
        

        #region add

        private bool _IsAdd;

        public bool IsAdd
        {
            get { return _IsAdd; }
            set { _IsAdd = value; OnPropertyChanged(); }
        }
        public ICommand OpenformAdd_Command { get; set; }
        public ICommand Add_Command { get; set; }

        #endregion

        #region update

        private bool _IsUpdate;

        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value; OnPropertyChanged(); }
        }
        public ICommand OpenformUpdate_Command { get; set; }
        public ICommand Update_Command { get; set; }

        #endregion

        #region delete

        private bool _IsDelete;

        public bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; OnPropertyChanged(); }
        }
        public ICommand OpenformDelete_Command { get; set; }
        public ICommand Delete_Command { get; set; }

        #endregion

        public ICommand Load_Command { get; set; }
        public ICommand Search_Command { get; set; }
        public ICommand CloseDlg_Command { get; set; }
        public ICommand PassChanged_Command { get; set; }
        public ICommand SortbyName_Command { get; set; }
        public ICommand SortbyBorn_Command { get; set; }
        public ICommand SortbyAddress_Command { get; set; }
        public ICommand SortbyPhone_Command { get; set; }
        public ICommand SortbyAcc_Command { get; set; }
        public ICommand SortbyRole_Command { get; set; }

        #region thong bao

        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; OnPropertyChanged(); }
        }

        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; OnPropertyChanged(); }
        }

        public ICommand CloseAlert_Command { get; set; }

        #endregion

        public User_ViewModel()
        {
            Load_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                getLst();
                UserName = String.Empty;
                Account = String.Empty;
                Phone = String.Empty;
                SelectedItem = null;
                IsActive = false;
                IsAdd = false;
                IsUpdate = false;
                IsDelete = false;

                ListRole = new ObservableCollection<string>();
                ListRole.Add("Quản trị");
                ListRole.Add("Nhân viên");

                SelectedRole = String.Empty;
            });

            CloseAlert_Command = new RelayCommand<object>(x =>
            {
                return true;
            }, x =>
            {
                IsActive = false;
            });

            CloseDlg_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                IsAdd = false;
                IsUpdate = false;
                IsDelete = false;
            });

            Search_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                getLst();
                if (!String.IsNullOrEmpty(UserName))
                {
                    List = new ObservableCollection<UserCustom>(List.Where(x => x.name.ToUpper().Contains(UserName.ToUpper())));
                }
                if (!String.IsNullOrEmpty(Account))
                {
                    List = new ObservableCollection<UserCustom>(List.Where(x => x.acc.ToUpper().Contains(Account.ToUpper())));
                }
                if (!String.IsNullOrEmpty(Phone))
                {
                    List = new ObservableCollection<UserCustom>(List.Where(x => x.phone.ToUpper().Contains(Phone.ToUpper())));
                }
            });

            PassChanged_Command = new RelayCommand<PasswordBox>(p =>
            {
                if (String.IsNullOrEmpty(p.Password))
                    return false;

                return true;
            }, p =>
            {
                SelectedItem.pass = MyStaticMethods.MD5Hash(p.Password);
            });

            #region sort

            SortbyName_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<UserCustom> chkList = new ObservableCollection<UserCustom>(List.OrderByDescending(x => x.name));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<UserCustom>(List.OrderBy(x => x.name));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyBorn_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<UserCustom> chkList = new ObservableCollection<UserCustom>(List.OrderByDescending(x => x.born));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<UserCustom>(List.OrderBy(x => x.born));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyAddress_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<UserCustom> chkList = new ObservableCollection<UserCustom>(List.OrderByDescending(x => x.user_address));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<UserCustom>(List.OrderBy(x => x.user_address));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyPhone_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<UserCustom> chkList = new ObservableCollection<UserCustom>(List.OrderByDescending(x => x.phone));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<UserCustom>(List.OrderBy(x => x.phone));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyAcc_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<UserCustom> chkList = new ObservableCollection<UserCustom>(List.OrderByDescending(x => x.acc));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<UserCustom>(List.OrderBy(x => x.acc));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyRole_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<UserCustom> chkList = new ObservableCollection<UserCustom>(List.OrderByDescending(x => x.user_role));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<UserCustom>(List.OrderBy(x => x.user_role));
                }
                else
                {
                    List = chkList;
                }
            });

            #endregion

            #region add

            OpenformAdd_Command = new RelayCommand<object>(p =>
            {
                if (IsUpdate == true || IsAdd == true || IsDelete == true)
                    return false;

                return true;
            }, p =>
            {
                SelectedItem = new UserCustom();
                SelectedRole = "Nhân viên";
                IsAdd = true;
            });

            Add_Command = new RelayCommand<object>(p =>
            {
                if (SelectedItem == null)
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.name))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.user_address))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.phone))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.Born))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.acc))
                    return false;

                if (String.IsNullOrEmpty(SelectedRole))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.pass))
                    return false;

                return true;
            }, p =>
            {
                var countAcc = List.Where(x => x.acc == SelectedItem.acc).Count();
                if (countAcc == 0)
                {
                    User user = new User();
                    user = UserCustom.MapUser(SelectedItem);
                    user.user_role = SelectedRole == "Quản trị" ? 1 : 0;

                    Models.DataProvider.Ins.DB.Users.Add(user);
                    Models.DataProvider.Ins.DB.SaveChanges();

                    IsAdd = false;
                    getLst();

                    successAlert();
                }
                else
                {
                    IsAdd = false;
                    getLst();

                    IsActive = true;
                    Message = "Tên đăng nhập bị trùng";
                }
            });

            #endregion

            #region update

            OpenformUpdate_Command = new RelayCommand<Button>(p =>
            {
                if (IsUpdate == true || IsAdd == true || IsDelete == true)
                    return false;

                return true;
            }, p =>
            {
                SelectedItem = new UserCustom();
                SelectedItem = List.Where(x => x.users_id == Convert.ToInt32(p.Uid)).SingleOrDefault();
                SelectedRole = SelectedItem.user_role == 1 ? "Quản trị" : "Nhân viên";
                IsUpdate = true;
            });

            Update_Command = new RelayCommand<object>(p =>
            {
                if (SelectedItem == null)
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.name))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.user_address))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.phone))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.Born))
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.Role))
                    return false;

                return true;
            }, p =>
            {
                User user = Models.DataProvider.Ins.DB.Users.Where(x => x.users_id == SelectedItem.users_id).SingleOrDefault();
                user.name = SelectedItem.name;
                user.user_address = SelectedItem.user_address;
                user.born = Convert.ToDateTime(SelectedItem.Born);
                user.phone = SelectedItem.phone;
                user.user_role = SelectedRole == "Quản trị" ? 1 : 0;
                Models.DataProvider.Ins.DB.SaveChanges();

                IsUpdate = false;
                getLst();

                successAlert();
            });

            #endregion

            #region delete

            OpenformDelete_Command = new RelayCommand<Button>(p =>
            {
                if (IsUpdate == true || IsAdd == true || IsDelete == true)
                    return false;

                return true;
            }, p =>
            {
                SelectedItem = new UserCustom();
                SelectedItem = List.Where(x => x.users_id == Convert.ToInt32(p.Uid)).SingleOrDefault();
                IsDelete = true;
            });

            Delete_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                User user = Models.DataProvider.Ins.DB.Users.Where(x => x.users_id == SelectedItem.users_id).SingleOrDefault();
                Models.DataProvider.Ins.DB.Users.Remove(user);
                Models.DataProvider.Ins.DB.SaveChanges();

                IsDelete = false;
                getLst();

                successAlert();
            });

            #endregion

        }

        private void getLst()
        {
            List<Models.User> listUser = new List<User>(Models.DataProvider.Ins.DB.Users);
            List = new ObservableCollection<UserCustom>();
            for (int i = 0; i < listUser.Count(); i++)
            {
                UserCustom u = new UserCustom();
                u = UserCustom.MapUserCus(listUser[i]);
                List.Add(u);
            }
        }

        private void successAlert()
        {
            IsActive = true;
            Message = "Thao tác thành công";
        }

        private void errorAlert()
        {
            IsActive = true;
            Message = "Thao tác thất bại";
        }
    }
}
