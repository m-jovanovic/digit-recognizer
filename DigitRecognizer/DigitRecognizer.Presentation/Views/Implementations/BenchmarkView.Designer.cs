namespace DigitRecognizer.Presentation.Views.Implementations
{
    partial class BenchmarkView
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
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.pBarProgress = new System.Windows.Forms.ProgressBar();
            this.pBarAccuracy = new System.Windows.Forms.ProgressBar();
            this.panelLine = new System.Windows.Forms.Panel();
            this.labelBenchmark = new System.Windows.Forms.Label();
            this.btnBenchmark = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(8, 64);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(72, 20);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "Progress";
            // 
            // lblAccuracy
            // 
            this.lblAccuracy.AutoSize = true;
            this.lblAccuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccuracy.Location = new System.Drawing.Point(8, 127);
            this.lblAccuracy.Name = "lblAccuracy";
            this.lblAccuracy.Size = new System.Drawing.Size(74, 20);
            this.lblAccuracy.TabIndex = 5;
            this.lblAccuracy.Text = "Accuracy";
            // 
            // pBarProgress
            // 
            this.pBarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBarProgress.ForeColor = System.Drawing.Color.Violet;
            this.pBarProgress.Location = new System.Drawing.Point(8, 92);
            this.pBarProgress.MarqueeAnimationSpeed = 0;
            this.pBarProgress.Name = "pBarProgress";
            this.pBarProgress.Size = new System.Drawing.Size(849, 23);
            this.pBarProgress.Step = 1;
            this.pBarProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBarProgress.TabIndex = 4;
            // 
            // pBarAccuracy
            // 
            this.pBarAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBarAccuracy.ForeColor = System.Drawing.Color.Violet;
            this.pBarAccuracy.Location = new System.Drawing.Point(8, 154);
            this.pBarAccuracy.MarqueeAnimationSpeed = 0;
            this.pBarAccuracy.Maximum = 10000;
            this.pBarAccuracy.Name = "pBarAccuracy";
            this.pBarAccuracy.Size = new System.Drawing.Size(849, 23);
            this.pBarAccuracy.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBarAccuracy.TabIndex = 6;
            // 
            // panelLine
            // 
            this.panelLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLine.BackColor = System.Drawing.Color.Black;
            this.panelLine.Location = new System.Drawing.Point(8, 48);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(848, 3);
            this.panelLine.TabIndex = 2;
            // 
            // labelBenchmark
            // 
            this.labelBenchmark.AutoSize = true;
            this.labelBenchmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBenchmark.Location = new System.Drawing.Point(8, 18);
            this.labelBenchmark.Name = "labelBenchmark";
            this.labelBenchmark.Size = new System.Drawing.Size(115, 24);
            this.labelBenchmark.TabIndex = 0;
            this.labelBenchmark.Text = "Benchmark";
            // 
            // btnBenchmark
            // 
            this.btnBenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBenchmark.Location = new System.Drawing.Point(736, 12);
            this.btnBenchmark.Name = "btnBenchmark";
            this.btnBenchmark.Size = new System.Drawing.Size(120, 30);
            this.btnBenchmark.TabIndex = 1;
            this.btnBenchmark.Text = "Run benchmark";
            this.btnBenchmark.UseVisualStyleBackColor = true;
            this.btnBenchmark.Click += new System.EventHandler(this.BtnBenchmark_Click);
            // 
            // BenchmarkView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnBenchmark);
            this.Controls.Add(this.labelBenchmark);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.pBarAccuracy);
            this.Controls.Add(this.pBarProgress);
            this.Controls.Add(this.lblAccuracy);
            this.Controls.Add(this.lblProgress);
            this.Name = "BenchmarkView";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(865, 188);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblAccuracy;
        private System.Windows.Forms.ProgressBar pBarProgress;
        private System.Windows.Forms.ProgressBar pBarAccuracy;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label labelBenchmark;
        private System.Windows.Forms.Button btnBenchmark;
    }
}
