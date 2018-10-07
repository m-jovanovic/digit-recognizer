namespace DigitRecognizer.Presentation.Views.Implementations
{
    partial class UploadImageView
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
            this.uploadPanel = new System.Windows.Forms.Panel();
            this.predictionPane = new DigitRecognizer.Presentation.Components.PredictionPane();
            this.btnClearImg = new System.Windows.Forms.Button();
            this.btnClassifyImg = new System.Windows.Forms.Button();
            this.btnUploadImg = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uploadPanel
            // 
            this.uploadPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uploadPanel.Location = new System.Drawing.Point(42, 27);
            this.uploadPanel.Name = "uploadPanel";
            this.uploadPanel.Size = new System.Drawing.Size(560, 560);
            this.uploadPanel.TabIndex = 0;
            // 
            // predictionPane
            // 
            this.predictionPane.BackColor = System.Drawing.Color.White;
            this.predictionPane.Location = new System.Drawing.Point(612, 27);
            this.predictionPane.Name = "predictionPane";
            this.predictionPane.Size = new System.Drawing.Size(550, 400);
            this.predictionPane.TabIndex = 1;
            // 
            // btnClearImg
            // 
            this.btnClearImg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearImg.Location = new System.Drawing.Point(308, 606);
            this.btnClearImg.Name = "btnClearImg";
            this.btnClearImg.Size = new System.Drawing.Size(127, 30);
            this.btnClearImg.TabIndex = 24;
            this.btnClearImg.Text = "Clear image";
            this.btnClearImg.UseVisualStyleBackColor = true;
            // 
            // btnClassifyImg
            // 
            this.btnClassifyImg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClassifyImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClassifyImg.Location = new System.Drawing.Point(175, 606);
            this.btnClassifyImg.Name = "btnClassifyImg";
            this.btnClassifyImg.Size = new System.Drawing.Size(127, 30);
            this.btnClassifyImg.TabIndex = 23;
            this.btnClassifyImg.Text = "Classify image";
            this.btnClassifyImg.UseVisualStyleBackColor = true;
            // 
            // btnUploadImg
            // 
            this.btnUploadImg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadImg.Location = new System.Drawing.Point(42, 606);
            this.btnUploadImg.Name = "btnUploadImg";
            this.btnUploadImg.Size = new System.Drawing.Size(127, 30);
            this.btnUploadImg.TabIndex = 26;
            this.btnUploadImg.Text = "Upload image";
            this.btnUploadImg.UseVisualStyleBackColor = true;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(38, 0);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(181, 20);
            this.lblInstructions.TabIndex = 27;
            this.lblInstructions.Text = "Upload your image here:";
            // 
            // UploadImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnUploadImg);
            this.Controls.Add(this.btnClearImg);
            this.Controls.Add(this.btnClassifyImg);
            this.Controls.Add(this.predictionPane);
            this.Controls.Add(this.uploadPanel);
            this.Name = "UploadImageView";
            this.Size = new System.Drawing.Size(1165, 648);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel uploadPanel;
        private Components.PredictionPane predictionPane;
        private System.Windows.Forms.Button btnClearImg;
        private System.Windows.Forms.Button btnClassifyImg;
        private System.Windows.Forms.Button btnUploadImg;
        private System.Windows.Forms.Label lblInstructions;
    }
}
