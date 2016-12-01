namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Contains all members to draw custom lines with the mouse on the form.
    /// </summary>
    public class MousePaint : Polygon
    {
        /// <summary>
        /// Initializes a new instance of the MousePaint class.
        /// </summary>
        public MousePaint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MousePaint class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="color">The color.</param>
        public MousePaint(Point start, Color color)
        {
            this.FillColor = color;
            this.PolygonPoints = new List<Point>();
            this.PolygonPoints.Add(start);
        }

        /// <inheritdoc/>
        public override void Draw(Graphics graphics)
        {
            try
            {
                graphics.DrawCurve(new Pen(this.FillColor), this.PolygonPoints.ToArray());
            }
            catch (ArgumentException)
            {
            }
        }

        /// <inheritdoc/>
        public override bool PointIsInShape(Point point)
        {
            foreach (var p in this.PolygonPoints)
            {
                if (Math.Abs(p.X - point.X) < 10 &&
                    Math.Abs(p.Y - point.Y) < 10)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
