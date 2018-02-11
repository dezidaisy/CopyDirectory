using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static string sourcePath;
        public static string targetPath;
 
        public Form1()
        {
            InitializeComponent();
        }
        void RFileCopy(string sourcePath, string targetPath)
        {
            string fileName;
            string destFile;

            if (!System.IO.Directory.Exists(targetPath))
            {
                //MessageBox.Show("Target Directory does not exist", "Error");

            }
            else
            {

                if (System.IO.Directory.Exists(sourcePath))
                {
                    string[] files = System.IO.Directory.GetFiles(sourcePath);

                    // Copy the files 
                    foreach (string s in files)
                    {

                        fileName = System.IO.Path.GetFileName(s);
                        destFile = System.IO.Path.Combine(targetPath, fileName);
                        if (!System.IO.File.Exists(destFile))
                        {
                            System.IO.File.Copy(s, destFile, false);
                            listBox1.Items.Add(s);
                            listBox1.Update();
                            listBox1.Refresh();


                            Application.DoEvents();
                        }
                        else
                        {
                           // MessageBox.Show("File " + destFile + " already exists!", "Error");
                        }


                    }
                }
                else
                {
                    //MessageBox.Show("Source path does not exist!", "Error");
                }
            }

            string[] subDirs = null;
            if (sourcePath != null)
            {
                subDirs = System.IO.Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
            }
            // Recursive call for each subdirectory.
            string fileName2;
            if (subDirs != null)
            {
                foreach (string s in subDirs)
                {
                    fileName2 = s.Remove(0, sourcePath.Length + 1);
                    string sourceFolder = System.IO.Path.Combine(sourcePath, fileName2);
                    string destFolder = System.IO.Path.Combine(targetPath, fileName2);

                    if (!System.IO.Directory.Exists(destFolder))
                    {
                        System.IO.Directory.CreateDirectory(destFolder);

                    }
                    RFileCopy(sourceFolder, destFolder);


                }
            }
            
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    sourcePath = fbd.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    targetPath = fbd.SelectedPath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RFileCopy(sourcePath,targetPath);
            listBox1.Items.Add("***Done***");
            listBox1.Update();
            listBox1.Refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
