﻿// <auto-generated />
using System.Drawing;
namespace ArtPainter
{
    partial class ArtPainter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.graphicsPanel = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zeichnungLeerenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeRotationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCurrentDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listShapes = new System.Windows.Forms.ListBox();
            this.contextMenuStripShape = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.typeContextMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.changeBorderThicknessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BringToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inDenHintergrundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillRectanlgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.löschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabManagers = new System.Windows.Forms.TabControl();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChangeBackColorPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.contextMenuStripShape.SuspendLayout();
            this.panelContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphicsPanel
            // 
            this.graphicsPanel.AllowDrop = true;
            this.graphicsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsPanel.Location = new System.Drawing.Point(258, 55);
            this.graphicsPanel.Name = "graphicsPanel";
            this.graphicsPanel.Size = new System.Drawing.Size(489, 407);
            this.graphicsPanel.TabIndex = 0;
            this.graphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicsPanel_Paint);
            this.graphicsPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel_MouseClick);
            this.graphicsPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel_MouseDoubleClick);
            this.graphicsPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel_MouseDown);
            this.graphicsPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel_MouseMove);
            this.graphicsPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel_MouseUp);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.bearbeitenToolStripMenuItem,
            this.drawingToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(759, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.openToolStripMenuItem.Text = "Öffnen";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveToolStripMenuItem.Text = "Speichern";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // bearbeitenToolStripMenuItem
            // 
            this.bearbeitenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zeichnungLeerenToolStripMenuItem,
            this.removeRotationsToolStripMenuItem});
            this.bearbeitenToolStripMenuItem.Name = "bearbeitenToolStripMenuItem";
            this.bearbeitenToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.bearbeitenToolStripMenuItem.Text = "Bearbeiten";
            // 
            // zeichnungLeerenToolStripMenuItem
            // 
            this.zeichnungLeerenToolStripMenuItem.Name = "zeichnungLeerenToolStripMenuItem";
            this.zeichnungLeerenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.zeichnungLeerenToolStripMenuItem.Text = "Zeichnung leeren";
            this.zeichnungLeerenToolStripMenuItem.Click += new System.EventHandler(this.ClearImageToolStripMenuItem_Click);
            // 
            // removeRotationsToolStripMenuItem
            // 
            this.removeRotationsToolStripMenuItem.Name = "removeRotationsToolStripMenuItem";
            this.removeRotationsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removeRotationsToolStripMenuItem.Text = "Rotationen entfernen";
            this.removeRotationsToolStripMenuItem.Click += new System.EventHandler(this.RemoveRotationsToolStripMenuItem_Click);
            // 
            // drawingToolStripMenuItem
            // 
            this.drawingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrawingToolStripMenuItem,
            this.deleteCurrentDrawingToolStripMenuItem,
            this.exportToPNGToolStripMenuItem});
            this.drawingToolStripMenuItem.Name = "drawingToolStripMenuItem";
            this.drawingToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.drawingToolStripMenuItem.Text = "Zeichnung";
            // 
            // newDrawingToolStripMenuItem
            // 
            this.newDrawingToolStripMenuItem.Name = "newDrawingToolStripMenuItem";
            this.newDrawingToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newDrawingToolStripMenuItem.Text = "Neu";
            this.newDrawingToolStripMenuItem.Click += new System.EventHandler(this.NewDrawingToolStripMenuItem_Click);
            // 
            // deleteCurrentDrawingToolStripMenuItem
            // 
            this.deleteCurrentDrawingToolStripMenuItem.Name = "deleteCurrentDrawingToolStripMenuItem";
            this.deleteCurrentDrawingToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteCurrentDrawingToolStripMenuItem.Text = "Aktuelle löschen";
            this.deleteCurrentDrawingToolStripMenuItem.Click += new System.EventHandler(this.DeleteCurrentDrawingToolStripMenuItem_Click);
            // 
            // listShapes
            // 
            this.listShapes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listShapes.FormattingEnabled = true;
            this.listShapes.ItemHeight = 25;
            this.listShapes.Items.AddRange(new object[] {
            "-"});
            this.listShapes.Location = new System.Drawing.Point(12, 27);
            this.listShapes.Name = "listShapes";
            this.listShapes.Size = new System.Drawing.Size(240, 429);
            this.listShapes.TabIndex = 2;
            this.listShapes.SelectedIndexChanged += new System.EventHandler(this.ListShapes_SelectedIndexChanged);
            // 
            // contextMenuStripShape
            // 
            this.contextMenuStripShape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.typeContextMenuItem,
            this.changeBorderThicknessToolStripMenuItem,
            this.editorÖffnenToolStripMenuItem,
            this.BringToFrontToolStripMenuItem,
            this.inDenHintergrundToolStripMenuItem,
            this.rotateStripMenuItem,
            this.changeColorToolStripMenuItem,
            this.fillRectanlgeToolStripMenuItem,
            this.löschenToolStripMenuItem});
            this.contextMenuStripShape.Name = "contextMenuStripShape";
            this.contextMenuStripShape.Size = new System.Drawing.Size(187, 205);
            this.contextMenuStripShape.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // typeContextMenuItem
            // 
            this.typeContextMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.typeContextMenuItem.Name = "typeContextMenuItem";
            this.typeContextMenuItem.ReadOnly = true;
            this.typeContextMenuItem.Size = new System.Drawing.Size(100, 23);
            // 
            // changeBorderThicknessToolStripMenuItem
            // 
            this.changeBorderThicknessToolStripMenuItem.Name = "changeBorderThicknessToolStripMenuItem";
            this.changeBorderThicknessToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.changeBorderThicknessToolStripMenuItem.Text = "Rahmendicke ändern";
            this.changeBorderThicknessToolStripMenuItem.Click += new System.EventHandler(this.ChangeBorderThicknessToolStripMenuItem_Click);
            // 
            // editorÖffnenToolStripMenuItem
            // 
            this.editorÖffnenToolStripMenuItem.Name = "editorÖffnenToolStripMenuItem";
            this.editorÖffnenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.editorÖffnenToolStripMenuItem.Text = "Editor öffnen";
            this.editorÖffnenToolStripMenuItem.Click += new System.EventHandler(this.OpenEditorToolStripMenuItem_Click);
            // 
            // BringToFrontToolStripMenuItem
            // 
            this.BringToFrontToolStripMenuItem.Name = "BringToFrontToolStripMenuItem";
            this.BringToFrontToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.BringToFrontToolStripMenuItem.Text = "In den Vordergrund";
            this.BringToFrontToolStripMenuItem.Click += new System.EventHandler(this.BringToFrontToolStripMenuItem_Click);
            // 
            // inDenHintergrundToolStripMenuItem
            // 
            this.inDenHintergrundToolStripMenuItem.Name = "inDenHintergrundToolStripMenuItem";
            this.inDenHintergrundToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.inDenHintergrundToolStripMenuItem.Text = "In den Hintergrund";
            this.inDenHintergrundToolStripMenuItem.Click += new System.EventHandler(this.BringToBackToolStripMenuItem_Click);
            // 
            // rotateStripMenuItem
            // 
            this.rotateStripMenuItem.Name = "rotateStripMenuItem";
            this.rotateStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.rotateStripMenuItem.Text = "Rotieren";
            this.rotateStripMenuItem.Click += new System.EventHandler(this.RotateStripMenuItem_Click);
            // 
            // changeColorToolStripMenuItem
            // 
            this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
            this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.changeColorToolStripMenuItem.Text = "Rahmenfarbe ändern";
            this.changeColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeColorToolStripMenuItem_Click);
            // 
            // fillRectanlgeToolStripMenuItem
            // 
            this.fillRectanlgeToolStripMenuItem.Name = "fillRectanlgeToolStripMenuItem";
            this.fillRectanlgeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.fillRectanlgeToolStripMenuItem.Text = "Farbe ändern";
            this.fillRectanlgeToolStripMenuItem.Click += new System.EventHandler(this.FillRectanlgeToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // tabManagers
            // 
            this.tabManagers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabManagers.Location = new System.Drawing.Point(258, 27);
            this.tabManagers.Name = "tabManagers";
            this.tabManagers.SelectedIndex = 0;
            this.tabManagers.Size = new System.Drawing.Size(489, 22);
            this.tabManagers.TabIndex = 0;
            this.tabManagers.SelectedIndexChanged += new System.EventHandler(this.TabManagers_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Items";
            // 
            // panelContext
            // 
            this.panelContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeBackColorPanelToolStripMenuItem});
            this.panelContext.Name = "panelContext";
            this.panelContext.Size = new System.Drawing.Size(207, 26);
            // 
            // ChangeBackColorPanelToolStripMenuItem
            // 
            this.ChangeBackColorPanelToolStripMenuItem.Name = "ChangeBackColorPanelToolStripMenuItem";
            this.ChangeBackColorPanelToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ChangeBackColorPanelToolStripMenuItem.Text = "Hintergrundfarbe ändern";
            this.ChangeBackColorPanelToolStripMenuItem.Click += new System.EventHandler(this.ChangeBackColorPanelToolStripMenuItem_Click);
            // 
            // exportToPNGToolStripMenuItem
            // 
            this.exportToPNGToolStripMenuItem.Name = "exportToPNGToolStripMenuItem";
            this.exportToPNGToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exportToPNGToolStripMenuItem.Text = "Export to PNG";
            this.exportToPNGToolStripMenuItem.Click += new System.EventHandler(this.ExportToPNGToolStripMenuItem_Click);
            // 
            // ArtPainter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 474);
            this.Controls.Add(this.tabManagers);
            this.Controls.Add(this.listShapes);
            this.Controls.Add(this.graphicsPanel);
            this.Controls.Add(this.menu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menu;
            this.Name = "ArtPainter";
            this.ShowIcon = false;
            this.Text = "Art Painter";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.contextMenuStripShape.ResumeLayout(false);
            this.contextMenuStripShape.PerformLayout();
            this.panelContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel graphicsPanel;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bearbeitenToolStripMenuItem;
        private System.Windows.Forms.ListBox listShapes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripShape;
        private System.Windows.Forms.ToolStripMenuItem changeColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeBorderThicknessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BringToFrontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zeichnungLeerenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillRectanlgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDrawingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCurrentDrawingToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox typeContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inDenHintergrundToolStripMenuItem;
        private System.Windows.Forms.TabControl tabManagers;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem removeRotationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorÖffnenToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip panelContext;
        private System.Windows.Forms.ToolStripMenuItem ChangeBackColorPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToPNGToolStripMenuItem;

    }
}

