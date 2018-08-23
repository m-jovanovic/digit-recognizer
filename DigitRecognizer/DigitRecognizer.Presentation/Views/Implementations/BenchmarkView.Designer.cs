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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelBenchmark = new System.Windows.Forms.Button();
            this.btnRunBenchmark = new System.Windows.Forms.Button();
            this.labelBenchmark = new System.Windows.Forms.Label();
            this.panelLine = new System.Windows.Forms.Panel();
            this.pBarAccuracy = new System.Windows.Forms.ProgressBar();
            this.pBarProgress = new System.Windows.Forms.ProgressBar();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnCancelBenchmark);
            this.panel1.Controls.Add(this.btnRunBenchmark);
            this.panel1.Controls.Add(this.labelBenchmark);
            this.panel1.Controls.Add(this.panelLine);
            this.panel1.Controls.Add(this.pBarAccuracy);
            this.panel1.Controls.Add(this.pBarProgress);
            this.panel1.Controls.Add(this.lblAccuracy);
            this.panel1.Controls.Add(this.lblProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(5, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 184);
            this.panel1.TabIndex = 0;
            // 
            // btnCancelBenchmark
            // 
            this.btnCancelBenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelBenchmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelBenchmark.Location = new System.Drawing.Point(776, 9);
            this.btnCancelBenchmark.Name = "btnCancelBenchmark";
            this.btnCancelBenchmark.Size = new System.Drawing.Size(127, 30);
            this.btnCancelBenchmark.TabIndex = 14;
            this.btnCancelBenchmark.Text = "Cancel";
            this.btnCancelBenchmark.UseVisualStyleBackColor = true;
            // 
            // btnRunBenchmark
            // 
            this.btnRunBenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunBenchmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunBenchmark.Location = new System.Drawing.Point(643, 9);
            this.btnRunBenchmark.Name = "btnRunBenchmark";
            this.btnRunBenchmark.Size = new System.Drawing.Size(127, 30);
            this.btnRunBenchmark.TabIndex = 8;
            this.btnRunBenchmark.Text = "Run benchmark";
            this.btnRunBenchmark.UseVisualStyleBackColor = true;
            // 
            // labelBenchmark
            // 
            this.labelBenchmark.AutoSize = true;
            this.labelBenchmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBenchmark.Location = new System.Drawing.Point(5, 15);
            this.labelBenchmark.Name = "labelBenchmark";
            this.labelBenchmark.Size = new System.Drawing.Size(115, 24);
            this.labelBenchmark.TabIndex = 7;
            this.labelBenchmark.Text = "Benchmark";
            // 
            // panelLine
            // 
            this.panelLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLine.BackColor = System.Drawing.Color.Black;
            this.panelLine.Location = new System.Drawing.Point(7, 45);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(896, 3);
            this.panelLine.TabIndex = 9;
            // 
            // pBarAccuracy
            // 
            this.pBarAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBarAccuracy.ForeColor = System.Drawing.Color.Violet;
            this.pBarAccuracy.Location = new System.Drawing.Point(7, 151);
            this.pBarAccuracy.MarqueeAnimationSpeed = 0;
            this.pBarAccuracy.Maximum = 10000;
            this.pBarAccuracy.Name = "pBarAccuracy";
            this.pBarAccuracy.Size = new System.Drawing.Size(897, 23);
            this.pBarAccuracy.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBarAccuracy.TabIndex = 13;
            // 
            // pBarProgress
            // 
            this.pBarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBarProgress.ForeColor = System.Drawing.Color.Violet;
            this.pBarProgress.Location = new System.Drawing.Point(7, 89);
            this.pBarProgress.MarqueeAnimationSpeed = 0;
            this.pBarProgress.Name = "pBarProgress";
            this.pBarProgress.Size = new System.Drawing.Size(897, 23);
            this.pBarProgress.Step = 1;
            this.pBarProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBarProgress.TabIndex = 11;
            // 
            // lblAccuracy
            // 
            this.lblAccuracy.AutoSize = true;
            this.lblAccuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccuracy.Location = new System.Drawing.Point(5, 124);
            this.lblAccuracy.Name = "lblAccuracy";
            this.lblAccuracy.Size = new System.Drawing.Size(74, 20);
            this.lblAccuracy.TabIndex = 12;
            this.lblAccuracy.Text = "Accuracy";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(5, 61);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(72, 20);
            this.lblProgress.TabIndex = 10;
            this.lblProgress.Text = "Progress";
            // 
            // BenchmarkView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "BenchmarkView";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(921, 606);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRunBenchmark;
        private System.Windows.Forms.Label labelBenchmark;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.ProgressBar pBarAccuracy;
        private System.Windows.Forms.ProgressBar pBarProgress;
        private System.Windows.Forms.Label lblAccuracy;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnCancelBenchmark;
    }
}
