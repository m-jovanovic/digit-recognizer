namespace DigitRecognizer.Presentation.Views.Implementations
{
    partial class DrawingView
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
            this.btnClearDrawing = new System.Windows.Forms.Button();
            this.btnClassifyDrawing = new System.Windows.Forms.Button();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.predictionPane = new DigitRecognizer.Presentation.Components.PredictionPane();
            this.SuspendLayout();
            // 
            // btnClearDrawing
            // 
            this.btnClearDrawing.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDrawing.Location = new System.Drawing.Point(276, 589);
            this.btnClearDrawing.Name = "btnClearDrawing";
            this.btnClearDrawing.Size = new System.Drawing.Size(127, 30);
            this.btnClearDrawing.TabIndex = 22;
            this.btnClearDrawing.Text = "Clear drawing";
            this.btnClearDrawing.UseVisualStyleBackColor = true;
            // 
            // btnClassifyDrawing
            // 
            this.btnClassifyDrawing.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClassifyDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClassifyDrawing.Location = new System.Drawing.Point(143, 589);
            this.btnClassifyDrawing.Name = "btnClassifyDrawing";
            this.btnClassifyDrawing.Size = new System.Drawing.Size(127, 30);
            this.btnClassifyDrawing.TabIndex = 21;
            this.btnClassifyDrawing.Text = "Classify drawing";
            this.btnClassifyDrawing.UseVisualStyleBackColor = true;
            // 
            // drawingPanel
            // 
            this.drawingPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingPanel.Location = new System.Drawing.Point(143, 23);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(560, 560);
            this.drawingPanel.TabIndex = 20;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(3, 0);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(178, 20);
            this.lblInstructions.TabIndex = 24;
            this.lblInstructions.Text = "Draw your number here:";
            // 
            // predictionPane
            // 
            this.predictionPane.BackColor = System.Drawing.Color.White;
            this.predictionPane.Location = new System.Drawing.Point(802, 96);
            this.predictionPane.Name = "predictionPane";
            this.predictionPane.Size = new System.Drawing.Size(550, 400);
            this.predictionPane.TabIndex = 25;
            // 
            // DrawingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.predictionPane);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnClearDrawing);
            this.Controls.Add(this.btnClassifyDrawing);
            this.Controls.Add(this.drawingPanel);
            this.Name = "DrawingView";
            this.Size = new System.Drawing.Size(1371, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClearDrawing;
        private System.Windows.Forms.Button btnClassifyDrawing;
        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Label lblInstructions;
        private Components.PredictionPane predictionPane;
    }
}
