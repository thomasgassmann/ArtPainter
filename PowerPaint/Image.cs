namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml.Serialization;
    using System.Drawing;

    /// <summary>
    /// Contains all members of an image.
    /// </summary>
    public class Image : Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the Image class.
        /// </summary>
        public Image()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Image class.
        /// </summary>
        /// <param name="path">The path of the image.</param>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        public Image(string path, System.Drawing.Point start, System.Drawing.Point end)
        {
            this.ImagePath = path;
            this.StartPosition = start;
            this.Height = end.Y - start.Y;
            this.Width = end.X - start.X;
            this.BorderColor = Color.Black;
        }

        [XmlIgnore]
        public Bitmap CachedImage { get; set; }

        /// <summary>
        /// Gets or sets the path of the image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <inheritdoc/>
        public override void OpenDialog()
        {
            var result = MessageBox.Show("Möchten Sie die Datei auswählen oder den Editor öffnen? Ja = Editor, Nein = Datei", "?", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                var dia = new OpenFileDialog();
                if (dia.ShowDialog() == DialogResult.OK)
                {
                    this.ImagePath = dia.FileName;
                }
            }
            else
            {
                base.OpenDialog();
            }
        }

        /// <inheritdoc/>
        public override void Draw(Graphics graphics)
        {
            if (this.CachedImage == null && File.Exists(this.ImagePath))
            {
                this.CachedImage = new Bitmap(Bitmap.FromFile(this.ImagePath));
            }

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
                try
                {
                    graphics.DrawImage(
                        this.CachedImage,
                        rect);
                }
                catch
                {
                    graphics.DrawRectangle(
                        new Pen(this.BorderColor),
                        rect);
                }

                if (this.BorderShapes != null)
                {
                    this.BorderShapes.ToList().ForEach(x =>
                    {
                        x.Draw(graphics);
                        this.SetSelectionBorder();
                    });
                }

                graphics.ResetTransform();
            }
        }
    }
}
