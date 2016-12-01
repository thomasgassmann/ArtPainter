namespace ArtPainter
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Contains all members of a RotationPoint.
    /// </summary>
    public class RotationPoint : ResizePoint
    {
        /// <summary>
        /// Initializes a new instance of the RotationPoint class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public RotationPoint(Shape parent)
        {
            this.Initialize(
                new Point(parent.StartPosition.X + parent.Width / 2 - 6, parent.StartPosition.Y - 48),
                new Point(parent.StartPosition.X + parent.Width / 2 + 6, parent.StartPosition.Y - 36),
                Color.Black,
                1,
                parent);
        }

        /// <summary>
        /// Initializes a new instance of the RotationPoint class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="parent">The parent.</param>
        public RotationPoint(Point start, Point end, Shape parent)
        {
            this.Initialize(
                start,
                end,
                Color.Black,
                1,
                parent);
        }

        /// <summary>
        /// Initializes the current rotation point.
        /// </summary>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        /// <param name="color">The fill color.</param>
        /// <param name="border">The border thickness.</param>
        /// <param name="parent">The parent.</param>
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
