namespace SO_Projekt
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.train = new System.Windows.Forms.PictureBox();
            this.szlaban_1 = new System.Windows.Forms.PictureBox();
            this.szlaban_2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.train)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.szlaban_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.szlaban_2)).BeginInit();
            this.SuspendLayout();
            // 
            // train
            // 
            this.train.BackColor = System.Drawing.SystemColors.ControlDark;
            this.train.Image = ((System.Drawing.Image)(resources.GetObject("train.Image")));
            this.train.Location = new System.Drawing.Point(1044, 0);
            this.train.Name = "train";
            this.train.Size = new System.Drawing.Size(60, 66);
            this.train.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.train.TabIndex = 0;
            this.train.TabStop = false;
            // 
            // szlaban_1
            // 
            this.szlaban_1.Location = new System.Drawing.Point(1010, 491);
            this.szlaban_1.Name = "szlaban_1";
            this.szlaban_1.Size = new System.Drawing.Size(26, 113);
            this.szlaban_1.TabIndex = 1;
            this.szlaban_1.TabStop = false;
            this.szlaban_1.Visible = false;
            // 
            // szlaban_2
            // 
            this.szlaban_2.Location = new System.Drawing.Point(1114, 491);
            this.szlaban_2.Name = "szlaban_2";
            this.szlaban_2.Size = new System.Drawing.Size(26, 113);
            this.szlaban_2.TabIndex = 2;
            this.szlaban_2.TabStop = false;
            this.szlaban_2.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1230, 651);
            this.Controls.Add(this.szlaban_2);
            this.Controls.Add(this.szlaban_1);
            this.Controls.Add(this.train);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.train)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.szlaban_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.szlaban_2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox train;
        private System.Windows.Forms.PictureBox szlaban_1;
        private System.Windows.Forms.PictureBox szlaban_2;
    }
}

