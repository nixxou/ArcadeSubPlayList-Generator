namespace ArcadeSubPlayList_Generator
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			cmb_plateform_source = new ComboBox();
			btn_explore = new Button();
			txt_xmlfolder = new TextBox();
			cmb_plateform_dest = new ComboBox();
			btn_generate = new Button();
			listbox_PlaylistSource = new ListBox();
			btn_createPlaylistAll = new Button();
			btn_CreatePlaylistFav = new Button();
			btn_CreatePlaylistTop = new Button();
			btn_changeParentNode = new Button();
			btn_delete = new Button();
			button4 = new Button();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			SuspendLayout();
			// 
			// cmb_plateform_source
			// 
			cmb_plateform_source.DropDownStyle = ComboBoxStyle.DropDownList;
			cmb_plateform_source.FormattingEnabled = true;
			cmb_plateform_source.Location = new Point(159, 37);
			cmb_plateform_source.Name = "cmb_plateform_source";
			cmb_plateform_source.Size = new Size(611, 23);
			cmb_plateform_source.TabIndex = 6;
			cmb_plateform_source.SelectedIndexChanged += cmb_plateform_source_SelectedIndexChanged;
			// 
			// btn_explore
			// 
			btn_explore.Location = new Point(695, 8);
			btn_explore.Name = "btn_explore";
			btn_explore.Size = new Size(75, 23);
			btn_explore.TabIndex = 5;
			btn_explore.Text = "...";
			btn_explore.UseVisualStyleBackColor = true;
			btn_explore.Click += btn_explore_Click;
			// 
			// txt_xmlfolder
			// 
			txt_xmlfolder.Location = new Point(159, 9);
			txt_xmlfolder.Name = "txt_xmlfolder";
			txt_xmlfolder.Size = new Size(530, 23);
			txt_xmlfolder.TabIndex = 4;
			// 
			// cmb_plateform_dest
			// 
			cmb_plateform_dest.DropDownStyle = ComboBoxStyle.DropDownList;
			cmb_plateform_dest.FormattingEnabled = true;
			cmb_plateform_dest.Location = new Point(159, 66);
			cmb_plateform_dest.Name = "cmb_plateform_dest";
			cmb_plateform_dest.Size = new Size(611, 23);
			cmb_plateform_dest.TabIndex = 7;
			// 
			// btn_generate
			// 
			btn_generate.Location = new Point(513, 95);
			btn_generate.Name = "btn_generate";
			btn_generate.Size = new Size(257, 33);
			btn_generate.TabIndex = 8;
			btn_generate.Text = "Clone selected Playlist to destination Platform";
			btn_generate.UseVisualStyleBackColor = true;
			btn_generate.Click += btn_generate_Click;
			// 
			// listbox_PlaylistSource
			// 
			listbox_PlaylistSource.FormattingEnabled = true;
			listbox_PlaylistSource.ItemHeight = 15;
			listbox_PlaylistSource.Location = new Point(12, 95);
			listbox_PlaylistSource.Name = "listbox_PlaylistSource";
			listbox_PlaylistSource.SelectionMode = SelectionMode.MultiSimple;
			listbox_PlaylistSource.Size = new Size(491, 334);
			listbox_PlaylistSource.TabIndex = 10;
			// 
			// btn_createPlaylistAll
			// 
			btn_createPlaylistAll.Location = new Point(513, 134);
			btn_createPlaylistAll.Name = "btn_createPlaylistAll";
			btn_createPlaylistAll.Size = new Size(257, 33);
			btn_createPlaylistAll.TabIndex = 12;
			btn_createPlaylistAll.Text = "Create AllGames Playlist in Dest";
			btn_createPlaylistAll.UseVisualStyleBackColor = true;
			btn_createPlaylistAll.Click += btn_createPlaylistAll_Click;
			// 
			// btn_CreatePlaylistFav
			// 
			btn_CreatePlaylistFav.Location = new Point(513, 173);
			btn_CreatePlaylistFav.Name = "btn_CreatePlaylistFav";
			btn_CreatePlaylistFav.Size = new Size(257, 33);
			btn_CreatePlaylistFav.TabIndex = 13;
			btn_CreatePlaylistFav.Text = "Create Favorite Playlist in Dest";
			btn_CreatePlaylistFav.UseVisualStyleBackColor = true;
			btn_CreatePlaylistFav.Click += btn_CreatePlaylistFav_Click;
			// 
			// btn_CreatePlaylistTop
			// 
			btn_CreatePlaylistTop.Location = new Point(513, 212);
			btn_CreatePlaylistTop.Name = "btn_CreatePlaylistTop";
			btn_CreatePlaylistTop.Size = new Size(257, 33);
			btn_CreatePlaylistTop.TabIndex = 14;
			btn_CreatePlaylistTop.Text = "Create Top Playlist in Dest";
			btn_CreatePlaylistTop.UseVisualStyleBackColor = true;
			btn_CreatePlaylistTop.Click += btn_CreatePlaylistTop_Click;
			// 
			// btn_changeParentNode
			// 
			btn_changeParentNode.Location = new Point(513, 251);
			btn_changeParentNode.Name = "btn_changeParentNode";
			btn_changeParentNode.Size = new Size(257, 33);
			btn_changeParentNode.TabIndex = 15;
			btn_changeParentNode.Text = "Change Parent Node of Selected Playlists";
			btn_changeParentNode.UseVisualStyleBackColor = true;
			btn_changeParentNode.Click += btn_changeParentNode_Click;
			// 
			// btn_delete
			// 
			btn_delete.Location = new Point(513, 290);
			btn_delete.Name = "btn_delete";
			btn_delete.Size = new Size(257, 33);
			btn_delete.TabIndex = 16;
			btn_delete.Text = "Delete Selected Playlists";
			btn_delete.UseVisualStyleBackColor = true;
			btn_delete.Click += btn_delete_Click;
			// 
			// button4
			// 
			button4.Location = new Point(513, 329);
			button4.Name = "button4";
			button4.Size = new Size(257, 33);
			button4.TabIndex = 17;
			button4.Text = "Temporary Hide Selected Playlist in Listbox";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 16);
			label1.Name = "label1";
			label1.Size = new Size(108, 15);
			label1.TabIndex = 18;
			label1.Text = "Launchbox Folder :";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 45);
			label2.Name = "label2";
			label2.Size = new Size(98, 15);
			label2.TabIndex = 19;
			label2.Text = "Source Platform :";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 74);
			label3.Name = "label3";
			label3.Size = new Size(122, 15);
			label3.TabIndex = 20;
			label3.Text = "Destination Platform :";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(799, 450);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(button4);
			Controls.Add(btn_delete);
			Controls.Add(btn_changeParentNode);
			Controls.Add(btn_CreatePlaylistTop);
			Controls.Add(btn_CreatePlaylistFav);
			Controls.Add(btn_createPlaylistAll);
			Controls.Add(listbox_PlaylistSource);
			Controls.Add(btn_generate);
			Controls.Add(cmb_plateform_dest);
			Controls.Add(cmb_plateform_source);
			Controls.Add(btn_explore);
			Controls.Add(txt_xmlfolder);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox cmb_plateform_source;
		private Button btn_explore;
		private TextBox txt_xmlfolder;
		private ComboBox cmb_plateform_dest;
		private Button btn_generate;
		private ListBox listbox_PlaylistSource;
		private Button btn_createPlaylistAll;
		private Button btn_CreatePlaylistFav;
		private Button btn_CreatePlaylistTop;
		private Button btn_changeParentNode;
		private Button btn_delete;
		private Button button4;
		private Label label1;
		private Label label2;
		private Label label3;
	}
}