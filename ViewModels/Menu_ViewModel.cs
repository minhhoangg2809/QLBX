using MaterialDesignThemes.Wpf;
using QLBX.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QLBX.ViewModels
{
    class Menu_ViewModel : BaseViewModel
    {
        public ICommand Select_Command { get; set; }

        public Menu_ViewModel()
        {
            Select_Command = new RelayCommand<ListView>(p =>
            {
                return true;
            }, p =>
            {

                foreach (ListViewItem item in MyStaticMethods.FindVisualChildren<ListViewItem>(p))
                {
                    BrushConverter bc = new BrushConverter();
                    item.Foreground = (Brush)bc.ConvertFrom("#FF5C99D6");
                }

                Window wd = MyStaticMethods.getWindowParent(p) as Window;

                ListViewItem selectedItem = p.SelectedItem as ListViewItem;
                int i = Convert.ToInt32(selectedItem.Uid);

                if (wd != null)
                {
                    foreach (Frame item in MyStaticMethods.FindVisualChildren<Frame>(wd))
                    {
                        if (item.Name == "main_content")
                        {
                            Dieuhuong(i, item);
                        }
                        break;
                    }
                }

                selectedItem.Foreground = Brushes.White;
            });
        }

        private void Dieuhuong(int i, Frame fr)
        {
            switch (i)
            {
                case 1:
                    fr.Content = new Views.Pages.HomePage();
                    break;
                case 2:
                    fr.Content = new Views.Pages.CheckInPage();
                    break;
                case 3:
                    fr.Content = new Views.Pages.ReportPage();
                    break;
                case 4:
                    fr.Content = new Views.Pages.UsersPage();
                    break;
                case 5:
                    fr.Content = new Views.Pages.SpellDutyPage();
                    break;
                default:
                    break;
            }
        }
    }
}
