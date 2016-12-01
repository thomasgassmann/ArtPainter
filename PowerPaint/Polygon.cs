namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    /// <summary>
    /// Contains all members of a polygon.
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        /// Initializes a new instance of the Polygon class.
        /// </summary>
        public Polygon()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Polygon class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="border">The border.</param>
        /// <param name="borderColor">The border color.</param>
        /// <param name="isMoveable">A value indicating whether the polygon is moveable.</param>
        /// <param name="points">The points.</param>
        public Polygon(Color color, int border, Color borderColor, bool isMoveable, params Point[] points)
        {
            this.PolygonPoints = points.ToList();
            this.FillColor = color;
            this.Border = border;
            this.BorderColor = borderColor;
            this.IsMoveable = this.IsMoveable;
        }

        /// <summary>
        /// Gets or sets the points of the polygon.
        /// </summary>
        public List<Point> PolygonPoints { get; set; }

        /// <inheritdoc/>
        public override void Resize(Point mouseDownPos, Point newPos)
        {
            return;
        }

        /// <inheritdoc/>
        public override void Draw(Graphics graphics)
        {
            using (var m = new Matrix())
            {
                try
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
                        this.PolygonPoints.ToArray());
                    graphics.FillPolygon(new SolidBrush(this.FillColor), this.PolygonPoints.ToArray());
                    graphics.ResetTransform();
                    this.InitializeIfNecessary();
                    this.BorderShapes.Clear();
                    this.SetSelectionBorder();
                    this.BorderShapes.ToList().ForEach(x => x.Draw(graphics));
                }
                catch (ArgumentException)
                {
                }
            }
        }

        /// <inheritdoc/>
        public override void SetSelectionBorder()
        {
            this.InitializeIfNecessary();
            this.PolygonPoints.ForEach(point =>
            {
                this.BorderShapes.Add(new ResizePoint(
                    new Point(point.X, point.Y - 10),
                    new Point(point.X + 10, point.Y),
                    this,
                    this.PolygonPoints.IndexOf(point)));
            });
        }

        /// <inheritdoc/>
        public override void RemoveSelectionBorder()
        {
            this.BorderShapes.Clear();
        }

        /// <inheritdoc/>
        public override bool PointIsInShape(Point point)
        {
            var isInside = false;
            for (int i = 0, j = this.PolygonPoints.Count - 1; i < this.PolygonPoints.Count; j = i++)
            {
                if (((this.PolygonPoints[i].Y > point.Y) != (this.PolygonPoints[j].Y > point.Y)) &&
                (point.X < (this.PolygonPoints[j].X - this.PolygonPoints[i].X) * (point.Y - this.PolygonPoints[i].Y) / (this.PolygonPoints[j].Y - this.PolygonPoints[i].Y) + this.PolygonPoints[i].X))
                {
                    isInside = !isInside;
                }
            }

            return isInside;
        }

        /// <inheritdoc/>
        public override void Move(int diffX, int diffY)
        {
            if (this.IsMoveable)
            {
                for (var i = 0; i < this.PolygonPoints.Count; i++)
                {
                    this.PolygonPoints[i] = new Point(
                        this.PolygonPoints[i].X + diffX,
                        this.PolygonPoints[i].Y + diffY);
                }
            }
        }

        /// <inheritdoc/>
        public override double GetArea()
        {
            var list = new List<int>();
            var polygons = new List<Point>();
            this.PolygonPoints.ForEach(x => polygons.Add(x));
            polygons.Add(this.PolygonPoints[0]);
            for (var i = 0; i < polygons.Count - 1; i++)
            {
                var first = polygons[i].X * polygons[i + 1].Y;
                var second = polygons[i + 1].X * polygons[i].Y;
                var result = first - second;
                list.Add(result);
            }

            return Math.Abs(list.Sum() / 2);
        }

        /// <inheritdoc/>
        public override Point GetCenterPoint()
        {
            var sum = 0;
            var x = 0.0;
            var y = 0.0;
            this.PolygonPoints.ForEach(point => { sum += 1; x += point.X; y += point.Y; });
            return new Point((int)(x / sum), (int)(y / sum));
        }

        /// <summary>
        /// Initializes the border shapes if necessary.
        /// </summary>
        private void InitializeIfNecessary()
        {
            if (this.BorderShapes == null)
            {
                this.BorderShapes = new List<Shape>();
            }
        }
    }
}
