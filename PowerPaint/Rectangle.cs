namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Contains all members of a rectangle.
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the Rectangle class.
        /// </summary>
        public Rectangle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Rectangle class with parameters.
        /// </summary>
        /// <param name="start">The top left start position.</param>
        /// <param name="end">The down right end position.</param>
        /// <param name="color">The color.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="isMoveable">A value indicating whether the rectangle is moveable.</param>
        public Rectangle(Point start, Point end, Color color, int border, bool isMoveable)
        {
            this.StartPosition = start;
            this.Width = end.X - start.X;
            this.Height = end.Y - start.Y;
            this.BorderColor = color;
            this.Border = border;
            this.IsMoveable = isMoveable;
        }

        /// <inheritdoc />
        public override bool PointIsInShape(Point point)
        {
            if (point.X > this.StartPosition.X &&
                    point.X < (this.StartPosition.X + this.Width) &&
                    point.Y > this.StartPosition.Y &&
                    point.Y < (this.StartPosition.Y + this.Height))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override void Resize(Point mouseDownPos, Point e)
        {
            this.StartPosition = new Point(
                Math.Min(e.X, mouseDownPos.X),
                Math.Min(e.Y, mouseDownPos.Y));
            var endPos = new Point(
                Math.Max(e.X, mouseDownPos.X),
                Math.Max(e.Y, mouseDownPos.Y));
            this.Height = endPos.Y - this.StartPosition.Y;
            this.Width = endPos.X - this.StartPosition.X;
        }

        /// <inheritdoc />
        public override Point GetCenterPoint()
        {
            var x = (this.Width / 2) + this.StartPosition.X;
            var y = (this.Height / 2) + this.StartPosition.Y;
            return new Point(x, y);
        }

        /// <inheritdoc />
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
                graphics.DrawRectangle(
                    new Pen(this.BorderColor, this.Border),
                    rect);
                graphics.FillRectangle(new SolidBrush(this.FillColor), rect);
                if (this.BorderShapes != null)
                {
                    foreach (var shape in this.BorderShapes)
                    {
                        shape.Draw(graphics);
                        this.SetSelectionBorder();
                    }
                }

                graphics.ResetTransform();
            }
        }

        /// <inheritdoc/>
        public override double GetArea()
        {
            return this.Height * this.Width;
        }
    }
}
