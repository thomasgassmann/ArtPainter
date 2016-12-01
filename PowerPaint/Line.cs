namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    /// <summary>
    /// Contains all members of a line.
    /// </summary>
    public class Line : Shape
    {
        /// <summary>
        /// Initializes a new instance of the Line class.
        /// </summary>
        /// <param name="start">The start position.</param>
        /// <param name="end">The end position.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="color">The color.</param>
        /// <param name="isMoveable">A value indicating whether the line is moveable.</param>
        public Line(Point start, Point end, int border, Color color, bool isMoveable)
        {
            this.Resize(start, end);
            this.Border = border;
            this.FillColor = color;
            this.IsMoveable = isMoveable;
        }

        /// <summary>
        /// Initializes a new instance of the Line class.
        /// </summary>
        public Line()
        {
        }

        /// <summary>
        /// Gets or sets the end position of the line.
        /// </summary>
        public Point EndPos { get; set; }

        /// <summary>
        /// Gets the end position.
        /// </summary>
        public override Point EndPosition
        {
            get
            {
                return this.EndPos;
            }
        }

        /// <inheritdoc />
        public override Point GetCenterPoint()
        {
            return new Point(
                this.EndPos.X - this.StartPosition.X,
                this.EndPos.Y - this.StartPosition.Y);
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
                        new PointF(this.StartPosition.X + (this.Width / 2), this.StartPosition.Y + (this.Height / 2)));
                    graphics.Transform = m;
                }

                graphics.DrawLine(
                    new Pen(this.FillColor, this.Border),
                    this.StartPosition,
                    this.EndPos);
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
        public override double GetArea()
        {
            var axisXDistance = Math.Abs(this.StartPosition.X - this.EndPos.X);
            var axisYDistance = Math.Abs(this.StartPosition.Y - this.EndPos.Y);
            return Math.Sqrt(Math.Pow(axisXDistance, 2) + Math.Pow(axisYDistance, 2));
        }

        /// <inheritdoc/>
        public override void SetSelectionBorder()
        {
            base.SetSelectionBorder();
            var newList = new List<Shape>(this.BorderShapes);
            foreach (var item in this.BorderShapes)
            {
                if (!this.PointIsInShape(item.StartPosition))
                {
                    newList.Remove(item);
                }
            }

            this.BorderShapes = newList;
        }

        /// <inheritdoc/>
        public override void Move(int diffX, int diffY)
        {
            if (this.IsMoveable)
            {
                this.StartPosition = new Point(
                        this.StartPosition.X + diffX,
                        this.StartPosition.Y + diffY);
                this.EndPos = new Point(
                        this.EndPos.X + diffX,
                        this.EndPos.Y + diffY);
            }
        }

        /// <inheritdoc />
        public override bool PointIsInShape(Point point)
        {
            var start = new Point(
                Math.Min(this.StartPosition.X, this.EndPos.X),
                Math.Min(this.StartPosition.Y, this.EndPos.Y));
            var end = new Point(
                Math.Max(this.StartPosition.X, this.EndPos.X),
                Math.Max(this.StartPosition.Y, this.EndPos.Y));
            var xaxisDifference = Math.Abs(point.X - this.StartPosition.X);
            var yaxisDistance = Math.Abs(point.Y - this.StartPosition.Y);
            var startDifference = Math.Sqrt(
                Math.Pow(xaxisDifference, 2) + Math.Pow(yaxisDistance, 2));
            xaxisDifference = Math.Abs(point.X - this.EndPos.X);
            yaxisDistance = Math.Abs(point.Y - this.EndPos.Y);
            var endDifference = Math.Sqrt(
                Math.Pow(xaxisDifference, 2) + Math.Pow(yaxisDistance, 2));
            xaxisDifference = Math.Abs(start.X - end.X);
            yaxisDistance = Math.Abs(start.Y - end.Y);
            var difference = Math.Sqrt(
                Math.Pow(xaxisDifference, 2) + Math.Pow(yaxisDistance, 2));
            if (Math.Round((endDifference + startDifference) / 10, 0) * 10 ==
                Math.Round(difference / 10, 0) * 10)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override void Resize(Point mouseDownPos, Point newPos)
        {
            this.StartPosition = mouseDownPos;
            this.EndPos = newPos;
        }
    }
}
