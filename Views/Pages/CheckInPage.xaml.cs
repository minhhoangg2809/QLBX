using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using LaptrinhVBLibs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tesseract;

namespace QLBX.Views.Pages
{
    /// <summary>
    /// Interaction logic for CheckInPage.xaml
    /// </summary>
    public partial class CheckInPage : Page
    {
        public PictureBox pic1;
        public PictureBox pic2;
        public Emgu.CV.UI.ImageBox img1;
        public CheckInPage()
        {
            InitializeComponent();
            this.Loaded += CheckInPage_Loaded;
        }

        #region define
        List<Image<Bgr, Byte>> PlateImagesList = new List<Image<Bgr, byte>>();
        List<string> PlateTextList = new List<string>();
        List<System.Drawing.Rectangle> listRect = new List<System.Drawing.Rectangle>();
        PictureBox[] box = new PictureBox[12];

        public TesseractProcessor full_tesseract = null;
        public TesseractProcessor ch_tesseract = null;
        public TesseractProcessor num_tesseract = null;
        private string m_path = System.AppDomain.CurrentDomain.BaseDirectory + @"\data\";
        private List<string> lstimages = new List<string>();
        private const string m_lang = "eng";
        #endregion

        void CheckInPage_Loaded(object sender, RoutedEventArgs e)
        {
            full_tesseract = new TesseractProcessor();
            bool succeed = full_tesseract.Init(m_path, m_lang, 3);
            if (!succeed)
            {
                System.Windows.MessageBox.Show("Lỗi thư viện Tesseract. Chương trình cần kết thúc.");
            }
            full_tesseract.SetVariable("tessedit_char_whitelist", "ACDFHKLMNPRSTVXY1234567890").ToString();

            ch_tesseract = new TesseractProcessor();
            succeed = ch_tesseract.Init(m_path, m_lang, 3);
            if (!succeed)
            {
                System.Windows.MessageBox.Show("Lỗi thư viện Tesseract. Chương trình cần kết thúc.");
            }
            ch_tesseract.SetVariable("tessedit_char_whitelist", "ACDEFHKLMNPRSTUVXY").ToString();

            num_tesseract = new TesseractProcessor();
            succeed = num_tesseract.Init(m_path, m_lang, 3);
            if (!succeed)
            {
                System.Windows.MessageBox.Show("Lỗi thư viện Tesseract. Chương trình cần kết thúc.");
            }
            num_tesseract.SetVariable("tessedit_char_whitelist", "1234567890").ToString();

            System.Environment.CurrentDirectory = System.IO.Path.GetFullPath(m_path);
            for (int i = 0; i < box.Length; i++)
            {
                box[i] = new PictureBox();
            }
            string folder = System.AppDomain.CurrentDomain.BaseDirectory + "\\ImageTest";
            foreach (string fileName in Directory.GetFiles(folder, "*.bmp", SearchOption.TopDirectoryOnly))
            {
                lstimages.Add(System.IO.Path.GetFullPath(fileName));
            }
            foreach (string fileName in Directory.GetFiles(folder, "*.jpg", SearchOption.TopDirectoryOnly))
            {
                lstimages.Add(System.IO.Path.GetFullPath(fileName));
            }

            pic1 = (hostPic1.Child as System.Windows.Forms.PictureBox);
            pic2 = (hostPic2.Child as System.Windows.Forms.PictureBox);
            img1 = (hostImg1.Child as Emgu.CV.UI.ImageBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image (*.bmp; *.jpg; *.jpeg; *.png) |*.bmp; *.jpg; *.jpeg; *.png|All files (*.*)|*.*||";
                dlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "\\ImageTest";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                string startupPath = dlg.FileName;

                ProcessImage(startupPath);
                if (PlateImagesList.Count != 0)
                {
                    Image<Bgr, byte> src = new Image<Bgr, byte>(PlateImagesList[0].ToBitmap());
                    Bitmap grayframe;
                    Bitmap color;
                    //Tìm biên
                    int c = clsBSoft.IdentifyContours(src.ToBitmap(), 50, false, out grayframe, out color, out listRect);
                    pic1.Image = color;
                    pic2.Image = grayframe;
                    Image<Gray, byte> dst = new Image<Gray, byte>(grayframe);
                    grayframe = dst.ToBitmap();
                    //tạo chuỗi rỗng để hứng
                    string upp = "";
                    string down = "";

                    // lọc và sắp xếp số
                    List<Bitmap> bmp = new List<Bitmap>();
                    List<int> erode = new List<int>();
                    List<System.Drawing.Rectangle> up = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> dow = new List<System.Drawing.Rectangle>();
                    int up_y = 0, dow_y = 0;
                    bool flag_up = false;

                    int di = 0;

                    if (listRect == null) return;

                    for (int i = 0; i < listRect.Count; i++)
                    {
                        Bitmap ch = grayframe.Clone(listRect[i], grayframe.PixelFormat);
                        int cou = 0;
                        full_tesseract.Clear();
                        full_tesseract.ClearAdaptiveClassifier();
                        string temp = full_tesseract.Apply(ch);
                        while (temp.Length > 3)
                        {
                            Image<Gray, byte> temp2 = new Image<Gray, byte>(ch);
                            temp2 = temp2.Erode(2);
                            ch = temp2.ToBitmap();
                            full_tesseract.Clear();
                            full_tesseract.ClearAdaptiveClassifier();
                            temp = full_tesseract.Apply(ch);
                            cou++;
                            if (cou > 10)
                            {
                                listRect.RemoveAt(i);
                                i--;
                                di = 0;
                                break;
                            }
                            di = cou;
                        }
                    }

                    for (int i = 0; i < listRect.Count; i++)
                    {
                        for (int j = i; j < listRect.Count; j++)
                        {
                            if (listRect[i].Y > listRect[j].Y + 100)
                            {
                                flag_up = true;
                                up_y = listRect[j].Y;
                                dow_y = listRect[i].Y;
                                break;
                            }
                            else if (listRect[j].Y > listRect[i].Y + 100)
                            {
                                flag_up = true;
                                up_y = listRect[i].Y;
                                dow_y = listRect[j].Y;
                                break;
                            }
                            if (flag_up == true) break;
                        }
                    }

                    for (int i = 0; i < listRect.Count; i++)
                    {
                        if (listRect[i].Y < up_y + 50 && listRect[i].Y > up_y - 50)
                        {
                            up.Add(listRect[i]);
                        }
                        else if (listRect[i].Y < dow_y + 50 && listRect[i].Y > dow_y - 50)
                        {
                            dow.Add(listRect[i]);
                        }
                    }

                    if (flag_up == false) dow = listRect;

                    for (int i = 0; i < up.Count; i++)
                    {
                        for (int j = i; j < up.Count; j++)
                        {
                            if (up[i].X > up[j].X)
                            {
                                System.Drawing.Rectangle w = up[i];
                                up[i] = up[j];
                                up[j] = w;
                            }
                        }
                    }
                    for (int i = 0; i < dow.Count; i++)
                    {
                        for (int j = i; j < dow.Count; j++)
                        {
                            if (dow[i].X > dow[j].X)
                            {
                                System.Drawing.Rectangle w = dow[i];
                                dow[i] = dow[j];
                                dow[j] = w;
                            }
                        }
                    }

                    int x = 0;
                    int c_x = 0;

                    for (int i = 0; i < up.Count; i++)
                    {
                        Bitmap ch = grayframe.Clone(up[i], grayframe.PixelFormat);
                        Bitmap o = ch;
                        string temp;
                        if (i < 2)
                        {
                            temp = clsBSoft.Ocr(ch, false, full_tesseract, num_tesseract, ch_tesseract, true); // nhan dien so
                        }
                        else
                        {
                            temp = clsBSoft.Ocr(ch, false, full_tesseract, num_tesseract, ch_tesseract, false); // nhan dien chu
                        }

                        upp += temp;
                        box[i].Location = new System.Drawing.Point(x + i * 50, 0);
                        box[i].Size = new System.Drawing.Size(50, 100);
                        box[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        box[i].Image = ch;
                        c_x++;
                    }

                    for (int i = 0; i < dow.Count; i++)
                    {
                        Bitmap ch = grayframe.Clone(dow[i], grayframe.PixelFormat);
                        string temp = clsBSoft.Ocr(ch, false, full_tesseract, num_tesseract, ch_tesseract, true); // nhan dien so

                        down += temp;
                        box[i + c_x].Location = new System.Drawing.Point(x + i * 50, 100);
                        box[i + c_x].Size = new System.Drawing.Size(50, 100);
                        box[i + c_x].SizeMode = PictureBoxSizeMode.StretchImage;
                        box[i + c_x].Image = ch;
                    }
                    tblNhandien.Text = upp.Replace("\n", "") + "\n" + down.Replace("\n", "");
                    tbNhandien.Text = upp.Replace("\n", "") + "_" + down.Replace("\n", "");
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("tràn RAM");
            }

        }

        private void ProcessImage(string urlImage)
        {
            try
            {
                PlateImagesList.Clear();
                PlateTextList.Clear();
                Bitmap img = new Bitmap(urlImage);
                pic2.Image = img;
                pic1.Update();
                clsBSoft.FindLicensePlate(img, pic1, img1, PlateImagesList, null);
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(ex.ToString());
            }

        }

    }
}
