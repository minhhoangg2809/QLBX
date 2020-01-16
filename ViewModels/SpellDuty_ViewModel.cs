using QLBX.Models;
using QLBX.Models.Custom;
using QLBX.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLBX.ViewModels
{
    public class SpellDuty_ViewModel : BaseViewModel
    {
        private ObservableCollection<SpelldutyCustom> _List;

        public ObservableCollection<SpelldutyCustom> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Spellduty_User> _ListSpellUser;

        public ObservableCollection<Spellduty_User> ListSpellUser
        {
            get { return _ListSpellUser; }
            set { _ListSpellUser = value; OnPropertyChanged(); }
        }

        private ObservableCollection<User> _ListUser;

        public ObservableCollection<User> ListUser
        {
            get { return _ListUser; }
            set { _ListUser = value; OnPropertyChanged(); }
        }


        private SpelldutyCustom _SelectedItem;

        public SpelldutyCustom SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        private string _TimeStart;

        public string TimeStart
        {
            get { return _TimeStart; }
            set { _TimeStart = value; OnPropertyChanged(); }
        }

        private string _TimeEnd;

        public string TimeEnd
        {
            get { return _TimeEnd; }
            set { _TimeEnd = value; OnPropertyChanged(); }
        }

        private string _Date;

        public string Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(); }
        }


        #region sort
        public ICommand SortbyDate_Command { get; set; }
        public ICommand SortbyTimeSt_Command { get; set; }
        public ICommand SortbyTimeEn_Command { get; set; }

        #endregion

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

        #region users

        private Models.User _SelectedUser;

        public Models.User SelectedUser
        {
            get { return _SelectedUser; }
            set { _SelectedUser = value; OnPropertyChanged(); }
        }


        private bool _IsUserOpen;

        public bool IsUserOpen
        {
            get { return _IsUserOpen; }
            set { _IsUserOpen = value; OnPropertyChanged(); }
        }

        public ICommand OpenUser_Command { get; set; }
        public ICommand UpdateUser_Command { get; set; }
        public ICommand RemoveUser_Command { get; set; }

        #endregion

        public ICommand Load_Command { get; set; }
        public ICommand CloseDlg_Command { get; set; }
        public ICommand Search_Command { get; set; }

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

        public SpellDuty_ViewModel()
        {
            Load_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                getLst();
                IsAdd = false;
                IsDelete = false;
                IsUserOpen = false;

                TimeStart = String.Empty;
                TimeEnd = String.Empty;
                Date = String.Empty;
                ListUser = new ObservableCollection<User>(Models.DataProvider.Ins.DB.Users);
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
                IsDelete = false;
                IsUserOpen = false;
            });

            #region add

            OpenformAdd_Command = new RelayCommand<object>(p =>
            {
                if (IsAdd == true || IsDelete == true || IsUserOpen == true)
                    return false;

                return true;
            }, p =>
            {
                SelectedItem = new SpelldutyCustom();
                IsAdd = true;
            });

            Add_Command = new RelayCommand<object>(p =>
            {
                if (SelectedItem == null)
                    return false;

                if (String.IsNullOrEmpty(SelectedItem.TimeSt) || String.IsNullOrEmpty(SelectedItem.TimeEn))
                    return false;

                if (MyStaticMethods.ConvertTimeSpell(SelectedItem.TimeSt) >= MyStaticMethods.ConvertTimeSpell(SelectedItem.TimeEn))
                    return false;

                return true;
            }, p =>
            {
                if (checkValid(MyStaticMethods.ConvertTimeSpell(SelectedItem.TimeSt), MyStaticMethods.ConvertTimeSpell(SelectedItem.TimeEn)))
                {
                    Spellduty sp = new Spellduty();
                    sp = SpelldutyCustom.MapSpell(SelectedItem);

                    Models.DataProvider.Ins.DB.Spellduties.Add(sp);
                    Models.DataProvider.Ins.DB.SaveChanges();
                    IsAdd = false;
                    getLst();
                    successAlert();
                }
                else
                {
                    IsAdd = false;
                    IsActive = true;
                    Message = "Ca trực bị trùng lặp";
                }
            });

            #endregion

            #region delete

            OpenformDelete_Command = new RelayCommand<Button>(p =>
            {
                if (IsAdd == true || IsDelete == true || IsUserOpen == true)
                    return false;

                return true;
            }, p =>
            {
                SelectedItem = new SpelldutyCustom();
                SelectedItem.id = List.Where(x => x.id == Convert.ToInt32(p.Uid)).SingleOrDefault().id;
                IsDelete = true;
            });

            Delete_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                Spellduty item = Models.DataProvider.Ins.DB.Spellduties.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                Models.DataProvider.Ins.DB.Spellduties.Remove(item);
                Models.DataProvider.Ins.DB.SaveChanges();

                IsDelete = false;
                getLst();

                successAlert();
            });

            #endregion

            #region search

            Search_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                getLst();
                List = new ObservableCollection<SpelldutyCustom>(List.Where(x => x.startTime.Value.Date == DateTime.Today));
                if (!String.IsNullOrEmpty(Date))
                {
                    getLst();
                    List = new ObservableCollection<SpelldutyCustom>(List.Where(x => x.startTime.Value.Date.ToShortDateString() == Date));
                }
                if (!String.IsNullOrEmpty(TimeStart))
                {
                    List = new ObservableCollection<SpelldutyCustom>(List.Where(x => x.startTime.Value >= MyStaticMethods.ConvertTime(TimeStart)));
                }
                if (!String.IsNullOrEmpty(TimeEnd))
                {
                    List = new ObservableCollection<SpelldutyCustom>(List.Where(x => x.endTime.Value <= MyStaticMethods.ConvertTime(TimeEnd)));
                }
            });

            #endregion

            #region users

            OpenUser_Command = new RelayCommand<Button>(p =>
            {
                if (IsAdd == true || IsDelete == true || IsUserOpen == true)
                    return false;

                return true;
            }, p =>
            {
                int id = Convert.ToInt32(p.Uid);
                SelectedItem = new SpelldutyCustom();
                SelectedItem.id = id;
                SelectedUser = null;
                ListSpellUser = new ObservableCollection<Spellduty_User>(Models.DataProvider.Ins.DB.Spellduty_User.Where(x => x.spelldutyId == id));
                IsUserOpen = true;
            });

            UpdateUser_Command = new RelayCommand<object>(p =>
            {
                if (SelectedUser == null)
                    return false;

                var chk = new ObservableCollection<Spellduty_User>(ListSpellUser.Where(x => x.User == SelectedUser));
                if (chk.Count != 0)
                    return false;

                return true;
            }, p =>
            {
                Spellduty_User item = new Spellduty_User()
                     {
                         spelldutyId = SelectedItem.id,
                         User = SelectedUser
                     };
                Models.DataProvider.Ins.DB.Spellduty_User.Add(item);
                Models.DataProvider.Ins.DB.SaveChanges();
                ListSpellUser.Add(item);
            });

            RemoveUser_Command = new RelayCommand<Button>(p =>
            {
                return true;
            }, p =>
            {
                int userId = Convert.ToInt32(p.Uid);
                int spellId = SelectedItem.id;
                Spellduty_User item = Models.DataProvider.Ins.DB.Spellduty_User.Where(x => x.Spellduty.id == spellId && x.User.users_id == userId).SingleOrDefault();
                Models.DataProvider.Ins.DB.Spellduty_User.Remove(item);
                Models.DataProvider.Ins.DB.SaveChanges();
                ListSpellUser.Remove(item);
            });

            #endregion

            #region sort

            SortbyDate_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<SpelldutyCustom> chkList = new ObservableCollection<SpelldutyCustom>(List.OrderByDescending(x => x.startTime.Value.Date));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<SpelldutyCustom>(List.OrderBy(x => x.startTime.Value.Date));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyTimeSt_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<SpelldutyCustom> chkList = new ObservableCollection<SpelldutyCustom>(List.OrderByDescending(x => x.startTime.Value.TimeOfDay));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<SpelldutyCustom>(List.OrderBy(x => x.startTime.Value.TimeOfDay));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyTimeEn_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<SpelldutyCustom> chkList = new ObservableCollection<SpelldutyCustom>(List.OrderByDescending(x => x.endTime.Value.TimeOfDay));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<SpelldutyCustom>(List.OrderBy(x => x.endTime.Value.TimeOfDay));
                }
                else
                {
                    List = chkList;
                }
            });

            #endregion

        }

        private void getLst()
        {
            List<Spellduty> listUser = new List<Spellduty>(Models.DataProvider.Ins.DB.Spellduties);
            List = new ObservableCollection<SpelldutyCustom>();
            for (int i = 0; i < listUser.Count(); i++)
            {
                SpelldutyCustom u = new SpelldutyCustom();
                u = SpelldutyCustom.MapSpellCus(listUser[i]);
                List.Add(u);
            }
            List = new ObservableCollection<SpelldutyCustom>(List.OrderByDescending(x => x.startTime.Value.Date));

            SelectedItem = null;
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

        private bool checkValid(DateTime t1, DateTime t2)
        {
            var chk = Models.DataProvider.Ins.DB.Spellduties.Where(x => (x.startTime >= t1 && x.startTime <= t2) || (x.endTime >= t1 && x.startTime <= t2));
            if (chk.Count() != 0)
            {
                return false;
            }
            return true;
        }
    }
}
