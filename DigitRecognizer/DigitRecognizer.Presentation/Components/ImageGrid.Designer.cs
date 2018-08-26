namespace DigitRecognizer.Presentation.Components
{
    partial class ImageGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageGrid));
            this.panelCurrentImagesGrid = new System.Windows.Forms.Panel();
            this.panelIncorrectImagesGrid = new System.Windows.Forms.Panel();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnCurrentGridPrev = new System.Windows.Forms.Button();
            this.btnCurrentGridNext = new System.Windows.Forms.Button();
            this.btnIncorrectGridNext = new System.Windows.Forms.Button();
            this.btnIncorrectGridPrev = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelCurrentImagesGrid
            // 
            this.panelCurrentImagesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelCurrentImagesGrid.BackColor = System.Drawing.Color.White;
            this.panelCurrentImagesGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCurrentImagesGrid.Location = new System.Drawing.Point(3, 118);
            this.panelCurrentImagesGrid.MaximumSize = new System.Drawing.Size(560, 560);
            this.panelCurrentImagesGrid.MinimumSize = new System.Drawing.Size(560, 560);
            this.panelCurrentImagesGrid.Name = "panelCurrentImagesGrid";
            this.panelCurrentImagesGrid.Size = new System.Drawing.Size(560, 560);
            this.panelCurrentImagesGrid.TabIndex = 0;
            // 
            // panelIncorrectImagesGrid
            // 
            this.panelIncorrectImagesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIncorrectImagesGrid.BackColor = System.Drawing.Color.White;
            this.panelIncorrectImagesGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelIncorrectImagesGrid.Location = new System.Drawing.Point(590, 118);
            this.panelIncorrectImagesGrid.MaximumSize = new System.Drawing.Size(560, 560);
            this.panelIncorrectImagesGrid.MinimumSize = new System.Drawing.Size(560, 560);
            this.panelIncorrectImagesGrid.Name = "panelIncorrectImagesGrid";
            this.panelIncorrectImagesGrid.Size = new System.Drawing.Size(560, 560);
            this.panelIncorrectImagesGrid.TabIndex = 1;
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeft.Location = new System.Drawing.Point(2, 14);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(205, 20);
            this.lblLeft.TabIndex = 11;
            this.lblLeft.Text = "Currently processed images";
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRight.Location = new System.Drawing.Point(586, 14);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(206, 20);
            this.lblRight.TabIndex = 12;
            this.lblRight.Text = "Inocrrectly classified images";
            // 
            // lblNote
            // 
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(3, 47);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(253, 40);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "*Note: left digit represents the label, while the right digit represents the pred" +
    "iction.";
            // 
            // btnCurrentGridPrev
            // 
            this.btnCurrentGridPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCurrentGridPrev.BackgroundImage")));
            this.btnCurrentGridPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCurrentGridPrev.Location = new System.Drawing.Point(560, 47);
            this.btnCurrentGridPrev.Name = "btnCurrentGridPrev";
            this.btnCurrentGridPrev.Size = new System.Drawing.Size(50, 40);
            this.btnCurrentGridPrev.TabIndex = 14;
            this.btnCurrentGridPrev.UseVisualStyleBackColor = true;
            // 
            // btnCurrentGridNext
            // 
            this.btnCurrentGridNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCurrentGridNext.BackgroundImage")));
            this.btnCurrentGridNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCurrentGridNext.Location = new System.Drawing.Point(610, 47);
            this.btnCurrentGridNext.Name = "btnCurrentGridNext";
            this.btnCurrentGridNext.Size = new System.Drawing.Size(50, 40);
            this.btnCurrentGridNext.TabIndex = 15;
            this.btnCurrentGridNext.UseVisualStyleBackColor = true;
            // 
            // btnIncorrectGridNext
            // 
            this.btnIncorrectGridNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIncorrectGridNext.BackgroundImage")));
            this.btnIncorrectGridNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIncorrectGridNext.Location = new System.Drawing.Point(764, 47);
            this.btnIncorrectGridNext.Name = "btnIncorrectGridNext";
            this.btnIncorrectGridNext.Size = new System.Drawing.Size(50, 40);
            this.btnIncorrectGridNext.TabIndex = 17;
            this.btnIncorrectGridNext.UseVisualStyleBackColor = true;
            // 
            // btnIncorrectGridPrev
            // 
            this.btnIncorrectGridPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIncorrectGridPrev.BackgroundImage")));
            this.btnIncorrectGridPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIncorrectGridPrev.Location = new System.Drawing.Point(714, 47);
            this.btnIncorrectGridPrev.Name = "btnIncorrectGridPrev";
            this.btnIncorrectGridPrev.Size = new System.Drawing.Size(50, 40);
            this.btnIncorrectGridPrev.TabIndex = 16;
            this.btnIncorrectGridPrev.UseVisualStyleBackColor = true;
            // 
            // ImageGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnIncorrectGridNext);
            this.Controls.Add(this.btnIncorrectGridPrev);
            this.Controls.Add(this.btnCurrentGridNext);
            this.Controls.Add(this.btnCurrentGridPrev);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.panelIncorrectImagesGrid);
            this.Controls.Add(this.panelCurrentImagesGrid);
            this.Name = "ImageGrid";
            this.Size = new System.Drawing.Size(1150, 681);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCurrentImagesGrid;
        private System.Windows.Forms.Panel panelIncorrectImagesGrid;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Button btnCurrentGridPrev;
        private System.Windows.Forms.Button btnCurrentGridNext;
        private System.Windows.Forms.Button btnIncorrectGridNext;
        private System.Windows.Forms.Button btnIncorrectGridPrev;
    }
}
