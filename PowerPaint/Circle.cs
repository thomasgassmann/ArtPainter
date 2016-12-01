namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    /// <summary>
    /// Contains all members of a circle.
    /// </summary>
    public class Circle : Square
    {
        /// <summary>
        /// Initializes a new instance of the Circle class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="color">The color.</param>
        /// <param name="border">The border.</param>
        /// <param name="isMoveable">A value indicating whether the shape is moveable.</param>
        public Circle(Point start, Point end, Color color, int border, bool isMoveable)
            : base(start, end, color, border, isMoveable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Circle class.
        /// </summary>
        public Circle()
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
                graphics.ResetTransform();
            }

            if (this.BorderShapes != null)
            {
                this.BorderShapes.ToList().ForEach(x =>
                {
                    x.Draw(graphics);
                    this.SetSelectionBorder();
                });
            }
        }

        /// <inheritdoc/>
        public override double GetArea()
        {
            return Math.Pow(this.Width / 2, 2) * Math.PI;
        }
    }
}
