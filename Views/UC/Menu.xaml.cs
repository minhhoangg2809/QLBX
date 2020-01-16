using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLBX.Views.UC
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            this.Loaded += Menu_Loaded;
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModels.Login_ViewModel.CurrentUser != null)
            {
                if (ViewModels.Login_ViewModel.CurrentUser.user_role != 1)
                {
                    foreach (ListViewItem item in ListViewMenu.Items)
                    {
                        if (item.Uid == "4" || item.Uid == "5")
                        {
                            item.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }
    }
}
