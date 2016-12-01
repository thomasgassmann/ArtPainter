namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains all members of an ellipse.
    /// </summary>
    public class Ellipse : Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the Ellipse class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="color">The border color.</param>
        /// <param name="isMoveable">A value indicating whether the shape is moveable.</param>
        public Ellipse(
            Point start,
            Point end,
            int border,
            Color color,
            bool isMoveable)
        {
            this.StartPosition = start;
            this.Width = end.X - start.X;
            this.Height = end.Y - start.Y;
            this.BorderColor = color;
            this.Border = border;
            this.IsMoveable = isMoveable;
        }

        /// <summary>
        /// Initializes a new instance of the Ellipse class.
        /// </summary>
        public Ellipse()
        {
        }

        /// <inheritdoc/>
        public override void Draw(Graphics graphics)
        {
            using (var m = new Matrix())
            {
                if (this.IsRotatable)
                {
                    m.RotateAt(
                        this.Rotation,
                        new PointF(
                            this.StartPosition.X + (this.Width / 2),
                            this.StartPosition.Y + (this.Height / 2)));
                    graphics.Transform = m;
                }

                var rect = new System.Drawing.Rectangle(
                    this.StartPosition.X,
                    this.StartPosition.Y,
                    this.Width,
                    this.Height);
                graphics.DrawEllipse(
                    new Pen(this.BorderColor, this.Border),
                    rect);
                graphics.FillEllipse(new SolidBrush(this.FillColor), rect);
                if (this.BorderShapes != null)
                {
                    this.BorderShapes.ToList().ForEach(x =>
                    {
                        x.Draw(graphics);
                        this.SetSelectionBorder();
                    });
                }

                graphics.ResetTransform();
            }
        }

        /// <inheritdoc/>
        public override double GetArea()
        {
            return this.Width * this.Height * Math.PI;
        }

        /// <inheritdoc/>
        public override bool PointIsInShape(Point point)
        {
            var center = this.GetCenterPoint();
            var xRad = this.Width / 2;
            var yRad = this.Height / 2;
            if (xRad <= 0.0 || yRad <= 0.0)
            {
                return false;
            }

            var normalized = new Point(point.X - center.X,
                                         point.Y - center.Y);
            return ((double)(normalized.X * normalized.X)
                     / (xRad * xRad)) + ((double)(normalized.Y * normalized.Y) / (yRad * yRad))
                <= 1.0;
        }
    }
}
