using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcadeSubPlayList_Generator
{
	public partial class Form2 : Form
	{
		public string res = "";
		public string type = "";
		public Form2()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			res = textBox1.Text;
			if (comboBox1.SelectedItem == null) type = "";
			else type = comboBox1.SelectedItem.ToString();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
			comboBox1.Items.Add("Platform");
			comboBox1.Items.Add("Category");
			comboBox1.Items.Add("Playlist");

		}
	}
}
