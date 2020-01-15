using QLBX.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLBX.ViewModels
{
    public class Login_ViewModel : BaseViewModel
    {
        public static Models.User CurrentUser { get; set; }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }

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

        public ICommand Login_Command { get; set; }
        public ICommand GetPassLogin_Command { get; set; }
        public ICommand CloseLoginform_Command { get; set; }
        public ICommand CloseAlert_Command { get; set; }

        public Login_ViewModel()
        {
            IsActive = false;
            CloseAlert_Command = new RelayCommand<object>(x =>
            {
                return true;
            }, x =>
            {
                IsActive = false;
            });

            #region login
            GetPassLogin_Command = new RelayCommand<PasswordBox>(x =>
            {
                return true;
            }, x =>
            {
                if (!String.IsNullOrEmpty(x.Password))
                {
                    Password = x.Password;
                }
            });

            Login_Command = new RelayCommand<Button>(p =>
            {
                if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password))
                    return false;

                return true;
            }, p =>
            {
                var list = new List<Models.User>(Models.DataProvider.Ins.DB.Users);

                if (list.Where(x => x.acc == UserName && x.pass == MyStaticMethods.MD5Hash(Password)).Count() != 0)
                {
                    Models.User user = list.Where(x => x.acc == UserName && x.pass == MyStaticMethods.MD5Hash(Password)).SingleOrDefault();
                    if (user.user_role == 0)
                    {

                        if (chkLoginTime(user) == false)
                        {
                            IsActive = true;
                            Message = "Tài khoản không được phân ca trực";
                        }
                        else
                        {
                            CurrentUser = user;

                            Views.Main view = new Views.Main();
                            view.Show();

                            Window wd = MyStaticMethods.getWindowParent(p) as Window;
                            if (wd != null)
                            {
                                wd.Close();
                            }

                            resetData();
                        }
                    }
                    else
                    {
                        Views.Main view = new Views.Main();
                        view.Show();

                        CurrentUser = user;

                        Window wd = MyStaticMethods.getWindowParent(p) as Window;
                        if (wd != null)
                        {
                            wd.Close();
                        }

                        resetData();
                    }
                }
                else
                {
                    IsActive = true;
                    Message = "Sai tên đăng nhập hoặc mật khẩu";
                }
            });

            CloseLoginform_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                Application.Current.Shutdown();
            });
            #endregion

        }

        private void resetData()
        {
            DateTime dateChk = DateTime.Today.Date.AddDays(-1);
            List<Models.CheckInOut> list = new List<Models.CheckInOut>(Models.DataProvider.Ins.DB.CheckInOuts);
            list = new List<Models.CheckInOut>(list.Where(x => x.checkInTime.Value.Date <= dateChk));
            for (int i = 0; i < list.Count(); i++)
            {
                Models.DataProvider.Ins.DB.CheckInOuts.Remove(list[i]);
            }
            Models.DataProvider.Ins.DB.SaveChanges();
        }

        private bool chkLoginTime(Models.User user)
        {
            var chkList = Models.DataProvider.Ins.DB.Spellduty_User.ToList();
            chkList = chkList.Where(x => x.User.users_id == user.users_id).ToList();
            chkList = chkList.Where(x => x.Spellduty.startTime.Value.Date.ToShortDateString() == DateTime.Now.Date.ToShortDateString()).ToList();
            chkList = chkList.Where(x =>
            {
                return x.Spellduty.startTime.Value <= DateTime.Now && x.Spellduty.endTime.Value >= DateTime.Now;
            }).ToList();
            return chkList.Count == 0 ? false : true;
        }
    }
}
