namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Contains all members of a triangle.
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the Triangle class.
        /// </summary>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        /// <param name="color">The border color.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="isMoveable">A value indicating whether the triangle is moveable.</param>
        public Triangle(Point start, Point end, Color color, int border, bool isMoveable)
        {
            this.StartPosition = start;
            this.Width = end.X - start.X;
            this.Height = end.Y - start.Y;
            this.BorderColor = color;
            this.Border = border;
            this.IsMoveable = isMoveable;
        }

        /// <summary>
        /// Initializes a new instance of the Triangle class.
        /// </summary>
        public Triangle()
        {
        }

        /// <summary>
        /// Gets the point A.
        /// </summary>
        private Point A
        {
            get
            {
                return new Point(this.StartPosition.X, this.EndPosition.Y);
            }
        }

        /// <summary>
        /// Gets the point B.
        /// </summary>
        private Point B
        {
            get
            {
                return this.EndPosition;
            }
        }

        /// <summary>
        /// Gets the point C.
        /// </summary>
        private Point C
        {
            get
            {
                return new Point(this.StartPosition.X + this.Width / 2, this.StartPosition.Y);
            }
        }

        /// <summary>
        /// Gets the points of the triangle.
        /// </summary>
        private Point[] Points
        {
            get
            {
                var arr = new Point[3];
                arr[0] = this.A;
                arr[1] = this.B;
                arr[2] = this.C;
                return arr;
            }
        }

        /// <inheritdoc/>
        public override void Resize(Point mouseDownPos, Point newPos)
        {
            this.StartPosition = new Point(
                Math.Min(newPos.X, mouseDownPos.X),
                Math.Min(newPos.Y, mouseDownPos.Y));
            var endPos = new Point(
                Math.Max(newPos.X, mouseDownPos.X),
                Math.Max(newPos.Y, mouseDownPos.Y));
            this.Height = endPos.Y - this.StartPosition.Y;
            this.Width = endPos.X - this.StartPosition.X;
        }

        /// <inheritdoc/>
        public override Point GetCenterPoint()
        {
            return new Point(
                this.StartPosition.X + this.Width / 2,
                this.StartPosition.Y + this.Height / 2);
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
                        this.GetCenterPoint());
                    graphics.Transform = m;
                }

                graphics.DrawPolygon(
                    new Pen(this.BorderColor, this.Border),
                    this.Points);
                graphics.FillPolygon(
                    new SolidBrush(this.FillColor),
                    this.Points);
                graphics.ResetTransform();
            }

            if (this.BorderShapes != null)
            {
                foreach (var shape in this.BorderShapes)
                {
                    shape.Draw(graphics);
                    this.SetSelectionBorder();
                }
            }
        }

        /// <inheritdoc/>
        public override bool PointIsInShape(Point point)
        {
            return new Polygon(
                Color.Black,
                0,
                Color.Black,
                false,
                this.A,
                this.B,
                this.C).PointIsInShape(point);
        }

        /// <inheritdoc/>
        public override double GetArea()
        {
            var line = this.B.X - this.A.X;
            var height = this.B.Y - this.C.Y;
            return line * height / 2;
        }
    }
}
