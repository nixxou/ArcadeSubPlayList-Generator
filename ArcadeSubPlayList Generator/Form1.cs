using System.Xml.Linq;
using System;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Reflection;
using Microsoft.VisualBasic;

namespace ArcadeSubPlayList_Generator
{
	public partial class Form1 : Form
	{
		Dictionary<string, string> PlaylistsSourceFiles = new Dictionary<string, string>();
		Dictionary<string, XDocument> PlaylistsSourceXdoc = new Dictionary<string, XDocument>();
		public Form1()
		{
			InitializeComponent();
		}

		private void btn_explore_Click(object sender, EventArgs e)
		{
			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					txt_xmlfolder.Text = fbd.SelectedPath;
					Properties.Settings.Default["XmlDir"] = fbd.SelectedPath;
					Properties.Settings.Default.Save();
				}
			}
			if (Directory.Exists(Path.Combine(txt_xmlfolder.Text, "Data", "Platforms")))
			{
				var playlistFiles = Directory.GetFiles(Path.Combine(txt_xmlfolder.Text, "Data", "Platforms"), "*.xml");
				foreach (var file in playlistFiles)
				{
					cmb_plateform_source.Items.Add(Path.GetFileNameWithoutExtension(file));
					cmb_plateform_dest.Items.Add(Path.GetFileNameWithoutExtension(file));
				}
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			txt_xmlfolder.Text = Properties.Settings.Default["XmlDir"].ToString();
			if (Directory.Exists(Path.Combine(txt_xmlfolder.Text, "Data", "Platforms")))
			{
				var playlistFiles = Directory.GetFiles(Path.Combine(txt_xmlfolder.Text, "Data", "Platforms"), "*.xml");
				foreach (var file in playlistFiles)
				{
					cmb_plateform_source.Items.Add(Path.GetFileNameWithoutExtension(file));
					cmb_plateform_dest.Items.Add(Path.GetFileNameWithoutExtension(file));
				}
			}
		}

		private void btn_generate_Click(object sender, EventArgs e)
		{

			string plateformSourceName = cmb_plateform_source.SelectedItem.ToString().Trim();
			if (String.IsNullOrEmpty(plateformSourceName))
			{
				MessageBox.Show("You Have to select a source Platform");
				return;
			}

			string plateformDestName = cmb_plateform_dest.SelectedItem.ToString().Trim();
			if (String.IsNullOrEmpty(plateformDestName))
			{
				MessageBox.Show("You Have to select a dest Platform");
				return;
			}

			foreach (var playlistsSourceFile in PlaylistsSourceFiles)
			{
				string playlistSourceName = playlistsSourceFile.Key;

				bool isValueSelected = false;
				foreach (var item in listbox_PlaylistSource.SelectedItems)
				{
					if (item.ToString() == playlistSourceName)
					{
						isValueSelected = true;
						break;
					}
				}
				if (!isValueSelected) { continue; }


				var playlistSourceData = PlaylistsSourceXdoc[playlistSourceName].Root.Elements("Playlist").First();
				string playlistSourceId = (string)playlistSourceData.Element("PlaylistId");
				string playlistSourceNestedName = (string)playlistSourceData.Element("NestedName");
				playlistSourceNestedName = playlistSourceNestedName.Trim();
				string newName = plateformDestName + " " + playlistSourceNestedName;
				string DestPlaylistFileName = Path.Combine(txt_xmlfolder.Text, "Data", "Playlists", newName + ".xml");

				if (File.Exists(DestPlaylistFileName)) continue;

				string PathImage = Path.Combine(Path.GetDirectoryName(txt_xmlfolder.Text), "Images", "Playlists");
				string PathImageSource = Path.Combine(PathImage, playlistSourceName);
				string PathImageDest = Path.Combine(PathImage, newName);
				if (Directory.Exists(PathImageSource))
				{
					CopyDirectoryAndRenameFile(PathImageSource, PathImageDest, playlistSourceName, newName);
				}
				playlistSourceData.SetElementValue("Name", newName);
				Guid uuid = Guid.NewGuid();
				string uuidval = uuid.ToString();
				playlistSourceData.SetElementValue("PlaylistId", uuid);
				PlaylistsSourceXdoc[playlistSourceName].Root.Elements("PlaylistFilter").Where(e => (string)e.Element("FieldKey") == "Platform").First().SetElementValue("Value", plateformDestName);
				PlaylistsSourceXdoc[playlistSourceName].Save(DestPlaylistFileName);


				var parentXmlFile = Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml");
				XDocument xdoc = XDocument.Parse(File.ReadAllText(parentXmlFile));
				var parent = xdoc.Root.Elements("Parent").Where(p => (string)p.Element("PlaylistId") == playlistSourceId).FirstOrDefault();
				if (parent != null)
				{

					var newNode = new XElement("Parent",
					 new XElement("PlaylistId", uuidval),
					 new XElement("ParentPlatformName", plateformDestName)
					 );


					xdoc.Root.Add(newNode);
					xdoc.Save(Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml"));

				}
			}
		}

		static void CopyDirectoryAndRenameFile(string sourceDir, string destDir, string originalName, string newName)
		{
			// Créer le répertoire de destination s'il n'existe pas
			if (!Directory.Exists(destDir))
			{
				Directory.CreateDirectory(destDir);
			}

			// Copier les fichiers
			foreach (string file in Directory.GetFiles(sourceDir))
			{
				string originalFile = Path.GetFileName(file);
				string newFile = originalFile.Replace(originalName, newName);

				string destFile = Path.Combine(destDir, newFile);
				if (!File.Exists(destFile))
				{
					File.Copy(file, destFile, false);
				}

			}

			// Copier les sous-répertoires récursivement
			foreach (string subDir in Directory.GetDirectories(sourceDir))
			{
				string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
				CopyDirectoryAndRenameFile(subDir, destSubDir, originalName, newName);
			}
		}

		private void cmb_plateform_source_SelectedIndexChanged(object sender, EventArgs e)
		{
			listbox_PlaylistSource.Items.Clear();
			PlaylistsSourceFiles.Clear();
			PlaylistsSourceXdoc.Clear();
			string plateformSourceName = cmb_plateform_source.SelectedItem.ToString().Trim();
			if (String.IsNullOrEmpty(plateformSourceName))
			{
				return;
			}

			var playlistFiles = Directory.GetFiles(Path.Combine(txt_xmlfolder.Text, "Data", "Playlists"), "*.xml");
			foreach (var file in playlistFiles)
			{
				XDocument xdoc = XDocument.Parse(File.ReadAllText(file));
				var playlist = xdoc.Root.Elements("Playlist").First();
				string playlistName = (string)playlist.Element("Name");
				if (playlistName.StartsWith(plateformSourceName))
				{
					if ((string)playlist.Element("AutoPopulate") == "true")
					{
						var PlateformPlaylist = (string)xdoc.Root.Elements("PlaylistFilter").Where(e => (string)e.Element("FieldKey") == "Platform").First().Element("Value");
						if (PlateformPlaylist.Contains(PlateformPlaylist))
						{
							PlaylistsSourceFiles.Add(playlistName, file);
							PlaylistsSourceXdoc.Add(playlistName, xdoc);
							listbox_PlaylistSource.Items.Add(playlistName);
						}
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var assembly = typeof(Form1).Assembly;
			Stream emptyplaylistRessource = assembly.GetManifestResourceStream("ArcadeSubPlayList_Generator.Ressources.emptyplaylist.xml");
			var sR = new StreamReader(emptyplaylistRessource);
			var emptyXmlTxt = sR.ReadToEnd();
			sR.Close();
			XDocument xdoc = XDocument.Parse(emptyXmlTxt);


		}

		private void btn_createPlaylistAll_Click(object sender, EventArgs e)
		{
			string playlistName = "All Games";
			string plateformDestName = cmb_plateform_dest.SelectedItem.ToString().Trim();
			if (String.IsNullOrEmpty(plateformDestName))
			{
				MessageBox.Show("You Have to select a dest Platform");
				return;
			}

			string newName = plateformDestName + " " + playlistName;
			string DestPlaylistFileName = Path.Combine(txt_xmlfolder.Text, "Data", "Playlists", newName + ".xml");
			if (!File.Exists(DestPlaylistFileName))
			{
				var assembly = typeof(Form1).Assembly;
				Stream emptyplaylistRessource = assembly.GetManifestResourceStream("ArcadeSubPlayList_Generator.Ressources.emptyplaylist.xml");
				var sR = new StreamReader(emptyplaylistRessource);
				var emptyXmlTxt = sR.ReadToEnd();
				sR.Close();
				XDocument playlistAllGameXdoc = XDocument.Parse(emptyXmlTxt);
				var playlistSourceData = playlistAllGameXdoc.Root.Elements("Playlist").First();
				playlistSourceData.SetElementValue("Name", newName);
				playlistSourceData.SetElementValue("NestedName", playlistName);
				playlistSourceData.SetElementValue("SortTitle", "_" + playlistName);
				Guid uuid = Guid.NewGuid();
				string uuidval = uuid.ToString();
				playlistSourceData.SetElementValue("PlaylistId", uuid);
				playlistAllGameXdoc.Root.Elements("PlaylistFilter").Where(e => (string)e.Element("FieldKey") == "Platform").First().SetElementValue("Value", plateformDestName);
				playlistAllGameXdoc.Save(DestPlaylistFileName);

				var parentXmlFile = Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml");
				XDocument xdoc = XDocument.Parse(File.ReadAllText(parentXmlFile));
				var newNode = new XElement("Parent",
					new XElement("PlaylistId", uuidval),
					new XElement("ParentPlatformName", plateformDestName)
					);
				xdoc.Root.Add(newNode);
				xdoc.Save(Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml"));


				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Clear Logo"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Banner"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Device"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Fanart"));
				File.Copy(@"Ressources\\" + playlistName + ".png", Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Clear Logo", newName + ".png"), true);

			}
			else
			{
				MessageBox.Show(plateformDestName + "already exist !");
				return;
			}
			MessageBox.Show("Done !");
		}

		private void btn_CreatePlaylistFav_Click(object sender, EventArgs e)
		{
			string playlistName = "Favorites";
			string plateformDestName = cmb_plateform_dest.SelectedItem.ToString().Trim();
			if (String.IsNullOrEmpty(plateformDestName))
			{
				MessageBox.Show("You Have to select a dest Platform");
				return;
			}

			string newName = plateformDestName + " " + playlistName;
			string DestPlaylistFileName = Path.Combine(txt_xmlfolder.Text, "Data", "Playlists", newName + ".xml");
			if (!File.Exists(DestPlaylistFileName))
			{
				var assembly = typeof(Form1).Assembly;
				Stream emptyplaylistRessource = assembly.GetManifestResourceStream("ArcadeSubPlayList_Generator.Ressources.Favorites.xml");
				var sR = new StreamReader(emptyplaylistRessource);
				var emptyXmlTxt = sR.ReadToEnd();
				sR.Close();
				XDocument playlistAllGameXdoc = XDocument.Parse(emptyXmlTxt);
				var playlistSourceData = playlistAllGameXdoc.Root.Elements("Playlist").First();
				playlistSourceData.SetElementValue("Name", newName);
				playlistSourceData.SetElementValue("NestedName", playlistName);
				playlistSourceData.SetElementValue("SortTitle", "_" + playlistName);
				Guid uuid = Guid.NewGuid();
				string uuidval = uuid.ToString();
				playlistSourceData.SetElementValue("PlaylistId", uuid);
				playlistAllGameXdoc.Root.Elements("PlaylistFilter").Where(e => (string)e.Element("FieldKey") == "Platform").First().SetElementValue("Value", plateformDestName);
				playlistAllGameXdoc.Save(DestPlaylistFileName);

				var parentXmlFile = Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml");
				XDocument xdoc = XDocument.Parse(File.ReadAllText(parentXmlFile));
				var newNode = new XElement("Parent",
					new XElement("PlaylistId", uuidval),
					new XElement("ParentPlatformName", plateformDestName)
					);
				xdoc.Root.Add(newNode);
				xdoc.Save(Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml"));


				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Clear Logo"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Banner"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Device"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Fanart"));
				File.Copy(@"Ressources\\" + playlistName + ".png", Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Clear Logo", newName + ".png"), true);

			}
			else
			{
				MessageBox.Show(plateformDestName + "already exist !");
				return;
			}
			MessageBox.Show("Done !");
		}

		private void btn_CreatePlaylistTop_Click(object sender, EventArgs e)
		{
			string playlistName = "Top";
			string plateformDestName = cmb_plateform_dest.SelectedItem.ToString().Trim();
			if (String.IsNullOrEmpty(plateformDestName))
			{
				MessageBox.Show("You Have to select a dest Platform");
				return;
			}

			string newName = plateformDestName + " " + playlistName;
			string DestPlaylistFileName = Path.Combine(txt_xmlfolder.Text, "Data", "Playlists", newName + ".xml");
			if (!File.Exists(DestPlaylistFileName))
			{
				var assembly = typeof(Form1).Assembly;
				Stream emptyplaylistRessource = assembly.GetManifestResourceStream("ArcadeSubPlayList_Generator.Ressources.Top.xml");
				var sR = new StreamReader(emptyplaylistRessource);
				var emptyXmlTxt = sR.ReadToEnd();
				sR.Close();
				XDocument playlistAllGameXdoc = XDocument.Parse(emptyXmlTxt);
				var playlistSourceData = playlistAllGameXdoc.Root.Elements("Playlist").First();
				playlistSourceData.SetElementValue("Name", newName);
				playlistSourceData.SetElementValue("NestedName", playlistName);
				playlistSourceData.SetElementValue("SortTitle", "_" + playlistName);
				Guid uuid = Guid.NewGuid();
				string uuidval = uuid.ToString();
				playlistSourceData.SetElementValue("PlaylistId", uuid);
				playlistAllGameXdoc.Root.Elements("PlaylistFilter").Where(e => (string)e.Element("FieldKey") == "Platform").First().SetElementValue("Value", plateformDestName);
				playlistAllGameXdoc.Save(DestPlaylistFileName);

				var parentXmlFile = Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml");
				XDocument xdoc = XDocument.Parse(File.ReadAllText(parentXmlFile));
				var newNode = new XElement("Parent",
					new XElement("PlaylistId", uuidval),
					new XElement("ParentPlatformName", plateformDestName)
					);
				xdoc.Root.Add(newNode);
				xdoc.Save(Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml"));


				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Clear Logo"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Banner"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Device"));
				Directory.CreateDirectory(Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Fanart"));
				File.Copy(@"Ressources\\" + playlistName + ".png", Path.Combine(txt_xmlfolder.Text, "Images", "Playlists", newName, "Clear Logo", newName + ".png"), true);

			}
			else
			{
				MessageBox.Show(plateformDestName + "already exist !");
				return;
			}
			MessageBox.Show("Done !");
		}

		private void button4_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (var selected in listbox_PlaylistSource.SelectedItems) { list.Add(selected.ToString()); }

			foreach (var selected in list)
			{
				listbox_PlaylistSource.Items.Remove(selected);
			}
		}

		private void btn_delete_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			var parentXmlFile = Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml");
			XDocument xdoc = XDocument.Parse(File.ReadAllText(parentXmlFile));

			foreach (var selected in listbox_PlaylistSource.SelectedItems)
			{

				string selected_playlist = selected.ToString();


				var playlist = PlaylistsSourceXdoc[selected_playlist].Root.Elements("Playlist").First();
				string playlist_id = (string)playlist.Element("PlaylistId");

				var nb = xdoc.Root.Elements("Parent").Where(p => (string)p.Element("PlaylistId") == playlist_id).Count();
				if (nb > 0)
				{
					foreach (var item in xdoc.Root.Elements("Parent").Where(p => (string)p.Element("PlaylistId") == playlist_id))
					{
						item.Remove();
					}
				}
				File.Delete(PlaylistsSourceFiles[selected_playlist]);
				list.Add(selected.ToString());

			}
			xdoc.Save(parentXmlFile);

			foreach (var selected in list)
			{
				listbox_PlaylistSource.Items.Remove(selected);
			}
		}

		private void btn_changeParentNode_Click(object sender, EventArgs e)
		{
			Form2 testDialog = new Form2();
			string newName = "";
			string newNameType = "";
			if (testDialog.ShowDialog(this) == DialogResult.OK)
			{
				newName = testDialog.res;
				newNameType = testDialog.type;
			}
			var parentXmlFile = Path.Combine(txt_xmlfolder.Text, "Data", "Parents.xml");
			XDocument xdoc = XDocument.Parse(File.ReadAllText(parentXmlFile));

			foreach (var selected in listbox_PlaylistSource.SelectedItems)
			{

				string selected_playlist = selected.ToString();

				var playlist = PlaylistsSourceXdoc[selected_playlist].Root.Elements("Playlist").First();
				string playlist_id = (string)playlist.Element("PlaylistId");

				var nb = xdoc.Root.Elements("Parent").Where(p => (string)p.Element("PlaylistId") == playlist_id).Count();
				if (nb > 0)
				{
					foreach (var item in xdoc.Root.Elements("Parent").Where(p => (string)p.Element("PlaylistId") == playlist_id))
					{

						item.Remove();
					}
				}


				XElement newNode = null;


				if (newNameType == "Category")
				{
					newNode = new XElement("Parent",
					new XElement("PlaylistId", playlist_id),
					new XElement("ParentPlatformCategoryName", newName)
					);

				}

				if (newNameType == "Platform")
				{
					newNode = new XElement("Parent",
					new XElement("PlaylistId", playlist_id),
					new XElement("ParentPlatformName", newName)
					);

				}

				if (newNameType == "Playlist")
				{
					newNode = new XElement("Parent",
					new XElement("PlaylistId", playlist_id),
					new XElement("ParentPlaylistId", newName)
					);

				}

				if (newNode != null) xdoc.Root.Add(newNode);

			}
			xdoc.Save(parentXmlFile);
		}
	}
}