namespace ArcadeSubPlayList_Generator
{
	partial class Form2
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
			comboBox1 = new ComboBox();
			button1 = new Button();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// comboBox1
			// 
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(7, 37);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(395, 23);
			comboBox1.TabIndex = 7;
			// 
			// button1
			// 
			button1.Location = new Point(408, 36);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 6;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(7, 8);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(395, 23);
			textBox1.TabIndex = 5;
			// 
			// Form2
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(512, 79);
			Controls.Add(comboBox1);
			Controls.Add(button1);
			Controls.Add(textBox1);
			Name = "Form2";
			Text = "Form2";
			Load += Form2_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox comboBox1;
		private Button button1;
		private TextBox textBox1;
	}
}