namespace ConsoleApp.C7
{
    partial class BackgroundWorkerForm
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
            this.progressBarSecond = new System.Windows.Forms.ProgressBar();
            this.progressBarFirst = new System.Windows.Forms.ProgressBar();
            this.btnFirstOn = new System.Windows.Forms.Button();
            this.btnFirstOff = new System.Windows.Forms.Button();
            this.btnSecondOn = new System.Windows.Forms.Button();
            this.btnSecondOff = new System.Windows.Forms.Button();
            this.bWorkerFirst = new System.ComponentModel.BackgroundWorker();
            this.bWorkerSecond = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBarSecond
            // 
            this.progressBarSecond.Location = new System.Drawing.Point(13, 70);
            this.progressBarSecond.Name = "progressBarSecond";
            this.progressBarSecond.Size = new System.Drawing.Size(504, 23);
            this.progressBarSecond.TabIndex = 0;
            // 
            // progressBarFirst
            // 
            this.progressBarFirst.Location = new System.Drawing.Point(13, 41);
            this.progressBarFirst.Name = "progressBarFirst";
            this.progressBarFirst.Size = new System.Drawing.Size(504, 23);
            this.progressBarFirst.TabIndex = 1;
            // 
            // btnFirstOn
            // 
            this.btnFirstOn.Location = new System.Drawing.Point(24, 12);
            this.btnFirstOn.Name = "btnFirstOn";
            this.btnFirstOn.Size = new System.Drawing.Size(75, 23);
            this.btnFirstOn.TabIndex = 2;
            this.btnFirstOn.Text = "Start";
            this.btnFirstOn.UseVisualStyleBackColor = true;
            this.btnFirstOn.Click += new System.EventHandler(this.btnFirstOn_Click);
            // 
            // btnFirstOff
            // 
            this.btnFirstOff.Location = new System.Drawing.Point(105, 12);
            this.btnFirstOff.Name = "btnFirstOff";
            this.btnFirstOff.Size = new System.Drawing.Size(75, 23);
            this.btnFirstOff.TabIndex = 2;
            this.btnFirstOff.Text = "Stop";
            this.btnFirstOff.UseVisualStyleBackColor = true;
            this.btnFirstOff.Click += new System.EventHandler(this.btnFirstOff_Click);
            // 
            // btnSecondOn
            // 
            this.btnSecondOn.Location = new System.Drawing.Point(352, 12);
            this.btnSecondOn.Name = "btnSecondOn";
            this.btnSecondOn.Size = new System.Drawing.Size(75, 23);
            this.btnSecondOn.TabIndex = 2;
            this.btnSecondOn.Text = "Start";
            this.btnSecondOn.UseVisualStyleBackColor = true;
            this.btnSecondOn.Click += new System.EventHandler(this.btnSecondOn_Click);
            // 
            // btnSecondOff
            // 
            this.btnSecondOff.Location = new System.Drawing.Point(433, 12);
            this.btnSecondOff.Name = "btnSecondOff";
            this.btnSecondOff.Size = new System.Drawing.Size(75, 23);
            this.btnSecondOff.TabIndex = 2;
            this.btnSecondOff.Text = "Stop";
            this.btnSecondOff.UseVisualStyleBackColor = true;
            this.btnSecondOff.Click += new System.EventHandler(this.btnSecondOff_Click);
            // 
            // bWorkerFirst
            // 
            this.bWorkerFirst.WorkerReportsProgress = true;
            this.bWorkerFirst.WorkerSupportsCancellation = true;
            this.bWorkerFirst.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWorkerFirst_DoWork);
            this.bWorkerFirst.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWorkerFirst_ProgressChanged);
            this.bWorkerFirst.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWorkerFirst_RunWorkerCompleted);
            // 
            // bWorkerSecond
            // 
            this.bWorkerSecond.WorkerReportsProgress = true;
            this.bWorkerSecond.WorkerSupportsCancellation = true;
            // 
            // BackgroundWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 104);
            this.Controls.Add(this.btnSecondOff);
            this.Controls.Add(this.btnSecondOn);
            this.Controls.Add(this.btnFirstOff);
            this.Controls.Add(this.btnFirstOn);
            this.Controls.Add(this.progressBarFirst);
            this.Controls.Add(this.progressBarSecond);
            this.Name = "BackgroundWorkerForm";
            this.Text = "BackgroundWorkerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarSecond;
        private System.Windows.Forms.ProgressBar progressBarFirst;
        private System.Windows.Forms.Button btnFirstOn;
        private System.Windows.Forms.Button btnFirstOff;
        private System.Windows.Forms.Button btnSecondOn;
        private System.Windows.Forms.Button btnSecondOff;
        private System.ComponentModel.BackgroundWorker bWorkerFirst;
        private System.ComponentModel.BackgroundWorker bWorkerSecond;
    }
}