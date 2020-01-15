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
using System.Windows.Shapes;

namespace QLBX.Views
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        public UserInfo()
        {
            InitializeComponent();
            this.Loaded += UserInfo_Loaded;
        }

        void UserInfo_Loaded(object sender, RoutedEventArgs e)
        {
            pb_oldCrpass.Password = String.Empty;
            pb_Crpass.Password = String.Empty;
            pb_reCrpass.Password = String.Empty;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ColorZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
