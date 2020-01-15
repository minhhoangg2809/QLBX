using QLBX.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLBX.ViewModels
{
    public class UserInfo_ViewModel : BaseViewModel
    {
        private string _Acc;
        public string Acc
        {
            get { return _Acc; }
            set { _Acc = value; OnPropertyChanged(); }
        }

        private string _OldPass;
        public string OldPass
        {
            get { return _OldPass; }
            set { _OldPass = value; OnPropertyChanged(); }
        }

        private string _NewPass;
        public string NewPass
        {
            get { return _NewPass; }
            set { _NewPass = value; OnPropertyChanged(); }
        }

        private string _NewConfirmPass;
        public string NewConfirmPass
        {
            get { return _NewConfirmPass; }
            set { _NewConfirmPass = value; OnPropertyChanged(); }
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

        public ICommand LoadInfor_Command { get; set; }
        public ICommand ChangeInfor_Command { get; set; }
        public ICommand CurOldPasswordChange_Command { get; set; }
        public ICommand CurPasswordChange_Command { get; set; }
        public ICommand CurRePasswordChange_Command { get; set; }
        public ICommand CloseAlert_Command { get; set; }

        public UserInfo_ViewModel()
        {
            LoadInfor_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                Acc = Login_ViewModel.CurrentUser.acc;
                OldPass = String.Empty;
                NewPass = String.Empty;
                NewConfirmPass = String.Empty;
                IsActive = false;
            });

            CloseAlert_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                IsActive = false;
                Message = String.Empty;
            });

            CurOldPasswordChange_Command = new RelayCommand<PasswordBox>(p =>
            {
                if (String.IsNullOrEmpty(p.Password))
                    return false;

                return true;
            }, p =>
            {
                OldPass = p.Password;
            });

            CurPasswordChange_Command = new RelayCommand<PasswordBox>(p =>
            {
                if (String.IsNullOrEmpty(p.Password))
                    return false;

                return true;
            }, p =>
            {
                NewPass = p.Password;
            });

            CurRePasswordChange_Command = new RelayCommand<PasswordBox>(p =>
            {
                if (String.IsNullOrEmpty(p.Password))
                    return false;

                return true;
            }, p =>
            {
                NewConfirmPass = p.Password;
            });

            ChangeInfor_Command = new RelayCommand<object>(p =>
            {
                if (String.IsNullOrEmpty(Acc))
                    return false;

                return true;
            }, p =>
            {
                if (Acc != Login_ViewModel.CurrentUser.acc)
                {
                    if (Models.DataProvider.Ins.DB.Users.Where(x => x.acc == Acc).Count() == 0)
                    {
                        Models.User item = Models.DataProvider.Ins.DB.Users.Where(x => x.users_id == Login_ViewModel.CurrentUser.users_id).SingleOrDefault();
                        item.acc = Acc;
                        Models.DataProvider.Ins.DB.SaveChanges();

                        IsActive = true;
                        Message = "Đã thay đổi tên đăng nhập";

                        Login_ViewModel.CurrentUser = Models.DataProvider.Ins.DB.Users.Where(x => x.acc == Acc).SingleOrDefault();
                    }
                    else
                    {
                        IsActive = true;
                        Message = "Tên đăng nhập đã tồn tại";
                    }
                }

                if (!String.IsNullOrEmpty(NewPass))
                {
                    if (String.IsNullOrEmpty(OldPass) || String.IsNullOrEmpty(NewConfirmPass))
                    {
                        IsActive = true;
                        Message = "Điền đầy đủ thông tin";
                        return;
                    }
                    if (NewPass != NewConfirmPass)
                    {
                        IsActive = true;
                        Message = "Xác nhận mật khẩu không khớp";
                        return;
                    }
                    if (MyStaticMethods.MD5Hash(OldPass) != Login_ViewModel.CurrentUser.pass)
                    {
                        IsActive = true;
                        Message = "Mật khẩu cũ không chính xác";
                        return;
                    }
                    Models.User item = Models.DataProvider.Ins.DB.Users.Where(x => x.users_id == Login_ViewModel.CurrentUser.users_id).SingleOrDefault();
                    item.pass = MyStaticMethods.MD5Hash(NewPass);
                    Models.DataProvider.Ins.DB.SaveChanges();

                    IsActive = true;
                    Message = "Đã thay đổi mật khẩu";
                }

            });
        }
    }
}
