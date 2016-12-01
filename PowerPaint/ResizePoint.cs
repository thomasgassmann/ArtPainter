namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Contains a value indicating the side of the item.
    /// </summary>
    public enum BorderShapePosition
    {
        /// <summary>
        /// The item to draw on the top on the left side.
        /// </summary>
        TopLeft,

        /// <summary>
        /// The item to draw on the top on the right side.
        /// </summary>
        TopRight,

        /// <summary>
        /// The item to draw at the bottom on the left side.
        /// </summary>
        BottomLeft,

        /// <summary>
        /// The item to draw at the bottom on the right side.
        /// </summary>
        BottomRight
    }

    /// <summary>
    /// Contains all members of a point which is used to resize other shapes.
    /// </summary>
    public class ResizePoint : Ellipse
    {
        /// <summary>
        /// The start position.
        /// </summary>
        private readonly Point startPos;

        /// <summary>
        /// The tag.
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the ResizePoint class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="index">The index.</param>
        public ResizePoint(Point start, Point end, Shape parent, int index)
        {
            this.Initialize(
                start,
                end,
                Color.Black,
                1,
                parent);
            this.index = index;
        }

        /// <summary>
        /// Initializes a new instance of the ResizePoint class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="position">The position.</param>
        public ResizePoint(Shape parent, BorderShapePosition position)
        {
            if (position == BorderShapePosition.BottomLeft)
            {
                this.startPos = new Point(
                    parent.EndPosition.X,
                    parent.StartPosition.Y);
                this.Initialize(
                    new Point(parent.StartPosition.X, parent.EndPosition.Y - 10),
                    new Point(parent.StartPosition.X + 10, parent.EndPosition.Y),
                    Color.Black,
                    1,
                    parent);
            }
            else if (position == BorderShapePosition.BottomRight)
            {
                this.startPos = parent.StartPosition;
                this.Initialize(
                    new Point(parent.EndPosition.X - 10, parent.EndPosition.Y - 10),
                    new Point(parent.EndPosition.X, parent.EndPosition.Y),
                    Color.Black,
                    1,
                    parent);
            }
            else if (position == BorderShapePosition.TopLeft)
            {
                this.startPos = parent.EndPosition;
                this.Initialize(
                    parent.StartPosition,
                    new Point(parent.StartPosition.X + 10, parent.StartPosition.Y + 10),
                    Color.Black,
                    1,
                    parent);
            }
            else if (position == BorderShapePosition.TopRight)
            {
                this.startPos = new Point(
                    parent.StartPosition.X,
                    parent.EndPosition.Y);
                this.Initialize(
                    new Point(parent.EndPosition.X - 10, parent.StartPosition.Y),
                    new Point(parent.EndPosition.X, parent.StartPosition.Y + 10),
                    Color.Black,
                    1,
                    parent);
            }
            else
            {
                throw new ArgumentException("position");
            }
        }

        /// <summary>
        /// Initializes a new instance of the ResizePoint class.
        /// </summary>
        public ResizePoint()
        {
            this.Initialize(
                new Point(0, 0),
                new Point(10, 10),
                Color.Black,
                3,
                new Rectangle());
        }

        /// <summary>
        /// Gets or sets the parent shape of the resize point.
        /// </summary>
        public Shape Parent { get; set; }

        /// <inheritdoc />
        public override void Resize(Point mouseDownPos, Point e)
        {
            if (this.Parent.IsResizable)
            {
                if (this.Parent.GetType() != typeof(Polygon))
                {
                    var point = ArtPainterHelper.RotatePoint(
                        e, 
                        this.startPos, 
                        360 - this.Parent.Rotation);
                    var newStart = ArtPainterHelper.RotatePoint(
                        this.startPos,
                        this.Parent.StartPosition,
                        360 - this.Parent.Rotation);
                    this.Parent.Resize(newStart, point);
                }
                else
                {
                    var polygon = (Polygon)this.Parent;
                    polygon.PolygonPoints[this.index] = new Point(e.X, e.Y);
                }
            }
        }

        /// <inheritdoc/>
        public override void Draw(Graphics graphics)
        {
            using (var m = new Matrix())
            {
                m.RotateAt(
                    this.Parent.Rotation,
                    new PointF(
                        this.Parent.StartPosition.X + (this.Parent.Width / 2),
                        this.Parent.StartPosition.Y + (this.Parent.Height / 2)));
                graphics.Transform = m;
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
        }

        /// <inheritdoc />
        public override void SetSelectionBorder()
        {
            return;
        }

        /// <summary>
        /// Initializes a new instance of the ResizePoint class.
        /// </summary>
        /// <param name="start">The start position.</param>
        /// <param name="end">The end position.</param>
        /// <param name="color">The color.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="parent">The parent shape.</param>
        private void Initialize(Point start, Point end, Color color, int border, Shape parent)
        {
            this.Parent = parent;
            this.StartPosition = start;
            this.Height = end.Y - start.Y;
            this.Width = end.X - start.X;
            this.FillColor = color;
            this.Border = border;
            this.IsMoveable = true;
        }
    }
}
