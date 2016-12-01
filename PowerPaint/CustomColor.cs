namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains the custom color to serialize.
    /// </summary>
    public class CustomColor
    {
        /// <summary>
        /// Initializes a new instance of the CustomColor class.
        /// </summary>
        public CustomColor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CustomColor class.
        /// </summary>
        /// <param name="color">The color to create.</param>
        public CustomColor(Color color)
        {
            this.Alpha = color.A;
            this.Red = color.R;
            this.Green = color.G;
            this.Blue = color.B;
        }

        /// <summary>
        /// Gets the real color.
        /// </summary>
        public Color Color
        {
            get
            {
                return Color.FromArgb(this.Alpha, this.Red, this.Green, this.Blue);
            }
        }

        /// <summary>
        /// Gets or sets the alpha value.
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        /// Gets or sets the red value.
        /// </summary>
        public byte Red { get; set; }

        /// <summary>
        /// Gets or sets the green value.
        /// </summary>
        public byte Green { get; set; }

        /// <summary>
        /// Gets or sets the blue value.
        /// </summary>
        public byte Blue { get; set; }
    }
}
