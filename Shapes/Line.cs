namespace Shapes
{
    using System;
    using System.Drawing;
    public class Line
    {
        public Line(Func<double, double> func)
        {
            Func = func;
        }

        public Func<double, double> Func { get; set; }

        public double InvokeFunc(double x)
        {
            return (double)Func?.Invoke(x);
        }

        public bool Contains(Point x)
        {
            return Math.Abs(Func(x.X) - x.Y) < 1;
        }

        /// <summary>
        /// Draws rectangle
        /// </summary>
        /// <param name="graphics">Graphic canvas</param>
        /// <param name="pen">Pen to draw</param>
        public void Draw(Graphics graphics, Pen pen)
        {
            PointF p1 = new PointF(0, (float)Func(0));
            PointF p2 = new PointF(500, (float)Func(500));
            graphics.DrawLine(pen, p1, p2);
        }
    }
}
