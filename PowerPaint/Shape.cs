namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Serialization;

    /// <summary>
    /// This class defines all members of a shape to draw on the form.
    /// </summary>
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Ellipse))]
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Square))]
    [XmlInclude(typeof(Triangle))]
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Polygon))]
    [XmlInclude(typeof(MousePaint))]
    [XmlInclude(typeof(Image))]
    public abstract class Shape
    {
        /// <summary>
        /// Contains a value indicating whether the shape is resizable.
        /// </summary>
        private bool isResizable = true;

        /// <summary>
        /// Contains a value indicating whether the shape is rotatable.
        /// </summary>
        private bool isRotatable = true;

        /// <summary>
        /// Contains a value indicating whether the shape is moveable.
        /// </summary>
        private bool isMoveable = true;

        /// <summary>
        /// Contains the rotation in degrees.
        /// </summary>
        private int rotation;

        /// <summary>
        /// Gets or sets the top left start position of the shape.
        /// </summary>
        public virtual Point StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the border color of the shape.
        /// </summary>
        [XmlIgnore]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the shape to serialize it.
        /// </summary>
        [XmlElement(ElementName = "BorderColor")]
        public CustomColor BorderColorSerializable
        {
            get
            {
                return new CustomColor(this.BorderColor);
            }

            set
            {
                this.BorderColor = Color.FromArgb(
                    value.Alpha,
                    value.Red,
                    value.Green,
                    value.Blue);
            }
        }

        /// <summary>
        /// Gets or sets the color of the shape.
        /// </summary>
        [XmlIgnore]
        public Color FillColor { get; set; }

        /// <summary>
        /// Gets or sets the fill color of the shape to serialize it.
        /// </summary>
        [XmlElement(ElementName = "FillColor")]
        public CustomColor FillColorSerializable
        {
            get
            {
                return new CustomColor(this.FillColor);
            }

            set
            {
                this.FillColor = Color.FromArgb(value.Alpha, value.Red, value.Green, value.Blue);
            }
        }

        /// <summary>
        /// Gets or sets the layer of the shape.
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        public int Border { get; set; }

        /// <summary>
        /// Gets or sets the width of the shape.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the shape is rotatable.
        /// </summary>
        public bool IsRotatable
        {
            get
            {
                return this.isRotatable;
            }

            set
            {
                this.isRotatable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the shape is resizable.
        /// </summary>
        public bool IsResizable
        {
            get 
            {
                return this.isResizable;
            }

            set
            { 
                this.isResizable = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the shape.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the shape is moveable or not.
        /// </summary>
        public bool IsMoveable
        {
            get
            {
                return this.isMoveable;
            }

            set
            {
                this.isMoveable = value;
            }
        }

        /// <summary>
        /// Gets the down right end position of the shape, if there is any.
        /// </summary>
        public virtual Point EndPosition
        {
            get
            {
                return new Point(
                    this.StartPosition.X + this.Width,
                    this.StartPosition.Y + this.Height);
            }
        }

        /// <summary>
        /// Gets or sets the rotation in degrees of the shape.
        /// </summary>
        public int Rotation
        {
            get
            {
                return this.rotation;
            }

            set
            {
                if (value <= 360 && value >= 0)
                {
                    this.rotation = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// Gets or sets all shapes to draw the border.
        /// </summary>
        [XmlIgnore]
        public List<Shape> BorderShapes { get; set; }

        /// <summary>
        /// Resizes the shape from the mouse down position to the new mouse position.
        /// </summary>
        /// <param name="mouseDownPos">The mouse down position.</param>
        /// <param name="newPos">The new mouse position.</param>
        public abstract void Resize(Point mouseDownPos, Point newPos);

        /// <summary>
        /// Opens the dialog.
        /// </summary>
        public virtual void OpenDialog()
        {
            var dia = new ShapeEditor(this);
            dia.ShowDialog();
        }

        /// <summary>
        /// Removes the selection border.
        /// </summary>
        public virtual void RemoveSelectionBorder()
        {
            if (this.BorderShapes != null)
            {
                this.BorderShapes.Clear();
            }
        }

        /// <summary>
        /// Moves the shape.
        /// </summary>
        /// <param name="diffX">The difference on the x axis.</param>
        /// <param name="diffY">The difference of the y axis.</param>
        public virtual void Move(int diffX, int diffY)
        {
            if (this.IsMoveable)
            {
                this.StartPosition = new Point(
                                    this.StartPosition.X + diffX,
                                    this.StartPosition.Y + diffY);
            }
        }

        /// <summary>
        /// Returns the center point of the shape.
        /// </summary>
        /// <returns>Returns the center point.</returns>
        public abstract Point GetCenterPoint();

        /// <summary>
        /// Draws the shape on the form.
        /// </summary>
        /// <param name="graphics">The graphics to draw the shape.</param>
        public abstract void Draw(Graphics graphics);

        /// <summary>
        /// Fills the BorderShapes property with the shapes to draw the border.
        /// </summary>
        public virtual void SetSelectionBorder()
        {
            this.BorderShapes = new List<Shape>();
            this.BorderShapes.Add(new ResizePoint(this, BorderShapePosition.TopLeft));
            this.BorderShapes.Add(new ResizePoint(this, BorderShapePosition.TopRight));
            this.BorderShapes.Add(new ResizePoint(this, BorderShapePosition.BottomLeft));
            this.BorderShapes.Add(new ResizePoint(this, BorderShapePosition.BottomRight));
            this.SetRotationPoint();
        }

        /// <summary>
        /// Sets the rotation point.
        /// </summary>
        public virtual void SetRotationPoint()
        {
            this.BorderShapes.Add(new RotationPoint(this));
        }

        /// <summary>
        /// Returns a value indicating whether a point is in a shape or not.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>Returns the result.</returns>
        public abstract bool PointIsInShape(Point point);

        /// <summary>
        /// Returns the area of the shape.
        /// </summary>
        /// <returns>Returns the area.</returns>
        public abstract double GetArea();
    }
}