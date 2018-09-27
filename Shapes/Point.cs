﻿// <copyright file="Point.cs" company="LNU">
// All rights reserved.
// </copyright>
// <author>Roman Mulyk</author>

namespace Shapes
{
    using System;

    /// <summary>
    /// The instance of point
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the x coordinate
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the y coordinate
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets distance between two points
        /// </summary>
        /// <param name="one">First point</param>
        /// <param name="two">Second point</param>
        /// <returns>The distance</returns>
        public static double Distance(Point one, Point two)
        {
            if (one == null || two == null)
            {
                throw new ArgumentNullException("Invalid argument");
            }

            double distance = Math.Sqrt(Math.Pow(one.X - two.X, 2) + Math.Pow(one.Y - two.Y, 2));
            return distance;
        }

        /// <summary>
        /// Checks if points are located on one line
        /// </summary>
        /// <param name="points">Points to check</param>
        /// <returns>True, if Points are on one line</returns>
        public static bool OnOneLine(params Point[] points)
        {
            if (points.Length <= 2)
            {
                return true;
            }

            Point p1 = points[0], p2 = points[1];

            for (int i = 2; i < points.Length; i++)
            {
                if ((p2.X - p1.X) * (points[i].Y - p1.Y) != (points[i].X - p1.X) * (p2.Y - p1.Y))
                {
                    return false;
                }
            }

            return true;
        }

        public void Rotate(double alpha, Point center)
        {
            alpha = Math.PI * alpha / 180;
            float tx = (float)(center.X + (this.X - center.X) * (float)Math.Cos(alpha) - (this.Y - center.Y) * (float)Math.Sin(alpha));
            float ty = (float)(center.Y + (this.Y - center.Y) * (float)Math.Cos(alpha) + (this.X - center.X) * (float)Math.Sin(alpha));
            this.X = tx;
            this.Y = ty;
        }
    }
}