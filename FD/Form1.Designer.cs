namespace FD
{
    partial class TheForm
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
            this.leftPBox = new System.Windows.Forms.PictureBox();
            this.rightPBox = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.leftPath = new System.Windows.Forms.Label();
            this.rightPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leftPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPBox
            // 
            this.leftPBox.Location = new System.Drawing.Point(13, 13);
            this.leftPBox.Name = "leftPBox";
            this.leftPBox.Size = new System.Drawing.Size(358, 235);
            this.leftPBox.TabIndex = 0;
            this.leftPBox.TabStop = false;
            // 
            // rightPBox
            // 
            this.rightPBox.Location = new System.Drawing.Point(529, 13);
            this.rightPBox.Name = "rightPBox";
            this.rightPBox.Size = new System.Drawing.Size(358, 235);
            this.rightPBox.TabIndex = 1;
            this.rightPBox.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(389, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(123, 394);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(812, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.leftPath);
            this.groupBox1.Location = new System.Drawing.Point(13, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 177);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rightPath);
            this.groupBox2.Location = new System.Drawing.Point(529, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 177);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // leftPath
            // 
            this.leftPath.AutoSize = true;
            this.leftPath.Location = new System.Drawing.Point(6, 16);
            this.leftPath.Name = "leftPath";
            this.leftPath.Size = new System.Drawing.Size(29, 13);
            this.leftPath.TabIndex = 0;
            this.leftPath.Text = "Path";
            // 
            // rightPath
            // 
            this.rightPath.AutoSize = true;
            this.rightPath.Location = new System.Drawing.Point(6, 16);
            this.rightPath.Name = "rightPath";
            this.rightPath.Size = new System.Drawing.Size(29, 13);
            this.rightPath.TabIndex = 1;
            this.rightPath.Text = "Path";
            // 
            // TheForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 503);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.rightPBox);
            this.Controls.Add(this.leftPBox);
            this.Name = "TheForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.leftPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox leftPBox;
        private System.Windows.Forms.PictureBox rightPBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label leftPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label rightPath;
    }
}

