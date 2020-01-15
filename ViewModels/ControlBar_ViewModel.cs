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
    public class ControlBar_ViewModel : BaseViewModel
    {
        private bool _IsOpen;

        public bool IsOpen
        {
            get { return _IsOpen; }
            set { _IsOpen = value; OnPropertyChanged(); }
        }

        private string _Content;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }

        public ICommand Minimize_Command { get; set; }
        public ICommand Maximize_Command { get; set; }
        public ICommand Close_Command { get; set; }
        public ICommand Drag_Command { get; set; }
        public ICommand CloseDialog_Command { get; set; }
        public ICommand OpenDialog_Command { get; set; }
        public ICommand OpenDetail_Command { get; set; }

        public ControlBar_ViewModel()
        {
            IsOpen = false;
            Content = "  Đóng ứng dụng ???";

            Minimize_Command = new RelayCommand<UserControl>(p =>
            {
                if (IsOpen == true)
                    return false;

                return true;
            }, p =>
            {
                Window windowparent = MyStaticMethods.getWindowParent(p) as Window;

                if (windowparent != null)
                {
                    windowparent.WindowState = WindowState.Minimized;
                }
            });

            Maximize_Command = new RelayCommand<UserControl>(p =>
            {
                if (IsOpen == true)
                    return false;

                return true;
            }, p =>
            {
                Window windowparent = MyStaticMethods.getWindowParent(p) as Window;

                if (windowparent != null)
                {
                    if (windowparent.WindowState == WindowState.Maximized)
                    {
                        windowparent.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        windowparent.WindowState = WindowState.Maximized;
                    }
                }
            });


            Close_Command = new RelayCommand<UserControl>(p =>
            {
                if (IsOpen == false)
                    return false;

                return true;
            }, p =>
            {
                Application.Current.Shutdown();
            });

            Drag_Command = new RelayCommand<UserControl>(p =>
            {
                return true;
            }, p =>
            {
                Window windowparent = MyStaticMethods.getWindowParent(p) as Window;

                if (windowparent != null)
                {
                    windowparent.DragMove();
                }

            });

            OpenDialog_Command = new RelayCommand<UserControl>(p =>
            {
                if (IsOpen == true)
                    return false;

                return true;
            }, p =>
            {
                IsOpen = true;
            });

            CloseDialog_Command = new RelayCommand<UserControl>(p =>
            {
                return true;
            }, p =>
            {
                IsOpen = false;
            });

            OpenDetail_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                Views.UserInfo w = new Views.UserInfo();
                w.ShowDialog();
            });
        }
    }
}
