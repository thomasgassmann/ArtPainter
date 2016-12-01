namespace ArtPainter
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// Contains the main window.
    /// </summary>
    public partial class ArtPainter : Form
    {
        /// <summary>
        /// Contains the current ShapeManager.
        /// </summary>
        private ShapeManager currentManager = new ShapeManager();

        /// <summary>
        /// Contains all ShapeManagers.
        /// </summary>
        private ShapeManagerCollection managers = new ShapeManagerCollection();

        /// <summary>
        /// Contains the last mouse position.
        /// </summary>
        private Point mouseDownPosition;

        /// <summary>
        /// Contains a value indicating whether the user is drawing a polygon.
        /// </summary>
        private bool isDrawingPolygon;

        /// <summary>
        /// Contains a value indicating whether the user is drawing with the mouse.
        /// </summary>
        private bool isDrawingWithMouse;

        /// <summary>
        /// Initializes a new instance of the ArtPainter class.
        /// </summary>
        public ArtPainter()
        {
            this.InitializeComponent();
            this.listShapes.Items.Add(ArtPainterConstants.RectangleDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.SquareDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.LineDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.EllipseDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.CircleDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.TriangleDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.PolygonDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.MousePaintingDisplayName);
            this.listShapes.Items.Add(ArtPainterConstants.ImageDisplayName);
            this.LoadManagers();
            typeof(Panel).InvokeMember("DoubleBuffered",
               BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
               null, this.graphicsPanel, new object[] { true });
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        /// <summary>
        /// Shows the context menu if the user right clicked on a shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void GraphicsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.currentManager != null && this.currentManager.CurrentShape != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.contextMenuStripShape.Show(this.graphicsPanel, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (this.isDrawingPolygon)
                    {
                        var polygon = (Polygon)this.currentManager.CurrentShape;
                        polygon.PolygonPoints.Add(e.Location);
                        this.graphicsPanel.Refresh();
                    }
                }
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.panelContext.Show(this.graphicsPanel, e.Location);
                }
            }
        }

        /// <summary>
        /// Creates a new shape depending on which item is selected in the toolbox.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void GraphicsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.currentManager != null && !this.isDrawingPolygon)
            {
                this.currentManager.CurrentShape = this.currentManager.GetShapeAt(e.Location);
                this.mouseDownPosition = e.Location;
                if (this.listShapes.SelectedItem != null && e.Button == MouseButtons.Left)
                {
                    if (this.listShapes.SelectedItem.ToString() != ArtPainterConstants.MouseDisplayName)
                    {
                        this.currentManager.AddNewShape(this.listShapes.SelectedItem.ToString(), e.Location);
                        this.isDrawingPolygon = false;
                        this.isDrawingWithMouse = false;
                        if (this.listShapes.SelectedItem.ToString() == ArtPainterConstants.PolygonDisplayName)
                        {
                            this.isDrawingPolygon = true;
                        }
                        else if (this.listShapes.SelectedItem.ToString() == ArtPainterConstants.MousePaintingDisplayName)
                        {
                            this.isDrawingWithMouse = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Moves or creates the shape depending on which item is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void GraphicsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.currentManager != null)
            {
                if (e.Button == MouseButtons.Left &&
                    this.listShapes.SelectedItem != null &&
                    this.currentManager.CurrentShape != null)
                {
                    if (this.listShapes.SelectedItem.ToString() == ArtPainterConstants.MouseDisplayName)
                    {
                        this.currentManager.DoMouseAction(ref this.mouseDownPosition, e.Location);
                    }
                    else if (this.listShapes.SelectedItem.ToString() != ArtPainterConstants.PolygonDisplayName &&
                        this.listShapes.SelectedItem.ToString() != ArtPainterConstants.MousePaintingDisplayName)
                    {
                        this.currentManager.CurrentShape.Resize(
                            this.mouseDownPosition,
                            e.Location);
                    }
                    else if (this.listShapes.SelectedItem.ToString() == ArtPainterConstants.MousePaintingDisplayName &&
                        this.isDrawingWithMouse)
                    {
                        var polygon = (Polygon)this.currentManager.CurrentShape;
                        polygon.PolygonPoints.Add(e.Location);
                    }

                    this.graphicsPanel.Refresh();
                }
            }
        }

        /// <summary>
        /// Refreshes the graphics panel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void GraphicsPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.isDrawingWithMouse = false;
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Draws all shapes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void GraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            if (this.currentManager != null)
            {
                this.currentManager.Shapes.ToList().ForEach(x => x.Draw(e.Graphics));
            }
        }

        /// <summary>
        /// Changes the border color of a shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ChangeColorToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.currentManager.CurrentShape.BorderColor = ArtPainterHelper.GetColor();
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Changes the border thickness.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ChangeBorderThicknessToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var number = ArtPainterHelper.GetIntDialog(this.currentManager.CurrentShape.Border, 0, 1000000);
            this.currentManager.CurrentShape.Border = number;
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Brings the current shape to the front.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BringToFrontToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.currentManager.BringToFront(this.currentManager.CurrentShape);
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Brings the current shape to the back.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BringToBackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.currentManager.BringToBack(this.currentManager.CurrentShape);
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Clears all shapes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ClearImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.currentManager != null)
            {
                this.currentManager.Clear();
                this.graphicsPanel.Refresh();
            }
        }

        /// <summary>
        /// Rotates the current shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void RotateStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var angle = ArtPainterHelper.GetIntDialog(this.currentManager.CurrentShape.Rotation, 0, 360);
            this.currentManager.CurrentShape.Rotation = angle;
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Sets the fill color of the current shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void FillRectanlgeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.currentManager.CurrentShape != null)
            {
                this.currentManager.CurrentShape.FillColor = ArtPainterHelper.GetColor();
                this.graphicsPanel.Refresh();
            }
        }

        /// <summary>
        /// Removes a shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DeleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.currentManager.RemoveShape(this.currentManager.CurrentShape);
            this.graphicsPanel.Refresh();
        }

        /// <summary>
        /// Saves the current drawing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SaveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var dia = new SaveFileDialog();
            if (dia.ShowDialog() == DialogResult.OK)
            {
                ShapeManagerCollection.Save(dia.FileName, this.managers);
            }
        }

        /// <summary>
        /// Opens a drawing from a file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void OpenToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                var dia = new OpenFileDialog();
                if (dia.ShowDialog() == DialogResult.OK)
                {
                    if (this.managers.Any())
                    {
                        var res = MessageBox.Show("Möchten Sie das bestehende Projekt speichern?", "Speichern", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            var diaSave = new SaveFileDialog();
                            if (diaSave.ShowDialog() == DialogResult.OK)
                            {
                                ShapeManagerCollection.Save(diaSave.FileName, this.managers);
                            }
                        }
                    }

                    this.managers = ShapeManagerCollection.Load(dia.FileName);
                    this.currentManager = this.managers.FirstOrDefault();
                    this.LoadManagers();
                    this.graphicsPanel.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Die Datei kann nicht geöffnet werden.", Environment.NewLine, ex.Message));
            }
        }

        /// <summary>
        /// Creates a new drawing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NewDrawingToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var name = ArtPainterHelper.GetText("Geben Sie einen Titel ein.");
            if (name != null)
            {
                if (this.managers.Contains(name))
                {
                    MessageBox.Show("Der Name existiert bereits.");
                    return;
                }

                var shapeManager = new ShapeManager(name);
                this.managers.Add(shapeManager);
                this.currentManager = shapeManager;
                this.LoadManagers();
            }
        }

        /// <summary>
        /// Loads all managers.
        /// </summary>
        private void LoadManagers()
        {
            this.tabManagers.TabPages.Clear();
            if (this.managers.Count != 0)
            {
                this.graphicsPanel.Enabled = true;
                this.tabManagers.Enabled = true;
                foreach (var manager in this.managers)
                {
                    this.tabManagers.TabPages.Add(manager.Name);
                }
            }
            else
            {
                this.graphicsPanel.Enabled = false;
                this.tabManagers.Enabled = false;
            }
        }

        /// <summary>
        /// Loads the drawing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void TabManagers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var list = (TabControl)sender;
            if (list.SelectedTab != null)
            {
                var selectedText = list.SelectedTab.Text;
                this.currentManager = this.managers[selectedText];
                this.graphicsPanel.BackColor = Color.Empty;
                if (this.currentManager != null)
                {
                    this.graphicsPanel.BackColor = this.currentManager.FormBackGroundColor.Color;
                }

                this.graphicsPanel.Refresh();
            }
        }

        /// <summary>
        /// Deletes the current drawing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DeleteCurrentDrawingToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.managers.Count > 0)
            {
                this.currentManager.Clear();
                this.graphicsPanel.Refresh();
                this.managers.Remove(this.currentManager);
                this.LoadManagers();
                this.currentManager = this.managers.FirstOrDefault();
                this.graphicsPanel.Refresh();
            }
        }

        /// <summary>
        /// Removes all rotations from the shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void RemoveRotationsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.currentManager != null)
            {
                this.currentManager.ResetRotations();
                this.graphicsPanel.Refresh();
            }
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ListShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listShapes.SelectedItem.ToString() != ArtPainterConstants.PolygonDisplayName)
            {
                this.isDrawingPolygon = false;
            }
        }

        /// <summary>
        /// Initializes the context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.changeColorToolStripMenuItem.Enabled = true;
            this.changeBorderThicknessToolStripMenuItem.Enabled = true;
            this.rotateStripMenuItem.Enabled = true;
            if (this.currentManager.CurrentShape.GetType() == typeof(Line))
            {
                this.changeColorToolStripMenuItem.Enabled = false;
                this.changeBorderThicknessToolStripMenuItem.Enabled = false;
            }
            else if (this.currentManager.CurrentShape.GetType() == typeof(MousePaint))
            {
                this.changeBorderThicknessToolStripMenuItem.Enabled = false;
                this.changeColorToolStripMenuItem.Enabled = false;
                this.rotateStripMenuItem.Enabled = false;
            }

            this.typeContextMenuItem.Text = this.currentManager.CurrentShape.GetType().Name;
        }

        /// <summary>
        /// Changes the color of the current shape.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void GraphicsPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.currentManager.CurrentShape != null)
            {
                this.currentManager.CurrentShape.OpenDialog();
            }
        }

        /// <summary>
        /// Opens the editor.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void OpenEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentManager.CurrentShape != null)
            {
                new ShapeEditor(this.currentManager.CurrentShape).ShowDialog();
            }
        }

        /// <summary>
        /// Changes the back color.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ChangeBackColorPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.currentManager.FormBackGroundColor = new CustomColor(ArtPainterHelper.GetColor());
            this.LoadManagers();
        }

        /// <summary>
        /// Exports the graphics to a file.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ExportToPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var graphics = this.graphicsPanel.CreateGraphics();
            var bitmap = new Bitmap(this.graphicsPanel.Width, this.graphicsPanel.Height);
            this.graphicsPanel.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, this.graphicsPanel.Width, this.graphicsPanel.Height));
            var dia = new SaveFileDialog();
            if (dia.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(dia.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}