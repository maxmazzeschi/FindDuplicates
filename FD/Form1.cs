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

        string path = @"C:\Users\Massimiliano\SynoCloud\photo\Archivio fotografico";
		//string path = @"/run/user/1000/gvfs/smb-share:server=mdvstore,share=video/UsersTestArea/max/";
       // string path = @"Z:\UsersTestArea\max\test";

        void DirSearch(string sDir, ref List<FINFO> fileList)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (canBeDisplayed(f) == false)
                            continue;
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

        public class DirInfo
        {
            public int totalFiles;
            public int dupCount;
        }

        Dictionary<string, DirInfo> dirInfo = new Dictionary<string, DirInfo>();
        List<Item> dupList = new List<Item>();

        private void button1_Click(object sender, EventArgs e)
        {
            dirInfo = new Dictionary<string, DirInfo>();
            dupList = new List<Item>();
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
            dupList = new List<Item>();
            int step = (int)(fileList.Count / 100);
            if (step == 0)
                step = 1;
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
                        dupList.Add(item);
                        //Console.WriteLine("{0} {1}", key.fullPath, current.fullPath);
                        found++;
                    }

                }
                key = current;
            }

            button1.Parent.Text = "Arranging...";

            foreach (Item item in dupList)
            { 
                string dirPath;
                dirPath = System.IO.Path.GetDirectoryName(item.itemInfo.x.fullPath);
                if (dirInfo.ContainsKey(dirPath))
                {
                    DirInfo di = dirInfo[dirPath];
                    di.dupCount = di.dupCount + 1;
                    dirInfo[dirPath] = di;
                }
                else
                {
                    DirInfo di = new DirInfo();
                    di.dupCount = 1;
                    dirInfo[dirPath] = di;
                }
                dirPath = System.IO.Path.GetDirectoryName(item.itemInfo.y.fullPath);
                if (dirInfo.ContainsKey(dirPath))
                {
                    DirInfo di = dirInfo[dirPath];
                    di.dupCount = di.dupCount + 1;
                    dirInfo[dirPath] = di;
                }
                else
                {
                    DirInfo di = new DirInfo();
                    di.dupCount = 1;
                    dirInfo[dirPath] = di;
                }
            }
            Dictionary<string, DirInfo> tmp = new Dictionary<string, DirInfo>(dirInfo);
            foreach (string dirPath in tmp.Keys)
            {
                int count = 0;
                for (int i = 0; i < dupList.Count; i++)
                {
                    string dp;
                    dp = System.IO.Path.GetDirectoryName(dupList[i].itemInfo.x.fullPath);
                    if (dp == dirPath)
                        count++;
                    dp = System.IO.Path.GetDirectoryName(dupList[i].itemInfo.y.fullPath);
                    if (dp == dirPath)
                        count++;

                }
                DirInfo di = dirInfo[dirPath];
                di.totalFiles = count;
                dirInfo[dirPath] = di;
            }
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            //sw.Stop();
            //System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds.ToString());
            ReorderList();
            totalDup.Text = String.Format("Found {0} duplicates", dupList.Count.ToString());
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
                leftTaken.Text = exif.getValue("DateTimeOriginal");
				leftPBox.Load (info.x.fullPath);
                DirInfo di = dirInfo[System.IO.Path.GetDirectoryName(info.x.fullPath)];
                leftDupPercInFolder.Text = String.Format("{0} / {1} ", di.dupCount, di.totalFiles);
				} catch (Exception ex)
				{
					button1.Parent.Text =ex.Message;
				}
			}
            else
                leftPBox.Image = null;
			if (canBeDisplayed (info.y.fullPath)) {
                try
                {
                    ExifTagCollection exif = new ExifTagCollection(info.y.fullPath);
                    rightTaken.Text = exif.getValue("DateTimeOriginal");
                    rightPBox.Load(info.y.fullPath);
                    DirInfo di = dirInfo[System.IO.Path.GetDirectoryName(info.y.fullPath)];
                    rightDupPercInFolder.Text = String.Format("{0} / {1} ", di.dupCount, di.totalFiles);

                }
                catch (Exception ex)
                {
                    button1.Parent.Text = ex.Message;
                }
			}
            else
                rightPBox.Image = null;
            leftPath.Text = info.x.fullPath.Substring(path.Length);
            leftsize.Text = info.x.size.ToString();

            rightPath.Text = info.y.fullPath.Substring(path.Length);
            rightSize.Text = info.y.size.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReorderList();
        }

        public class DupListSort : IComparer<Item>
        {
            public enum Criteria{
                Unsorted,
                SizeAsc,
                SizeDesc,
                DirLeft,
                DirRight
            };
            Criteria c;
                
            public DupListSort(Criteria c)
            {
                this.c = c;
            }
            #region IComparer<Item> Members

            public int Compare(Item x, Item y)
            {
                switch (c)
                {
                    case Criteria.DirLeft: return System.IO.Path.GetDirectoryName(x.itemInfo.x.fullPath).CompareTo(System.IO.Path.GetDirectoryName(y.itemInfo.x.fullPath));
                    case Criteria.DirRight: return System.IO.Path.GetDirectoryName(x.itemInfo.y.fullPath).CompareTo(System.IO.Path.GetDirectoryName(y.itemInfo.y.fullPath));
                    case Criteria.SizeAsc: return x.itemInfo.x.size.CompareTo(y.itemInfo.x.size);
                    case Criteria.SizeDesc: return -(x.itemInfo.x.size.CompareTo(y.itemInfo.x.size));
                    case Criteria.Unsorted: return 0;               
                }
                return 0;
            }

            #endregion
        }
        private void ReorderList()
        {
            string selected = sortOption.SelectedText;
            DupListSort dls = null;
            if (selected == "Size Asc")
            {
                dls = new DupListSort(DupListSort.Criteria.SizeAsc);
            }
            if (selected == "Size Desc")
            {
                dls = new DupListSort(DupListSort.Criteria.SizeDesc);
            }
            if (selected == "Dir Left")
            {
                dls = new DupListSort(DupListSort.Criteria.DirLeft);
            }
            if (selected == "Dir Right")
            {
                dls = new DupListSort(DupListSort.Criteria.DirRight);
            }
            if ((selected == "") || (selected == "Unsorted"))
            {
                dls = new DupListSort(DupListSort.Criteria.Unsorted);
            }
            dupList.Sort(dls);
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(dupList.ToArray());
            listBox1.EndUpdate();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}