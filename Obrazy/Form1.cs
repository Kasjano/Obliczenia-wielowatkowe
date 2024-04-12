using System.Windows.Forms;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Security.Policy;

namespace Obrazy
{
    public partial class Form1 : Form
    {
        private Bitmap? img;
        private Bitmap img1;
        private Bitmap img2;
        private Bitmap img3;
        private Bitmap img4;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            label1.Text = null;
            if (file != null)
            {
                img = new Bitmap(file);
                pictureBox1.Image = img;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                label1.Text = ("Please load an image first!");
            }
            else
            {
                label1.Text = null;

                pictureBox2.Image = null;
                pictureBox3.Image = null;
                pictureBox4.Image = null;
                pictureBox5.Image = null;

                img1= new Bitmap(img);
                img2 = new Bitmap(img);
                img3 = new Bitmap(img);
                img4 = new Bitmap(img);

                Thread szaroscThread = new Thread(() =>
                {
                    pictureBox2.Image = Szaroœæ(img1);
                });

                Thread negatywThread = new Thread(() =>
                {
                    pictureBox3.Image = Negatyw(img2);
                });

                Thread zielonyThread = new Thread(() =>
                {
                    pictureBox4.Image = Zielony(img3);
                });

                Thread odbicieLustrzaneThread = new Thread(() =>
                {
                    pictureBox5.Image = OdbicieLustrzane(img4);
                });

                szaroscThread.Start();
                negatywThread.Start();
                zielonyThread.Start();
                odbicieLustrzaneThread.Start();
            }
        }

        public static Bitmap Szaroœæ(Bitmap img)
        {
            Bitmap szaroœæ = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    int grayscaleValue = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                    Color grayscaleColor = Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue);
                    szaroœæ.SetPixel(x, y, grayscaleColor);
                }
            }

            return szaroœæ;
        }

        public static Bitmap Negatyw(Bitmap img)
        {
            Bitmap negatyw = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    Color invertedPixel = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    negatyw.SetPixel(x, y, invertedPixel);
                }
            }

            return negatyw;
        }

        public static Bitmap Zielony(Bitmap img)
        {
            Bitmap zielony = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    int newRed = Math.Max(0, pixel.R - 100);
                    int newGreen = Math.Min(255, pixel.G + 100);
                    int newBlue = Math.Max(0, pixel.B - 100);

                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    zielony.SetPixel(x, y, newColor);
                }
            }

            return zielony;
        }

        public static Bitmap OdbicieLustrzane(Bitmap img)
        {
            Bitmap lustro = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    int newX = img.Width - 1 - x;
                    lustro.SetPixel(newX, y, pixel);
                }
            }

            return lustro;
        }
    }
}
