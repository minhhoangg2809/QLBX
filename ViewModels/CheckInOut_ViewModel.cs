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
    public class CheckInOut_ViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Price> _ListType;

        public ObservableCollection<Models.Price> ListType
        {
            get { return _ListType; }
            set { _ListType = value; OnPropertyChanged(); }
        }

        private Models.Price _SelectedType;

        public Models.Price SelectedType
        {
            get { return _SelectedType; }
            set { _SelectedType = value; OnPropertyChanged(); }
        }

        private Models.CheckInOut _SelectedItem;

        public Models.CheckInOut SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        private string _BiensoNhandang;

        public string BiensoNhandang
        {
            get { return _BiensoNhandang; }
            set { _BiensoNhandang = value; OnPropertyChanged(); }
        }

        private bool _IsChkInOpen;

        public bool IsChkInOpen
        {
            get { return _IsChkInOpen; }
            set { _IsChkInOpen = value; OnPropertyChanged(); }
        }

        private bool _IsChkOutOpen;

        public bool IsChkOutOpen
        {
            get { return _IsChkOutOpen; }
            set { _IsChkOutOpen = value; OnPropertyChanged(); }
        }

        private string _Thexe;

        public string Thexe
        {
            get { return _Thexe; }
            set { _Thexe = value; OnPropertyChanged(); }
        }

        public ICommand CheckIn_Command { get; set; }
        public ICommand CheckOut_Command { get; set; }
        public ICommand Load_Command { get; set; }
        public ICommand GeneId_Command { get; set; }
        public ICommand OpenChkIn_Command { get; set; }
        public ICommand OpenChkOut_Command { get; set; }
        public ICommand CloseDetail_Command { get; set; }

        #region alert

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

        public CheckInOut_ViewModel()
        {
            Load_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                IsActive = false;
                Message = String.Empty;

                IsChkInOpen = false;
                IsChkOutOpen = false;

                SelectedItem = new Models.CheckInOut();

                ListType = new ObservableCollection<Models.Price>(Models.DataProvider.Ins.DB.Prices);
                SelectedType = null;
            });

            CloseDetail_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                IsChkInOpen = false;
                IsChkOutOpen = false;
            });

            OpenChkIn_Command = new RelayCommand<object>(p =>
            {
                if (String.IsNullOrEmpty(BiensoNhandang) || String.IsNullOrEmpty(Thexe))
                    return false;

                if (SelectedType == null)
                    return false;

                if (IsChkInOpen == true)
                    return false;

                return true;
            }, p =>
            {
                IsChkInOpen = true;
            });

            CheckIn_Command = new RelayCommand<object>(p =>
            {
                if (String.IsNullOrEmpty(BiensoNhandang) || String.IsNullOrEmpty(Thexe))
                    return false;

                if (SelectedType == null)
                    return false;

                return true;
            }, p =>
            {
                if (checkValid())
                {
                    Models.CheckInOut insertItem = new Models.CheckInOut()
                    {
                        license = BiensoNhandang,
                        cardId = Thexe,
                        checkInTime = DateTime.Now,
                        Price = SelectedType,
                        checkInUserName = Login_ViewModel.CurrentUser.name
                    };
                    Models.DataProvider.Ins.DB.CheckInOuts.Add(insertItem);
                    Models.DataProvider.Ins.DB.SaveChanges();

                    IsChkInOpen = false;

                    IsActive = true;
                    Message = "Thao tác thành công";
                }
                else
                {
                    IsChkInOpen = false;

                    IsActive = true;
                    Message = "Biển số hoặc mã thẻ đã được thêm";
                }
            });

            OpenChkOut_Command = new RelayCommand<object>(p =>
            {
                if (IsChkOutOpen == true)
                    return false;

                if (Thexe.Length != 5)
                    return false;

                if (String.IsNullOrEmpty(BiensoNhandang) || String.IsNullOrEmpty(Thexe))
                    return false;

                return true;
            }, p =>
            {
                SelectedItem = Models.DataProvider.Ins.DB.CheckInOuts
                    .Where(x => x.license == BiensoNhandang && x.cardId == Thexe && x.checkOutTime == null)
                    .SingleOrDefault();
                if (SelectedItem != null)
                {
                    IsChkOutOpen = true;
                }
                else
                {
                    IsActive = true;
                    Message = "Không tìm được xe";
                }

            });

            CheckOut_Command = new RelayCommand<object>(p =>
            {
                if (String.IsNullOrEmpty(BiensoNhandang) || String.IsNullOrEmpty(Thexe))
                    return false;

                if (Thexe.Length != 5)
                    return false;

                return true;
            }, p =>
            {
                Models.CheckInOut updateItem = Models.DataProvider.Ins.DB.CheckInOuts
                    .Where(x => x.license == BiensoNhandang && x.cardId == Thexe && x.checkOutTime == null)
                    .SingleOrDefault();

                if (updateItem != null)
                {
                    updateItem.checkOutTime = DateTime.Now;
                    updateItem.checkOutUserName = Login_ViewModel.CurrentUser.name;
                    Models.DataProvider.Ins.DB.SaveChanges();

                    IsChkOutOpen = false;

                    IsActive = true;
                    Message = "Thao tác thành công";
                }

            });

            CloseAlert_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                IsActive = false;
                Message = String.Empty;
            });

            GeneId_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                Thexe = MyStaticMethods.RandomInt(5);
            });

        }

        private bool checkValid()
        {
            List<Models.CheckInOut> List = new List<Models.CheckInOut>(Models.DataProvider.Ins.DB.CheckInOuts);
            if (List.Contains(Models.DataProvider.Ins.DB.CheckInOuts.Where(x => x.license == BiensoNhandang && x.checkOutTime == null).SingleOrDefault()))
            {
                return false;
            }
            if (List.Contains(Models.DataProvider.Ins.DB.CheckInOuts.Where(x => x.cardId == Thexe && x.checkOutTime == null).SingleOrDefault()))
            {
                return false;
            }
            return true;
        }
    }
}
