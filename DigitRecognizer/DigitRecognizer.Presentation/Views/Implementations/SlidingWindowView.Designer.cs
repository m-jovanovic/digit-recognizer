namespace DigitRecognizer.Presentation.Views.Implementations
{
    partial class SlidingWindowView
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
            this.panelDrawing = new System.Windows.Forms.Panel();
            this.btnClearDrawing = new System.Windows.Forms.Button();
            this.btnClassifyDrawing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelDrawing
            // 
            this.panelDrawing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDrawing.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDrawing.Location = new System.Drawing.Point(0, 0);
            this.panelDrawing.Name = "panelDrawing";
            this.panelDrawing.Size = new System.Drawing.Size(1178, 628);
            this.panelDrawing.TabIndex = 0;
            // 
            // btnClearDrawing
            // 
            this.btnClearDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDrawing.Location = new System.Drawing.Point(136, 634);
            this.btnClearDrawing.Name = "btnClearDrawing";
            this.btnClearDrawing.Size = new System.Drawing.Size(127, 30);
            this.btnClearDrawing.TabIndex = 24;
            this.btnClearDrawing.Text = "Clear drawing";
            this.btnClearDrawing.UseVisualStyleBackColor = true;
            // 
            // btnClassifyDrawing
            // 
            this.btnClassifyDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClassifyDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClassifyDrawing.Location = new System.Drawing.Point(3, 634);
            this.btnClassifyDrawing.Name = "btnClassifyDrawing";
            this.btnClassifyDrawing.Size = new System.Drawing.Size(127, 30);
            this.btnClassifyDrawing.TabIndex = 23;
            this.btnClassifyDrawing.Text = "Classify drawing";
            this.btnClassifyDrawing.UseVisualStyleBackColor = true;
            // 
            // SlidingWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnClearDrawing);
            this.Controls.Add(this.btnClassifyDrawing);
            this.Controls.Add(this.panelDrawing);
            this.Name = "SlidingWindowView";
            this.Size = new System.Drawing.Size(1178, 667);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrawing;
        private System.Windows.Forms.Button btnClearDrawing;
        private System.Windows.Forms.Button btnClassifyDrawing;
    }
}
