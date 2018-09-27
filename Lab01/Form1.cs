using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        double kPerp;
        double xStep = 1;
        double kx, ky;
        Shapes.Rectangle rect;
        Line line, perp;
        System.Threading.Timer t;

        public Form1()
        {
            InitializeComponent();
            
            bmp = new Bitmap(this.canvas.Width, this.canvas.Height);
            graphics = Graphics.FromImage(bmp);

            double a = double.Parse(this.A.Text);
            double b = double.Parse(this.B.Text);
            double c = double.Parse(this.C.Text);
            kPerp = b / a;
            line = new Line((x) => (-(a / b) * x - c / b));
            perp = new Line((x) => kPerp * (x - float.Parse(this.sX.Text)) + float.Parse(this.sY.Text));


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                t?.Dispose();
                sXc = float.Parse(this.sX.Text);
                sYc = float.Parse(this.sY.Text);
                sideS = float.Parse(this.side.Text);
                rect = new Shapes.Rectangle(sXc, sYc, sideS);

                double a = double.Parse(this.A.Text);
                double b = double.Parse(this.B.Text);
                double c = double.Parse(this.C.Text);
                line = new Line((x) => ((-a / b) * x - c / b));
                perp = new Line((x) => (Math.Tan(Math.Atan((-a/b))-Math.PI/2) * x + GetC(a, -b, double.Parse(this.sX.Text), double.Parse(this.sY.Text))));

                graphics.Clear(Color.White);
                DrawLine();
                DrawSquare();
                CrossPoint();
                t = new System.Threading.Timer(new TimerCallback((object o) => { this.graphics.Clear(Color.White); DrawLine(); DrawSquare(); this.canvas.Image = bmp; }), 0, 1000, 1);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                MessageBox.Show(exc.StackTrace);
            }
        }

        private void DrawLine()
        {
            using (Pen pen = new Pen(Color.Red))
            {
                line.Draw(graphics, pen);
                pen.Color = Color.Green;
                //perp.Draw(graphics, pen);
                graphics.DrawLine(pen, (float)sXc, (float)sYc, (float)kx, (float)ky);
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
                rect.Move((float)(-xStep), (float)(-xStep/kPerp));
                if (line.Contains(rect.P1) || line.Contains(rect.P2) || line.Contains(rect.P3) || line.Contains(rect.P4))
                {
                    t?.Dispose();
                }
                alpha = (alpha + 1) % 360;
                textBox1.Text = alpha.ToString();
                rect.Draw(graphics, pen);
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
                    MessageBox.Show($"{kx} ; {ky}");
                }

                t += xStep;
            }
        }

        private void Update()
        {
            this.graphics.Clear(Color.White);
            this.canvas.Image = bmp;
        }
    }
}
