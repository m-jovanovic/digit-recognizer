namespace DigitRecognizer.Presentation
{
    partial class MainForm
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
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDecide = new System.Windows.Forms.Button();
            this.lblDecision = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.Location = new System.Drawing.Point(43, 30);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(400, 400);
            this.drawingPanel.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(368, 474);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnDecide
            // 
            this.btnDecide.BackColor = System.Drawing.Color.White;
            this.btnDecide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecide.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecide.Location = new System.Drawing.Point(43, 474);
            this.btnDecide.Name = "btnDecide";
            this.btnDecide.Size = new System.Drawing.Size(75, 30);
            this.btnDecide.TabIndex = 2;
            this.btnDecide.Text = "Decide";
            this.btnDecide.UseVisualStyleBackColor = false;
            // 
            // lblDecision
            // 
            this.lblDecision.AutoSize = true;
            this.lblDecision.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecision.ForeColor = System.Drawing.Color.Black;
            this.lblDecision.Location = new System.Drawing.Point(40, 439);
            this.lblDecision.Name = "lblDecision";
            this.lblDecision.Size = new System.Drawing.Size(66, 24);
            this.lblDecision.TabIndex = 3;
            this.lblDecision.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(484, 521);
            this.Controls.Add(this.lblDecision);
            this.Controls.Add(this.btnDecide);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.drawingPanel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Digit Recognizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDecide;
        private System.Windows.Forms.Label lblDecision;
    }
}

