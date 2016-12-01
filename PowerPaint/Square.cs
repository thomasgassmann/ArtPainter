namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Contains all members of a square.
    /// </summary>
    public class Square : Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the Square class.
        /// </summary>
        public Square()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Square class with parameters.
        /// </summary>
        /// <param name="start">The top left start position.</param>
        /// <param name="end">The down right end position.</param>
        /// <param name="color">The border color.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="isMoveable">A value indicating whether the shape is moveable.</param>
        public Square(Point start, Point end, Color color, int border, bool isMoveable)
            : base(start, end, color, border, isMoveable)
        {
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
            if (e.X > mouseDownPos.X)
            {
                this.Height = endPos.Y - this.StartPosition.Y;
                this.Width = this.Height;
            }
            else
            {
                if (e.Y > mouseDownPos.Y)
                {
                    this.StartPosition = new Point(e.X, mouseDownPos.Y);
                    this.Width = mouseDownPos.X - e.X;
                    this.Height = this.Width;
                }
                else
                {
                    this.Height = endPos.Y - this.StartPosition.Y;
                    this.Width = this.Height;
                    this.StartPosition = new Point(
                        endPos.X - this.Width,
                        endPos.Y - this.Height);
                }
            }
        }
    }
}