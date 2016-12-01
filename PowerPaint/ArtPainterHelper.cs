namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Contains all helper methods that are used in the ArtPainter.
    /// </summary>
    public static class ArtPainterHelper
    {
        /// <summary>
        /// Gets a dialog window to return an integer.
        /// </summary>
        /// <param name="startValue">The start value of the dialog.</param>
        /// <param name="min">The minimal return value.</param>
        /// <param name="max">The maximum return value.</param>
        /// <returns>Returns the value.</returns>
        public static int GetIntDialog(int startValue, int min, int max)
        {
            var frm = new Form();
            frm.Text = "Zahl eingeben";
            frm.ShowIcon = false;
            frm.ShowInTaskbar = false;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            var num = new NumericUpDown() 
            { 
                Minimum = min, 
                Maximum = max,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Value = startValue
            };

            var cancelButton = new Button()
            {
                Text = "Abbrechen",
                DialogResult = DialogResult.Cancel,
                Height = 20,
                Width = 100,
                Location = new Point(0, 20)
            };

            var doneBtn = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Height = 20,
                Width = 100,
                Location = new Point(110, 20)
            };

            frm.AcceptButton = doneBtn;
            frm.CancelButton = cancelButton;
            frm.Width = 350;
            frm.Height = 80;
            frm.Controls.Add(doneBtn);
            frm.Controls.Add(cancelButton);
            frm.Controls.Add(num);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                return (int)num.Value;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets a text with a dialog.
        /// </summary>
        /// <param name="title">The tile of the dialog.</param>
        /// <returns>Returns the value the user typed in.</returns>
        public static string GetText(string title)
        {
            var frm = new Form();
            frm.Text = title;
            frm.ShowIcon = false;
            var txt = new TextBox();
            frm.Controls.Add(txt);
            txt.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.Width = 250;
            frm.Height = 100;
            var btnOk = new Button();
            frm.AcceptButton = btnOk;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Text = "OK";
            btnOk.Location = new Point(0, 25);
            frm.Controls.Add(btnOk);
            var btnCancel = new Button();
            btnCancel.Text = "Abbrechen";
            btnCancel.Location = new Point(100, 25);
            btnCancel.DialogResult = DialogResult.Cancel;
            frm.CancelButton = btnCancel;
            frm.Controls.Add(btnCancel);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                return txt.Text;
            }

            return null;
        }
 
        /// <summary>
        /// Gets a color from the dialog.
        /// </summary>
        /// <returns>Returns the color.</returns>
        public static Color GetColor()
        {
            var dia = new ColorDialog();
            if (dia.ShowDialog() == DialogResult.OK)
            {
                return dia.Color;
            }

            return Color.Empty;
        }

        /// <summary>
        /// Rotates a point around another point.
        /// </summary>
        /// <param name="pointToRotate">The point to rotate.</param>
        /// <param name="centerPoint">The point to rotate the point around.</param>
        /// <param name="angleInDegrees">The angle in degrees.</param>
        /// <returns>Returns the new point.</returns>
        public static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            var angleInRadians = angleInDegrees * (2 * Math.PI / 360);
            var cosTheta = Math.Cos(angleInRadians);
            var sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    ((cosTheta * (pointToRotate.X - centerPoint.X)) -
                    (sinTheta * (pointToRotate.Y - centerPoint.Y)) + centerPoint.X),
                Y =
                    (int)
                    ((sinTheta * (pointToRotate.X - centerPoint.X)) +
                    ((cosTheta * (pointToRotate.Y - centerPoint.Y)) + centerPoint.Y))
            };
        }

        /// <summary>
        /// Calculates the angles between two points.
        /// </summary>
        /// <param name="mouseDownPosition">The first point.</param>
        /// <param name="mouseLocation">The second point.</param>
        /// <returns>Returns the angle.</returns>
        public static int CalculateAngle(Point mouseDownPosition, Point mouseLocation)
        {
            var val = (int)(Math.Atan2(mouseDownPosition.Y - mouseLocation.Y, mouseLocation.X - mouseDownPosition.X) * (180.0 / Math.PI));
            var newRotation = 360 - (180 - -val);
            if (newRotation < 90)
            {
                return 269 + newRotation;
            }
            else
            {
                return Math.Abs(newRotation - 90);
            }
        }
    }
}
