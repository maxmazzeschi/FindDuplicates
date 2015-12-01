using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LevDan.Exif;

namespace FD
{
    public partial class TheForm : Form
    {
        public TheForm()
        {
            InitializeComponent();
        }

        public class FINFO
        {
            public string fullPath;
            public long size;
        }

        //string path = @"C:\Users\Massimiliano\SynoCloud\photo\Archivio fotografico";
		string path = @"/run/user/1000/gvfs/smb-share:server=mdvstore,share=video/UsersTestArea/max/";


        static void DirSearch(string sDir, ref List<FINFO> fileList)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        FINFO finfo = new FINFO();
                        finfo.fullPath = f;
                        finfo.size = new FileInfo(f).Length;
                        fileList.Add(finfo);
                    }
                    DirSearch(d, ref fileList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public class ItemInfo
        {
            public FINFO x;
            public FINFO y;
        }

        public class Item
        {
            private string _text;

            public string text
            {
                get { return _text; }
                set { _text = value; }
            }
	        private ItemInfo _itemInfo;

	        public ItemInfo itemInfo
	        {
		        get { return _itemInfo;}
		        set { _itemInfo = value;}
	        }
	
            public Item(string text, ItemInfo itemInfo)
            {
                this._text = text;
                this._itemInfo = itemInfo;
            }
        }


        public class FINFOComparer : IComparer<FINFO>
        {
            #region IComparer<FINFO> Members

            public int Compare(FINFO x, FINFO y)
            {
                return x.size.CompareTo(y.size);
            }

            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Parent.Text = "Loading file list";
            List<FINFO> fileList = new List<FINFO>();
            DirSearch(path, ref fileList);
            Console.WriteLine("Sorting");
            fileList.Sort(new FINFOComparer());
            button1.Parent.Text = "Checking";

            FINFO key = fileList[0];
            int found = 0;
            listBox1.ValueMember = "itemInfo";
            listBox1.DisplayMember = "text";
            List<Item> tmp = new List<Item>();
            int step = (int)(fileList.Count / 100);
            for (int i = 1; i < fileList.Count; i++)
            {
                if ((i % step) == 0)
                {
                    button1.Parent.Text = "Checking " + (i / (double)step).ToString() + "%";
                }
                FINFO current = fileList[i];
                if (key.size == current.size)
                {
                    string keyName = System.IO.Path.GetFileName(key.fullPath).ToUpper();
                    string currentName = System.IO.Path.GetFileName(current.fullPath).ToUpper();
                    if (keyName == currentName)
                    {
                        ItemInfo ii = new ItemInfo();
                        ii.x = key;
                        ii.y = current;
                        Item item = new Item(keyName, ii);
                        tmp.Add(item);
                        //Console.WriteLine("{0} {1}", key.fullPath, current.fullPath);
                        found++;
                    }

                }
                key = current;
            }
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            listBox1.BeginUpdate();
            listBox1.Items.AddRange(tmp.ToArray());
            listBox1.EndUpdate();
            //sw.Stop();
            //System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds.ToString());

            button1.Parent.Text = "Done";
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            
        }

        bool canBeDisplayed(string fullPath)
        {
            string[] allowedExt = { ".GIF", ".BMP", ".JPG", ".TIF", ".TIFF" };
            string ext = System.IO.Path.GetExtension(fullPath).ToUpper();
            return Array.Exists<string>(allowedExt, delegate (string value) { return ext == value; });
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ItemInfo info = ((Item)((ListBox)sender).SelectedItem).itemInfo;
			if (canBeDisplayed (info.x.fullPath)) {
				try {
				ExifTagCollection exif = new ExifTagCollection (info.x.fullPath);
				leftPBox.Load (info.x.fullPath);
				} catch (Exception ex)
				{
					button1.Parent.Text =ex.Message;
				}
			}
            else
                leftPBox.Image = null;
			if (canBeDisplayed (info.y.fullPath)) {
				ExifTagCollection exif = new ExifTagCollection (info.y.fullPath);
				rightPBox.Load (info.y.fullPath);
			}
            else
                rightPBox.Image = null;
            leftPath.Text = info.x.fullPath.Substring(path.Length);
            rightPath.Text = info.y.fullPath.Substring(path.Length);
        }
    }
}