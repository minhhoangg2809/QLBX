using QLBX.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLBX.ViewModels
{
    public class Home_ViewModel : BaseViewModel
    {
        private ObservableCollection<Models.CheckInOut> _List;

        public ObservableCollection<Models.CheckInOut> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private Models.CheckInOut _SelectedItem;

        public Models.CheckInOut SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        private bool _IsDetailOpen;

        public bool IsDetailOpen
        {
            get { return _IsDetailOpen; }
            set { _IsDetailOpen = value; OnPropertyChanged(); }
        }

        private string _SearchLicense;

        public string SearchLicense
        {
            get { return _SearchLicense; }
            set { _SearchLicense = value; OnPropertyChanged(); }
        }

        private string _SearchTimeStart;

        public string SearchTimeStart
        {
            get { return _SearchTimeStart; }
            set { _SearchTimeStart = value; OnPropertyChanged(); }
        }

        private string _SearchTimeEnd;

        public string SearchTimeEnd
        {
            get { return _SearchTimeEnd; }
            set { _SearchTimeEnd = value; OnPropertyChanged(); }
        }

        private bool _IsEnableList;

        public bool IsEnableList
        {
            get { return _IsEnableList; }
            set { _IsEnableList = value; OnPropertyChanged(); }
        }

        public ICommand Load_Command { get; set; }
        public ICommand OpenDetail_Command { get; set; }
        public ICommand CloseDetail_Command { get; set; }
        public ICommand Search_Command { get; set; }
        public Home_ViewModel()
        {
            Load_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                List = new ObservableCollection<Models.CheckInOut>(Models.DataProvider.Ins.DB.CheckInOuts.Where(x => x.checkOutTime == null));
                List = new ObservableCollection<Models.CheckInOut>(List.Where(x => x.checkInTime.Value.Date == DateTime.Today));
                SelectedItem = null;
                IsDetailOpen = false;
                SearchLicense = String.Empty;
                SearchTimeStart = String.Empty;
                SearchTimeEnd = String.Empty;
                IsEnableList = true;
            });

            OpenDetail_Command = new RelayCommand<object>(p =>
            {
                if (IsDetailOpen == true)
                    return false;

                return true;
            }, p =>
            {
                IsDetailOpen = true;
                IsEnableList = false;
            });

            CloseDetail_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                SelectedItem = null;
                IsDetailOpen = false;
                IsEnableList = true;
            });

            Search_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                List = new ObservableCollection<Models.CheckInOut>(Models.DataProvider.Ins.DB.CheckInOuts.Where(x => x.checkOutTime == null));
                List = new ObservableCollection<Models.CheckInOut>(List.Where(x => x.checkInTime.Value.Date == DateTime.Today));
                if (!String.IsNullOrEmpty(SearchLicense))
                {
                    List = new ObservableCollection<Models.CheckInOut>(List.Where(x => x.license.ToUpper().Contains(SearchLicense.ToUpper())));
                }
                if (!String.IsNullOrEmpty(SearchTimeStart))
                {
                    DateTime timeStart = MyStaticMethods.ConvertTime(SearchTimeStart);
                    List = new ObservableCollection<Models.CheckInOut>(List.Where(x => x.checkInTime.Value >= timeStart));
                }
                if (!String.IsNullOrEmpty(SearchTimeEnd))
                {
                    DateTime timeEnd = MyStaticMethods.ConvertTime(SearchTimeEnd);
                    List = new ObservableCollection<Models.CheckInOut>(List.Where(x => x.checkInTime.Value <= timeEnd));
                }
            });
        }
    }
}
