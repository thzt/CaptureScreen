using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region field

        private KeyboardHook keyboardHook = new KeyboardHook();

        private string folderPath;

        #endregion

        #region event

        private void EnableCapture_Click(object sender, EventArgs e)
        {
            keyboardHook.SetHook();
            keyboardHook.OnKeyDownEvent += KeyboardHook_OnKeyDownEvent;

            EnableCapture.Enabled = false;
            DisableCapture.Enabled = true;
            SelectFolder.Enabled = false;
            FolderPath.Enabled = false;

            this.WindowState = FormWindowState.Minimized;

            folderPath = Path.Combine(FolderPath.Text, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            CreateFolderIfNotExist(folderPath);
        }

        private void DisableCapture_Click(object sender, EventArgs e)
        {
            keyboardHook.OnKeyDownEvent -= KeyboardHook_OnKeyDownEvent;
            keyboardHook.UnHook();

            EnableCapture.Enabled = true;
            DisableCapture.Enabled = false;
            SelectFolder.Enabled = true;
            FolderPath.Enabled = true;
        }

        private void SelectFolder_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            string folderPath = dialog.SelectedPath;
            FolderPath.Text = folderPath;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            keyboardHook.OnKeyDownEvent -= KeyboardHook_OnKeyDownEvent;
            keyboardHook.UnHook();
        }

        #endregion

        #region business

        private void CreateFolderIfNotExist(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                return;
            }

            Directory.CreateDirectory(folderPath);
        }

        private void KeyboardHook_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            var isUserCopy = willCopy(e);
            if (!isUserCopy)
            {
                return;
            }

            var imageFullPath = Path.Combine(folderPath, string.Format("{0}.jpeg", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")));
            CaptureFullScreenToFile(e, imageFullPath);
        }

        private void CaptureFullScreenToFile(KeyEventArgs e, string filePath)
        {
            var isUserWillCopy = willCopy(e);
            if (!isUserWillCopy)
            {
                return;
            }

            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;

            var image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(width, height));

            image.Save(filePath);
        }

        private bool willCopy(KeyEventArgs e)
        {
            return e.Control && e.KeyCode == Keys.PrintScreen;
        }

        #endregion
    }
}
