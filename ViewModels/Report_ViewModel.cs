using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLBX.Models.Custom;
using QLBX.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLBX.ViewModels
{
    public class Report_ViewModel : BaseViewModel
    {
        private ObservableCollection<Report> _List;

        public ObservableCollection<Report> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private Report _SelectedItem;

        public Report SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }


        private ObservableCollection<string> _ListStatus;

        public ObservableCollection<string> ListStatus
        {
            get { return _ListStatus; }
            set { _ListStatus = value; OnPropertyChanged(); }
        }

        private string _SelectedStatus;

        public string SelectedStatus
        {
            get { return _SelectedStatus; }
            set { _SelectedStatus = value; OnPropertyChanged(); }
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

        private bool _IsUserOpen;

        public bool IsUserOpen
        {
            get { return _IsUserOpen; }
            set { _IsUserOpen = value; OnPropertyChanged(); }
        }


        public ICommand Load_Command { get; set; }
        public ICommand Report_Command { get; set; }
        public ICommand SortbyLicense_Command { get; set; }
        public ICommand SortbyCardId_Command { get; set; }
        public ICommand SortbyTimeIn_Command { get; set; }
        public ICommand SortbyTimeOut_Command { get; set; }
        public ICommand SortbyTime_Command { get; set; }
        public ICommand SortbyType_Command { get; set; }
        public ICommand Export_Command { get; set; }
        public ICommand OpenUser_Command { get; set; }
        public ICommand CloseDetail_Command { get; set; }
        public Report_ViewModel()
        {
            Load_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                List<Models.CheckInOut> listBase = Models.DataProvider.Ins.DB.CheckInOuts.ToList();
                listBase = new List<Models.CheckInOut>(listBase.Where(x => x.checkInTime.Value.Date == DateTime.Today));
                List = new ObservableCollection<Report>();
                for (int i = 0; i < listBase.Count(); i++)
                {
                    List.Add(new Report()
                    {
                        id = listBase[i].id,
                        license = listBase[i].license,
                        cardId = listBase[i].cardId,
                        checkInTime = listBase[i].checkInTime,
                        checkOutTime = listBase[i].checkOutTime,
                        Price = listBase[i].Price,
                        checkInUserName = listBase[i].checkInUserName,
                        checkOutUserName = listBase[i].checkOutUserName
                    });
                }

                ListStatus = new ObservableCollection<string>();
                ListStatus.Add("Xe vào");
                ListStatus.Add("Xe ra");

                SelectedStatus = String.Empty;
                TimeStart = String.Empty;
                TimeEnd = String.Empty;

                IsUserOpen = false;
            });

            CloseDetail_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                IsUserOpen = false;
            });

            OpenUser_Command = new RelayCommand<Button>(p =>
            {
                return true;
            }, p =>
            {
                SelectedItem = new Report();
                int id = Convert.ToInt32(p.Uid);
                SelectedItem = List.Where(x => x.id == id).SingleOrDefault();
                IsUserOpen = true;
            });

            #region Report
            Report_Command = new RelayCommand<object>(p =>
            {
                if (IsUserOpen == true)
                    return false;

                return true;
            }, p =>
            {
                List<Models.CheckInOut> listBase = Models.DataProvider.Ins.DB.CheckInOuts.ToList();
                listBase = new List<Models.CheckInOut>(listBase.Where(x => x.checkInTime.Value.Date == DateTime.Today));

                List = new ObservableCollection<Report>();
                for (int i = 0; i < listBase.Count(); i++)
                {
                    List.Add(new Report()
                    {
                        id = listBase[i].id,
                        license = listBase[i].license,
                        cardId = listBase[i].cardId,
                        checkInTime = listBase[i].checkInTime,
                        checkOutTime = listBase[i].checkOutTime,
                        Price = listBase[i].Price,
                        checkInUserName = listBase[i].checkInUserName,
                        checkOutUserName = listBase[i].checkOutUserName
                    });
                }

                if (!String.IsNullOrEmpty(TimeStart))
                {
                    DateTime timeStart = MyStaticMethods.ConvertTime(TimeStart);
                    List = new ObservableCollection<Report>(List.Where(x =>
                    {
                        if (x.checkOutTime != null)
                        {
                            return x.checkInTime.Value >= timeStart || x.checkOutTime.Value >= timeStart;
                        }
                        return x.checkInTime >= timeStart;
                    }));
                }
                if (!String.IsNullOrEmpty(TimeEnd))
                {
                    DateTime timeEnd = MyStaticMethods.ConvertTime(TimeEnd);
                    List = new ObservableCollection<Report>(List.Where(x =>
                    {
                        if (x.checkOutTime != null)
                        {
                            return x.checkInTime.Value <= timeEnd || x.checkOutTime.Value <= timeEnd;
                        }
                        return x.checkInTime.Value <= timeEnd;
                    }));
                }
                if (!String.IsNullOrEmpty(SelectedStatus))
                {
                    DateTime timeStart = new DateTime();
                    DateTime timeEnd = new DateTime();

                    if (!String.IsNullOrEmpty(TimeStart))
                    {
                        timeStart = MyStaticMethods.ConvertTime(TimeStart);
                    }
                    else
                    {
                        timeStart = MyStaticMethods.ConvertTime("00:00");
                    }
                    if (!String.IsNullOrEmpty(TimeEnd))
                    {
                        timeEnd = MyStaticMethods.ConvertTime(TimeEnd);
                    }
                    else
                    {
                        timeEnd = DateTime.Now;
                    }

                    if (SelectedStatus == "Xe vào")
                    {
                        List = new ObservableCollection<Report>(List.Where(x => x.checkInTime.Value >= timeStart && x.checkInTime.Value <= timeEnd));
                    }
                    if (SelectedStatus == "Xe ra")
                    {
                        List = new ObservableCollection<Report>(List.Where(x =>
                            {
                                if (x.checkOutTime != null)
                                {
                                    return x.checkOutTime.Value >= timeStart && x.checkOutTime.Value <= timeEnd;
                                }
                                return x.id < 0;
                            }));
                    }
                }
            });
            #endregion

            #region Export Excel

            Export_Command = new RelayCommand<object>(p =>
            {
                if (IsUserOpen == true)
                    return false;

                if (List.Count() == 0)
                    return false;

                return true;
            }, p =>
            {
                string filePath = "*.xlsx";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialogs = new SaveFileDialog();

                // chỉ lọc ra các file có định dạng Excel
                dialogs.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialogs.ShowDialog() == true)
                {
                    filePath = dialogs.FileName;
                }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                    return;
                }
                try
                {
                    using (ExcelPackage pk = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        pk.Workbook.Properties.Author = "admin";

                        // đặt tiêu đề cho file
                        pk.Workbook.Properties.Title = "QLBX UTT";

                        //Tạo một sheet để làm việc trên đó
                        pk.Workbook.Worksheets.Add("QLBX sheet");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = pk.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = "QLBX sheet";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {  
                                                "Biển số xe",
                                                "Thẻ xe",
                                                "Thời gian vào",
                                                "Thời gian ra",
                                                "Loại xe",
                                                "Thời gian gửi (h)",
                                                "Giá tiền (đ)",
                                                "Nhân viên Check-In",
                                                "Nhân viên Check-Out"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tin
                        ws.Cells[1, 1].Value = "Thống kê";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 2;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        // lấy ra danh sách UserInfo từ ItemSource của DataGrid
                        List<Report> userList = List.Cast<Report>().ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell      
                            ws.Cells[rowIndex, colIndex++].Value = item.license;
                            ws.Cells[rowIndex, colIndex++].Value = item.cardId;
                            ws.Cells[rowIndex, colIndex++].Value = item.checkInTime.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.checkOutTime.ToString();
                            ws.Cells[rowIndex, colIndex++].Value = item.Price.veh_type;
                            ws.Cells[rowIndex, colIndex++].Value = item.Time.ToString() + "h";
                            ws.Cells[rowIndex, colIndex++].Value = item.Price.price1.ToString() + "đ";
                            ws.Cells[rowIndex, colIndex++].Value = item.checkInUserName;
                            ws.Cells[rowIndex, colIndex++].Value = item.checkOutUserName;
                        }

                        //Lưu file lại
                        Byte[] bin = pk.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Thành công !!!", "THÔNG BÁO");
                    Process.Start(filePath);
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi khi lưu file !!!", "THÔNG BÁO");
                }

            });

            #endregion

            // sap xep bang (click vao header)
            #region Sort

            SortbyType_Command = new RelayCommand<object>(p => 
            {
                return true;
            }, p => 
            {
                ObservableCollection<Report> chkList = new ObservableCollection<Report>(List.OrderByDescending(x => x.Price.veh_type));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<Report>(List.OrderBy(x => x.Price.veh_type));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyLicense_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<Report> chkList = new ObservableCollection<Report>(List.OrderByDescending(x => x.license));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<Report>(List.OrderBy(x => x.license));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyCardId_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<Report> chkList = new ObservableCollection<Report>(List.OrderByDescending(x => x.cardId));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<Report>(List.OrderBy(x => x.cardId));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyTimeIn_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<Report> chkList = new ObservableCollection<Report>(List.OrderByDescending(x => x.checkInTime));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<Report>(List.OrderBy(x => x.checkInTime));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyTimeOut_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<Report> chkList = new ObservableCollection<Report>(List.OrderByDescending(x => x.checkOutTime));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<Report>(List.OrderBy(x => x.checkOutTime));
                }
                else
                {
                    List = chkList;
                }
            });

            SortbyTime_Command = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ObservableCollection<Report> chkList = new ObservableCollection<Report>(List.OrderByDescending(x => x.Time));

                if (List[0] == chkList[0])
                {
                    List = new ObservableCollection<Report>(List.OrderBy(x => x.Time));
                }
                else
                {
                    List = chkList;
                }
            });

            #endregion

        }
    }
}
