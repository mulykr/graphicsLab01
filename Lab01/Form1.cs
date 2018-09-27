using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Shapes;

namespace Lab01
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Bitmap bmp;
        int alpha = 0;
        float sXc, sYc;
        float sideS;
        double xStep = 0.5;
        double kx, ky, kPerp;
        int moveKx = 1, moveKy = 1;
        Shapes.Rectangle rect;
        Line line, perp;
        System.Threading.Timer t;

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(this.canvas.Width, this.canvas.Height);
            graphics = Graphics.FromImage(bmp);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                t?.Dispose();

                double a = double.Parse(this.A.Text);
                double b = double.Parse(this.B.Text);
                double c = double.Parse(this.C.Text);

                sXc = float.Parse(this.sX.Text);
                sYc = float.Parse(this.sY.Text);
                sideS = float.Parse(this.side.Text);

                rect = new Shapes.Rectangle(new Shapes.Point(sXc - sideS, sYc - sideS), new Shapes.Point(sXc + sideS, sYc - sideS), new Shapes.Point(sXc + sideS, sYc + sideS), new Shapes.Point(sXc - sideS, sYc + sideS));
                rect.Center = new Shapes.Point(sXc, sYc);
                line = new Line((x) => (-(a / b) * x - c / b));

                kPerp = b / a;
                perp = new Line((x) => (b / a) * (x - sXc) + sYc);


                graphics.Clear(Color.White);

                CrossPoint();

                if (kx <= sXc && ky <= sYc)
                {
                    moveKx = -1;
                    moveKy = -1;
                }
                else if (kx <= sXc && ky >= sYc)
                {
                    moveKx = -1;
                    moveKy = 1;
                }
                else if (kx >= sXc && ky <= sYc)
                {
                    moveKx = 1;
                    moveKy = -1;
                }
                else if (kx >= sXc && ky >= sYc)
                {
                    moveKx = 1;
                    moveKy = 1;
                }
                else
                {
                    throw new ArgumentException("Incorrect data!");
                }

                t = new System.Threading.Timer(new TimerCallback((object o) => { this.graphics.Clear(Color.White); DrawLine(); DrawSquare(); this.canvas.Image = bmp; }), 0, 1000, 1);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                MessageBox.Show(exc.StackTrace);
            }
        }

        private void A_TextChanged(object sender, EventArgs e)
        {

        }

        private void DrawLine()
        {
            using (Pen pen = new Pen(Color.Red))
            {
                line.Draw(graphics, pen);
                pen.Color = Color.Green;
                perp.Draw(graphics, pen);
            }
        }

        private double GetC(double a, double b, double x, double y)
        {
            return a * x + b * y;
        }

        private void DrawSquare()
        {
            using (Pen pen = new Pen(Color.Black))
            {
                rect.Rotate(1);
                rect.Move((float)(moveKx * xStep), (float)(moveKy * Math.Abs(kPerp) * xStep));
                if (line.Contains(rect.P1) || line.Contains(rect.P2) || line.Contains(rect.P3) || line.Contains(rect.P4))
                {
                    t?.Dispose();
                }
                alpha = (alpha + 1) % 360;
                rect?.Draw(graphics, pen);
            }
        }

        private void CrossPoint()
        {
            double t = 0;
            while (t <= this.Width)
            {
                if (Math.Abs(line.InvokeFunc(t) - perp.InvokeFunc(t)) <= xStep)
                {
                    kx = t;
                    ky = line.InvokeFunc(t);
                    MessageBox.Show("Cross: " + kx + " " + ky);
                    return;
                }

                t += xStep;
            }
        }

        private new void Update()
        {
            this.graphics.Clear(Color.White);
            this.canvas.Image = bmp;
        }
    }
}