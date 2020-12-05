namespace ResxTranslator
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
            this.btnGo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInputBrowse = new System.Windows.Forms.Button();
            this.tbInputPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGo.Location = new System.Drawing.Point(645, 73);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.BtnGoClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInputBrowse);
            this.groupBox1.Controls.Add(this.tbInputPath);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(726, 112);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // btnInputBrowse
            // 
            this.btnInputBrowse.Location = new System.Drawing.Point(645, 44);
            this.btnInputBrowse.Name = "btnInputBrowse";
            this.btnInputBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnInputBrowse.TabIndex = 3;
            this.btnInputBrowse.Text = "browse...";
            this.btnInputBrowse.UseVisualStyleBackColor = true;
            this.btnInputBrowse.Click += new System.EventHandler(this.BtnInputBrowseClick);
            // 
            // tbInputPath
            // 
            this.tbInputPath.Location = new System.Drawing.Point(9, 47);
            this.tbInputPath.Name = "tbInputPath";
            this.tbInputPath.Size = new System.Drawing.Size(543, 20);
            this.tbInputPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Input folder (that contains English resources):";
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 143);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(726, 428);
            this.txtConsole.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 583);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Translator Bot";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbInputPath;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnInputBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtConsole;
	}
}