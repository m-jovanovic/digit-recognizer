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
            this.panelImagesGrid = new System.Windows.Forms.Panel();
            this.panelIncorrectImagesGrid = new System.Windows.Forms.Panel();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelImagesGrid
            // 
            this.panelImagesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelImagesGrid.BackColor = System.Drawing.Color.White;
            this.panelImagesGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImagesGrid.Location = new System.Drawing.Point(3, 96);
            this.panelImagesGrid.MaximumSize = new System.Drawing.Size(560, 560);
            this.panelImagesGrid.MinimumSize = new System.Drawing.Size(560, 560);
            this.panelImagesGrid.Name = "panelImagesGrid";
            this.panelImagesGrid.Size = new System.Drawing.Size(560, 560);
            this.panelImagesGrid.TabIndex = 0;
            // 
            // panelIncorrectImagesGrid
            // 
            this.panelIncorrectImagesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIncorrectImagesGrid.BackColor = System.Drawing.Color.White;
            this.panelIncorrectImagesGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelIncorrectImagesGrid.Location = new System.Drawing.Point(590, 96);
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
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(3, 47);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(480, 16);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "*Note: left digit represents the label, while the right digit represents the pred" +
    "iction.";
            // 
            // ImageGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.panelIncorrectImagesGrid);
            this.Controls.Add(this.panelImagesGrid);
            this.Name = "ImageGrid";
            this.Size = new System.Drawing.Size(1150, 659);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelImagesGrid;
        private System.Windows.Forms.Panel panelIncorrectImagesGrid;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblNote;
    }
}
