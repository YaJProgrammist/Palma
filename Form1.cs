using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palma
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Bitmap buf;
        private Point heops;
        private Point ramzes;
        private Point palkaH;
        private Point palkaR;
        private Point palma;
        private Point klad;
        private Color pyrColor;
        private Color palmaColor;
        private Color palkaColor;
        private Color kladColor;
        private Color heopsLinesColor;
        private Color ramzesLinesColor;
        private Color palkaLinesColor;
        private bool mouseIsDown;

        public Form1()
        {
            InitializeComponent();
            mouseIsDown = false;
            heops = new Point(200 + 40 * 2, 200);
            ramzes = new Point(200, 200 + 70 * 2);
            palma = new Point(200 - 100, 200 - 100 + 70 * 2);
            pyrColor = Color.Goldenrod;
            palmaColor = Color.Green;
            palkaColor = Color.Black;
            kladColor = Color.Peru;
            heopsLinesColor = Color.Crimson;
            ramzesLinesColor = Color.MidnightBlue;
            palkaLinesColor = Color.DimGray;
            RefreshCanvas();

            pictureBox1.Image = buf;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void RefreshCanvas()
        {
            DrawCanvas();
            palkaH = GetRightPalka(heops, heopsLinesColor);
            palkaR = GetLeftPalka(ramzes, ramzesLinesColor);
            klad = new Point((palkaH.X + palkaR.X) / 2, (palkaH.Y + palkaR.Y) / 2);
            BuildLines();
            DrawPyramid(heops);
            DrawPyramid(ramzes);
            DrawCircle(palma, palmaColor);
            DrawCircle(palkaH, palkaColor);
            DrawCircle(palkaR, palkaColor);
            DrawCircle(klad, kladColor);
            DrawTitle(heops, "Хеопс", pyrColor);
            DrawTitle(ramzes, "Рамзес", pyrColor);
            DrawTitle(palma, "Пальма", palmaColor);
            DrawTitle(palkaH, "Палка-копалка", palkaColor);
            DrawTitle(palkaR, "Палка-копалка", palkaColor);
            DrawTitle(klad, "КЛАД", kladColor);
        }

        private void DrawCanvas()
        {
            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height); 
            g = Graphics.FromImage(buf);
            g.Clear(Color.Khaki);
            
            pictureBox1.Image = buf;
        }

        private void DrawPyramid(Point pyr)
        {
            int pyrSize = 4; // 12 / 3

            Point p0 = new Point(pyr.X, pyr.Y - pyrSize * 2);
            Point p1 = new Point(pyr.X - (int)Math.Round(1.732 * pyrSize), pyr.Y + pyrSize);
            Point p2 = new Point(pyr.X + (int)Math.Round(1.732 * pyrSize), pyr.Y + pyrSize);

            SolidBrush solidbrush = new SolidBrush(pyrColor);
            Pen pen = new Pen(solidbrush);
            g.DrawPolygon(pen, new Point[] { p0, p1, p2 });
            g.FillPolygon(solidbrush, new Point[] { p0, p1, p2 });
        }

        private void DrawCircle(Point center, Color color)
        {
            int rad = 6;
            int diam = rad * 2;

            Rectangle fullRect = new Rectangle(center.X - rad, center.Y - rad, diam, diam);

            SolidBrush solidbrush = new SolidBrush(color);
            Pen pen = new Pen(solidbrush);
            g.DrawEllipse(pen, fullRect);
            g.FillEllipse(solidbrush, fullRect);
        }

        private void DrawTitle(Point pyr, String title, Color color)
        {
            SolidBrush solidbrush = new SolidBrush(color);
            Font titleFont = new Font(this.Font, FontStyle.Bold);
            Point titlePoint = new Point(pyr.X + 7, pyr.Y - 3);
            g.DrawString(title, titleFont, solidbrush, titlePoint);
        }

        private void BuildLines()
        {
            Pen heopsPen = new Pen(heopsLinesColor, 3);
            Pen ramzesPen = new Pen(ramzesLinesColor, 3);
            Pen palkaPen = new Pen(palkaLinesColor, 3);

            g.DrawLine(heopsPen, palma, heops);
            g.DrawLine(heopsPen, palkaH, heops);

            g.DrawLine(ramzesPen, palma, ramzes);
            g.DrawLine(ramzesPen, palkaR, ramzes);
            
            g.DrawLine(palkaPen, palkaH, palkaR);
        }

        private Point GetLeftPalka(Point pyr, Color color)
        {
            return new Point(pyr.X + (pyr.Y - palma.Y), pyr.Y - (pyr.X - palma.X));
        }

        private Point GetRightPalka(Point pyr, Color color)
        {
            return new Point(pyr.X - (pyr.Y - palma.Y), pyr.Y + (pyr.X - palma.X));
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseIsDown = true;
            palma = new Point(e.X, e.Y);
            RefreshCanvas();
            pictureBox1.Image = buf;
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                palma = new Point(e.X, e.Y);
                RefreshCanvas();
                pictureBox1.Image = buf;
            }
        }
    }
}
