using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I
{
    public partial class Form1 : Form
    {
        private Bitmap picture;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Size size = new Size(-1, -1);
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {

                var file = openFileDialog1.FileName;
                ThreadPool.QueueUserWorkItem(ReadAndWriteFileCallback, file);
            }
        }

        private void ReadAndWriteFileCallback(object state)
        {
            var file = (string) state;
            var size = new Size(-1, -1);
            try
            {

                picture = new Bitmap(file);
                size = picture.Size;
                pictureBox1.Image = picture;

            }
            catch (Exception exception)
            {
                Console.WriteLine("Could not load file");
            }

            Console.WriteLine("File Loaded: " + size);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
