namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Net;
    using System.IO;

    /// <summary>
    /// Contains all members of the ShapeEditor.
    /// </summary>
    public partial class ShapeEditor : Form
    {
        /// <summary>
        /// Initializes a new instance of the ShapeEditor class.
        /// </summary>
        /// <param name="shape">The shape to edit.</param>
        public ShapeEditor(Shape shape)
        {
            this.InitializeComponent();
            this.ShapeToEdit = shape;
            this.Text = string.Concat(
                "Shape-NumericEditor - ",
                this.ShapeToEdit.GetType().Name);
            this.panelBorderColor.BackColor = shape.BorderColor;
            this.panelFillColor.BackColor = shape.FillColor;
            this.cbMoveable.Checked = shape.IsMoveable;
            this.cbRotateable.Checked = shape.IsRotatable;
            this.cbResizeable.Checked = shape.IsResizable;
            this.numericRotation.Value = shape.Rotation;
            this.tbArea.Text = Math.Round(shape.GetArea(), 2).ToString();
            this.tbCenterPoint.Text = shape.GetCenterPoint().ToString();
            this.pointPanel.Hide();
            if (shape.GetType() == typeof(MousePaint))
            {
                this.numericRotation.Enabled = false;
                this.btnChangeBorderColor.Enabled = false;
                this.pointPanel.Show();
            }
            else if (shape.GetType() == typeof(Polygon))
            {
                this.pointPanel.Show();
            }

            if (shape.GetType() == typeof(Image))
            {
                this.btRandomize.Enabled = true;
                this.btAddImage.Enabled = true;
            }
            else
            {
                this.btRandomize.Enabled = false;
                this.btAddImage.Enabled = false;
            }

            this.LoadList();
        }

        /// <summary>
        /// Gets or sets the shape to edit.
        /// </summary>
        public Shape ShapeToEdit { get; set; }

        /// <summary>
        /// Loads the list.
        /// </summary>
        private void LoadList()
        {
            if (this.ShapeToEdit.GetType() == typeof(Polygon) ||
                this.ShapeToEdit.GetType() == typeof(MousePaint))
            {
                var points = (Polygon)this.ShapeToEdit;
                var y = 10;
                foreach (var point in points.PolygonPoints)
                {
                    var lb = new Label();
                    lb.Text = string.Concat(
                        "Punkt ",
                        points.PolygonPoints.IndexOf(point) + 1);
                    lb.Location = new Point(10, y);
                    var xLb = new NumericUpDown();
                    xLb.Maximum = 100000000000;
                    xLb.Minimum = -10000000000;
                    xLb.Value = point.X;
                    xLb.Location = new Point(10, y + 15);
                    xLb.Tag = new NumericEditor(
                        points.PolygonPoints.IndexOf(point),
                        NumericEditor.Type.X);
                    xLb.ValueChanged += this.Numeric_ValueChanged;
                    var yLb = new NumericUpDown();
                    yLb.Maximum = 100000000000;
                    yLb.Minimum = -10000000000;
                    yLb.Value = point.Y;
                    yLb.Location = new Point(140, y + 15);
                    y += 50;
                    yLb.Tag = new NumericEditor(
                        points.PolygonPoints.IndexOf(point),
                        NumericEditor.Type.Y);
                    yLb.ValueChanged += this.Numeric_ValueChanged;
                    this.pointPanel.Controls.Add(xLb);
                    this.pointPanel.Controls.Add(lb);
                    this.pointPanel.Controls.Add(yLb);
                }
            }
        }

        /// <summary>
        /// Changes the point value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Numeric_ValueChanged(object sender, EventArgs e)
        {
            var numeric = (NumericUpDown)sender;
            var tag = (NumericEditor)numeric.Tag;
            if (this.ShapeToEdit.GetType() == typeof(Polygon) ||
                this.ShapeToEdit.GetType() == typeof(MousePaint))
            {
                var polygon = (Polygon)this.ShapeToEdit;
                var current = polygon.PolygonPoints[tag.Index];
                if (tag.TypeXY == NumericEditor.Type.X)
                {
                    polygon.PolygonPoints[tag.Index] = new Point(
                        (int)numeric.Value,
                        current.Y);
                }
                else if (tag.TypeXY == NumericEditor.Type.Y)
                {
                    polygon.PolygonPoints[tag.Index] = new Point(
                        current.X,
                        (int)numeric.Value);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// Changes the border color.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ChangeBorderColor_Click(object sender, EventArgs e)
        {
            this.ShapeToEdit.BorderColor = ArtPainterHelper.GetColor();
            this.panelBorderColor.BackColor = this.ShapeToEdit.BorderColor;
        }

        /// <summary>
        /// Changes the border color.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ChangeFillColor_Click(object sender, EventArgs e)
        {
            this.ShapeToEdit.FillColor = ArtPainterHelper.GetColor();
            this.panelFillColor.BackColor = this.ShapeToEdit.FillColor;
        }

        /// <summary>
        /// Changes moveable state.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Moveable_CheckedChanged(object sender, EventArgs e)
        {
            this.ShapeToEdit.IsMoveable = this.cbMoveable.Checked;
        }

        /// <summary>
        /// Changes the rotation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NumericRotation_ValueChanged(object sender, EventArgs e)
        {
            this.ShapeToEdit.Rotation = int.Parse(this.numericRotation.Value.ToString());
        }

        /// <summary>
        /// Sets a value indicating whether the shape is rotatable.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Rotateable_CheckedChanged(object sender, EventArgs e)
        {
            this.ShapeToEdit.IsRotatable = this.cbRotateable.Checked;
        }

        /// <summary>
        /// Sets a value indicating whether the shape is resizable.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Resizeable_CheckedChanged(object sender, EventArgs e)
        {
            this.ShapeToEdit.IsResizable = this.cbResizeable.Checked;
        }

        /// <summary>
        /// Contains all members to edit the tag of the numeric up down.
        /// </summary>
        private class NumericEditor
        {
            /// <summary>
            /// Initializes a new instance of the NumericEditor class.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <param name="type">The type.</param>
            public NumericEditor(int index, Type type)
            {
                this.Index = index;
                this.TypeXY = type;
            }

            /// <summary>
            /// The X or Y axis.
            /// </summary>
            public enum Type
            {
                /// <summary>
                /// The X axis.
                /// </summary>
                X,

                /// <summary>
                /// The Y axis.
                /// </summary>
                Y
            }

            /// <summary>
            /// Gets or sets the index.
            /// </summary>
            public int Index { get; set; }

            /// <summary>
            /// Gets or sets the axis.
            /// </summary>
            public Type TypeXY { get; set; }
        }

        private void Randomize_Click(object sender, EventArgs e)
        {
            var image = (Image)this.ShapeToEdit;
            var bitmap = image.CachedImage;
            this.AddThingsToBitMap(bitmap, new Bitmap(this.GetImage(bitmap.Width, bitmap.Height)));
        }

        private void AddThingsToBitMap(Bitmap bmp, Bitmap two)
        {
            var img = two;
            var rnd = new Random();
            try
            {
                for (var i = 0; i < img.Width; i++)
                {
                    for (var j = 0; j < img.Height; j++)
                    {
                        if (rnd.Next(0, 4) == 2)
                        {
                            bmp.SetPixel(i, j, img.GetPixel(i, j));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private System.Drawing.Image GetImage(int width, int height)
        {
            var exception = false;
            do
            {
                try
                {
                    exception = false;
                    var rnd = new Random();
                    var url = string.Format(
                        "https://unsplash.it/{0}/{1}/?random",
                        width,
                        height);
                    using (var client = new WebClient())
                    {
                        var path = Path.GetTempFileName();
                        client.DownloadFile(url, path);
                        return System.Drawing.Image.FromFile(path);
                    }
                }
                catch (WebException)
                {
                    exception = true;
                }
            } while (exception);
            return null;
        }

        private void AddImage_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            if (dia.ShowDialog() == DialogResult.OK)
            {
                var file = Bitmap.FromFile(dia.FileName);
                var image = (Image)this.ShapeToEdit;
                this.AddThingsToBitMap(image.CachedImage, new Bitmap(file));
            }
        }
    }
}
