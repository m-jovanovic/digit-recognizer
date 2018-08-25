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
            this.panelBenchmarkContainer = new System.Windows.Forms.Panel();
            this.lblAccuracyValue = new System.Windows.Forms.Label();
            this.lblProgressVal = new System.Windows.Forms.Label();
            this.btnCancelBenchmark = new System.Windows.Forms.Button();
            this.btnRunBenchmark = new System.Windows.Forms.Button();
            this.labelBenchmark = new System.Windows.Forms.Label();
            this.panelLine = new System.Windows.Forms.Panel();
            this.pBarAccuracy = new System.Windows.Forms.ProgressBar();
            this.pBarProgress = new System.Windows.Forms.ProgressBar();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.panelGridContainer = new System.Windows.Forms.Panel();
            this.panelBenchmarkContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBenchmarkContainer
            // 
            this.panelBenchmarkContainer.BackColor = System.Drawing.Color.White;
            this.panelBenchmarkContainer.Controls.Add(this.lblAccuracyValue);
            this.panelBenchmarkContainer.Controls.Add(this.lblProgressVal);
            this.panelBenchmarkContainer.Controls.Add(this.btnCancelBenchmark);
            this.panelBenchmarkContainer.Controls.Add(this.btnRunBenchmark);
            this.panelBenchmarkContainer.Controls.Add(this.labelBenchmark);
            this.panelBenchmarkContainer.Controls.Add(this.panelLine);
            this.panelBenchmarkContainer.Controls.Add(this.pBarAccuracy);
            this.panelBenchmarkContainer.Controls.Add(this.pBarProgress);
            this.panelBenchmarkContainer.Controls.Add(this.lblAccuracy);
            this.panelBenchmarkContainer.Controls.Add(this.lblProgress);
            this.panelBenchmarkContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBenchmarkContainer.Location = new System.Drawing.Point(5, 435);
            this.panelBenchmarkContainer.Name = "panelBenchmarkContainer";
            this.panelBenchmarkContainer.Size = new System.Drawing.Size(911, 184);
            this.panelBenchmarkContainer.TabIndex = 0;
            // 
            // lblAccuracyValue
            // 
            this.lblAccuracyValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccuracyValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccuracyValue.Location = new System.Drawing.Point(812, 124);
            this.lblAccuracyValue.Name = "lblAccuracyValue";
            this.lblAccuracyValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAccuracyValue.Size = new System.Drawing.Size(96, 20);
            this.lblAccuracyValue.TabIndex = 16;
            this.lblAccuracyValue.Text = "0%";
            // 
            // lblProgressVal
            // 
            this.lblProgressVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgressVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressVal.Location = new System.Drawing.Point(842, 61);
            this.lblProgressVal.Name = "lblProgressVal";
            this.lblProgressVal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblProgressVal.Size = new System.Drawing.Size(66, 20);
            this.lblProgressVal.TabIndex = 15;
            this.lblProgressVal.Text = "0%";
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
            // panelGridContainer
            // 
            this.panelGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridContainer.Location = new System.Drawing.Point(5, 5);
            this.panelGridContainer.Name = "panelGridContainer";
            this.panelGridContainer.Size = new System.Drawing.Size(911, 430);
            this.panelGridContainer.TabIndex = 1;
            // 
            // BenchmarkView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelGridContainer);
            this.Controls.Add(this.panelBenchmarkContainer);
            this.Name = "BenchmarkView";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(921, 624);
            this.panelBenchmarkContainer.ResumeLayout(false);
            this.panelBenchmarkContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBenchmarkContainer;
        private System.Windows.Forms.Button btnRunBenchmark;
        private System.Windows.Forms.Label labelBenchmark;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.ProgressBar pBarAccuracy;
        private System.Windows.Forms.ProgressBar pBarProgress;
        private System.Windows.Forms.Label lblAccuracy;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnCancelBenchmark;
        private System.Windows.Forms.Label lblProgressVal;
        private System.Windows.Forms.Label lblAccuracyValue;
        private System.Windows.Forms.Panel panelGridContainer;
    }
}
