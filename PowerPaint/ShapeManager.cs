namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Manages all shapes.
    /// </summary>
    public class ShapeManager
    {
        /// <summary>
        /// Contains the current shape.
        /// </summary>
        private Shape current;

        /// <summary>
        /// Contains all shapes.
        /// </summary>
        private List<Shape> shapeList = new List<Shape>();

        /// <summary>
        /// Initializes a new instance of the ShapeManager class.
        /// </summary>
        public ShapeManager()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ShapeManager class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ShapeManager(string name)
        {
            this.Name = name;
            this.FormBackGroundColor = new CustomColor(Color.White);
        }

        /// <summary>
        /// Gets or sets the back color.
        /// </summary>
        public CustomColor FormBackGroundColor { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current shape.
        /// </summary>
        public Shape CurrentShape
        {
            get
            {
                return this.current;
            }

            set
            {
                if (this.current != null)
                {
                    this.current.RemoveSelectionBorder();
                }

                this.current = value;
                if (this.current != null)
                {
                    this.current.SetSelectionBorder();
                }
            }
        }

        /// <summary>
        /// Gets all shapes.
        /// </summary>
        public IEnumerable<Shape> Shapes
        {
            get
            {
                return this.shapeList;
            }
        }

        /// <summary>
        /// Gets a new shape.
        /// </summary>
        /// <param name="constantsType">The name of the shape found in the constants class.</param>
        /// <param name="start">The start position.</param>
        /// <param name="end">The end position.</param>
        /// <returns>Returns the shape.</returns>
        public Shape GetNewShape(string constantsType, Point start, Point end)
        {
            Shape returnShape;
            if (constantsType == ArtPainterConstants.RectangleDisplayName)
            {
                returnShape = new Rectangle(start, end, Color.Black, 1, true);
            }
            else if (constantsType == ArtPainterConstants.SquareDisplayName)
            {
                returnShape = new Square(start, end, Color.Black, 1, true);
            }
            else if (constantsType == ArtPainterConstants.LineDisplayName)
            {
                returnShape = new Line(start, end, 3, Color.Black, true);
            }
            else if (constantsType == ArtPainterConstants.EllipseDisplayName)
            {
                returnShape = new Ellipse(start, end, 3, Color.Black, true);
            }
            else if (constantsType == ArtPainterConstants.CircleDisplayName)
            {
                returnShape = new Circle(start, end, Color.Black, 3, true);
            }
            else if (constantsType == ArtPainterConstants.TriangleDisplayName)
            {
                returnShape = new Triangle(start, end, Color.Black, 3, true);
            }
            else if (constantsType == ArtPainterConstants.PolygonDisplayName)
            {
                returnShape = new Polygon(Color.Empty, 3, Color.Black, true);
            }
            else if (constantsType == ArtPainterConstants.MousePaintingDisplayName)
            {
                returnShape = new MousePaint(start, Color.Black);
            }
            else if (constantsType == ArtPainterConstants.ImageDisplayName)
            {
                returnShape = new Image(string.Empty, start, end);
            }
            else
            {
                throw new ArgumentException();
            }

            if (this.Shapes.Count() > 0)
            {
                returnShape.Layer = this.Shapes.Max(x => x.Layer) + 1;
            }
            else
            {
                returnShape.Layer = 0;
            }

            return returnShape;
        }

        /// <summary>
        /// Adds a new shape to the current list.
        /// </summary>
        /// <param name="constantName">The name of the shape in the constant.</param>
        /// <param name="mouse">The mouse position.</param>
        public void AddNewShape(string constantName, Point mouse)
        {
            var shape = this.GetNewShape(
                constantName,
                mouse,
                new Point(mouse.X + 3, mouse.Y + 3));
            this.AddShape(shape);
            this.CurrentShape = shape;
        }

        /// <summary>
        /// Gets a shape at a position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>The shape.</returns>
        public Shape GetShapeAt(Point pos)
        {
            return this.Shapes
                .Where(x => x.PointIsInShape(
                    ArtPainterHelper.RotatePoint(pos, x.GetCenterPoint(), (360 - x.Rotation))))
                    .Union(
                    this.Shapes
                .Where(shape => shape.BorderShapes != null)
                .SelectMany(shape => shape.BorderShapes)
                .Where(borderShape => borderShape.PointIsInShape(
                    ArtPainterHelper.RotatePoint(
                        pos,
                        ((ResizePoint)borderShape).Parent.GetCenterPoint(),
                        (360 - ((ResizePoint)borderShape).Parent.Rotation)))))
                .OrderByDescending(shape => shape.GetType() == typeof(ResizePoint) || shape.GetType() == typeof(RotationPoint))
                .ThenBy(x => x.Layer)
                .FirstOrDefault();
        }

        /// <summary>
        /// Moves or rotates the current shape depending on the mouse positions.
        /// </summary>
        /// <param name="mouseDownPosition">The mouse down position.</param>
        /// <param name="mouseLocation">The current mouse location.</param>
        public void DoMouseAction(ref Point mouseDownPosition, Point mouseLocation)
        {
            if (this.CurrentShape.GetType() == typeof(ResizePoint))
            {
                var resizePoint = (ResizePoint)this.CurrentShape;
                resizePoint.Resize(
                    mouseDownPosition,
                    mouseLocation);
                resizePoint.Parent.SetSelectionBorder();
            }
            else if (this.CurrentShape.GetType() == typeof(RotationPoint))
            {
                var point = (RotationPoint)this.CurrentShape;
                point.Parent.Rotation = ArtPainterHelper.CalculateAngle(point.Parent.GetCenterPoint(), mouseLocation);
                point.Parent.SetSelectionBorder();
            }
            else
            {
                this.CurrentShape.Move(
                    -(mouseDownPosition.X - mouseLocation.X),
                    -(mouseDownPosition.Y - mouseLocation.Y));
                mouseDownPosition = new Point(mouseLocation.X, mouseLocation.Y);
            }
        }

        /// <summary>
        /// Clears the current drawing.
        /// </summary>
        public void Clear()
        {
            this.shapeList = new List<Shape>();
        }

        /// <summary>
        /// Adds a new shape.
        /// </summary>
        /// <param name="shape">The shape to add.</param>
        public void AddShape(Shape shape)
        {
            this.shapeList.Add(shape);
            this.SortList();
        }

        /// <summary>
        /// Adds shapes to the current shape list of the shape manager.
        /// </summary>
        /// <param name="shapes">The shapes to add.</param>
        public void AddShapes(IEnumerable<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                this.AddShape(shape);
            }
        }

        /// <summary>
        /// Removes a shape.
        /// </summary>
        /// <param name="shape">The shape to remove.</param>
        public void RemoveShape(Shape shape)
        {
            this.shapeList.Remove(shape);
            this.SortList();
        }

        /// <summary>
        /// Brings a shape to the front.
        /// </summary>
        /// <param name="shape">The shape to bring to the front.</param>
        public void BringToFront(Shape shape)
        {
            var shapeTemp = shape;
            this.RemoveShape(shape);
            this.AddShape(shapeTemp);
        }

        /// <summary>
        /// Brings a shape to the back.
        /// </summary>
        /// <param name="shape">The shape to bring to the back.</param>
        public void BringToBack(Shape shape)
        {
            var tempShape = shape;
            this.RemoveShape(shape);
            this.shapeList.Insert(0, tempShape);
        }

        /// <summary>
        /// Sets all colors of all shapes to an empty color.
        /// </summary>
        public void ResetColors()
        {
            foreach (var shape in this.Shapes)
            {
                shape.FillColor = Color.Empty;
            }
        }

        /// <summary>
        /// Sets all rotations of all shapes to 0 degrees.
        /// </summary>
        public void ResetRotations()
        {
            this.Shapes.Select(x => x.Rotation = 0).ToList();
        }

        /// <summary>
        /// Sorts the list.
        /// </summary>
        private void SortList()
        {
            this.shapeList = this.shapeList.OrderBy(x => x.Layer).ToList();
        }
    }
}