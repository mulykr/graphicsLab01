// <copyright file="Square.cs" company="LNU">
// All rights reserved.
// </copyright>
// <author>Roman Mulyk</author>

namespace Shapes
{
    using System.Drawing;
    using Interfaces;

    /// <summary>
    /// The instance of circle
    /// </summary>
    public class Rectangle : IDrawable
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }
        public Point P4 { get; set; }
        public Point Center { get; set; }
        
        public Rectangle(Point p1, Point p2, Point p3, Point p4)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }

        public Rectangle(float cx, float cy, float side)
        {
            float hs = side / 2;
            P1 = new Point(cx - hs, cy - hs);
            P2 = new Point(cx + hs, cy - hs);
            P3 = new Point(cx + hs, cy + hs);
            P4 = new Point(cx - hs, cy + hs);
            Center = new Point(cx, cy);
        }
        
        /// <summary>
        /// Draws rectangle
        /// </summary>
        /// <param name="graphics">Graphic canvas</param>
        /// <param name="pen">Pen to draw</param>
        public void Draw(Graphics graphics, Pen pen)
        {
            graphics.DrawLine(pen, P1.X, P1.Y, P2.X, P2.Y);
            graphics.DrawLine(pen, P2.X, P2.Y, P3.X, P3.Y);
            graphics.DrawLine(pen, P3.X, P3.Y, P4.X, P4.Y);
            graphics.DrawLine(pen, P1.X, P1.Y, P4.X, P4.Y);
        }

        public void Rotate(double alpha)
        {
            P1.Rotate(alpha, Center);
            P2.Rotate(alpha, Center);
            P3.Rotate(alpha, Center);
            P4.Rotate(alpha, Center);
        }

        public void Move(float dx, float dy)
        {
            P1.X += dx;
            P2.X += dx;
            P3.X += dx;
            P4.X += dx;

            P1.Y += dy;
            P2.Y += dy;
            P3.Y += dy;
            P4.Y += dy;

            Center.X += dx;
            Center.Y += dy;
        }
    }
}