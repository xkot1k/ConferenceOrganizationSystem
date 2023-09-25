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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Interop;

namespace ConferenceOrganizationSystem.UI.Views
{

    public partial class wndCaptha : Window
    {
        private const string CaptchaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int CaptchaLength = 5;
        private string captchaText = string.Empty;

        public wndCaptha()
        {
            InitializeComponent();
            imgCaptcha.Source = CreateImage();
        }

        private BitmapSource CreateImage()
        {
            int width = 300;
            int height = 75;

            Random rand = new Random();

            Bitmap result = new Bitmap(width, height);

            int Xpos = rand.Next(0, width - 60);
            int Ypos = rand.Next(0, height - 20);

            System.Drawing.Brush[] colors = { System.Drawing.Brushes.Black,
                                 System.Drawing.Brushes.Red,
                                 System.Drawing.Brushes.RoyalBlue,
                                 System.Drawing.Brushes.Green};

            Graphics graphics = Graphics.FromImage((System.Drawing.Image)result);

            graphics.Clear(Color.Gray);

            captchaText = GenerateRandomCaptchaText();

            graphics.DrawString(captchaText, new Font("Arial", 15),
                colors[rand.Next(colors.Length)], new PointF(Xpos, Ypos));

            graphics.DrawLine(Pens.Black,
                new System.Drawing.Point(0, 0),
                new System.Drawing.Point(width - 1, height - 1));

            graphics.DrawLine(Pens.Black,
                new System.Drawing.Point(0, height - 1),
                new System.Drawing.Point(width - 1, 0));

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (rand.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                result.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return bitmapSource;

        }

        private string GenerateRandomCaptchaText()
        {
            Random random = new Random();
            string text = "";
            for (int i = 0; i < CaptchaLength; i++)
            {
                text += CaptchaCharacters[random.Next(CaptchaCharacters.Length)];
            }
            return text;
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {

            string textInput = txtEnterText.Text;

            if (textInput == captchaText)
            {
                UI.Views.wndAuthorization wnd = new wndAuthorization();
                wnd.Show();
                this.Close();
                MessageBox.Show("Проверка пройдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Вы не прошли проверку, попробуйте снова!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                imgCaptcha.Source = CreateImage();
                txtEnterText.Text = string.Empty;
            }

        }

    }
}
