using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QLBX.Resource
{
    public class MyStaticMethods
    {
        public static bool CheckEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                sb.Append(c);
            }
            if (lowerCase)
                return sb.ToString().ToLower();
            return sb.ToString();

        }

        public static string RandomInt(int size)
        {
            string sb = "";
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                sb += (rand.Next(0, 9)).ToString();
            }

            return sb;

        }

        public static DateTime ConvertStringtoDate(string str)
        {
            string[] arrdate = str.Split('/');

            DateTime Myreal_Date = new DateTime(Convert.ToInt32(arrdate[2]), Convert.ToInt32(arrdate[1]), Convert.ToInt32(arrdate[0]));

            return Myreal_Date;
        }

        public static string FormatDateString(string str)
        {
            DateTime XDate = Convert.ToDateTime(str);
            string resultDate = XDate.Day + "-" + XDate.Month + "-" + XDate.Year;

            return resultDate;
        }

        public static string ConvertDate2Vn_Today()
        {
            DateTime date = DateTime.Today;
            string resultDate = date.Day + "/" + date.Month + "/" + date.Year;

            return resultDate;
        }

        public static string DateVNtoUS(string str)
        {
            DateTime Myreal_Date = MyStaticMethods.ConvertStringtoDate(str);
            return (Myreal_Date.Month.ToString() + "/" + Myreal_Date.Day.ToString() + "/" + Myreal_Date.Year.ToString());
        }

        public static DateTime ConvertTime(string str)
        {
            string[] parts = str.Split(new[] { ':' });
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]),0);
            return dt;
        }

        public static DateTime ConvertTimeSpell(string str)
        {
            string[] parts = str.Split(new[] { ':' });
            DateTime dt = new DateTime(DateTime.Now.AddDays(+1).Year, DateTime.Now.AddDays(+1).Month, DateTime.Now.AddDays(+1).Day, Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), 0);
            return dt;
        }

        public static FrameworkElement getWindowParent(Control crl)
        {
            FrameworkElement p = crl as FrameworkElement;

            while (p.Parent != null)
            {
                p = p.Parent as FrameworkElement;
            }

            return p;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {

                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                    {

                        yield return (T)child;

                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
