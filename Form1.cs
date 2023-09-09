using System.Diagnostics;
using System.Drawing.Imaging;

namespace ImgAutoCropper
{
    public partial class AutoCropper : Form
    {
        public AutoCropper()
        {
            InitializeComponent();
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += PictureBoxDragEnter;
            pictureBox1.DragDrop += PictureBoxDragDrop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image image = pictureBox1.Image;
            if (image != null && (image.Width > 1 || image.Height > 1))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    saveFileDialog.Title = "Save Cropped Image";
                    saveFileDialog.FileName = "output.png";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        ImageFormat imageFormat = ImageFormat.Png;
                        if (filePath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                        {
                            imageFormat = ImageFormat.Jpeg;
                        }
                        else if (filePath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                        {
                            imageFormat = ImageFormat.Bmp;
                        }
                        image.Save(filePath, imageFormat);
                    }
                }
                label2.Text = "Saved!";
            }
            else
            {
                MessageBox.Show("No cropped image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "Crop Border Removal:\n\nRemove unwanted borders and focus on the main content of an image.\n" +
                "Select an image, and the app will detect and remove any possible borders,\n" +
                "leaving only the central content and eliminating the unnecessary border.";
            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // label2.Text = "Image loaded!";
            // Image selectedImage = Image.FromFile(textBox1.Text);
            Image selectedImage = ImageCropper.CropImage(textBox1.Text, label2, (int)numericUpDown1.Value);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = selectedImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/xbandrade";
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBoxDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PictureBoxDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string imagePath = files[0];
                string extension = Path.GetExtension(imagePath);
                if (IsImageFile(extension))
                {
                    label2.Text = "Loading";
                    textBox1.Text = imagePath;
                    Image selectedImage = ImageCropper.CropImage(imagePath, label2, (int)numericUpDown1.Value);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = selectedImage;
                }
                else
                {
                    label2.Text = "Please drop an image file.";
                    MessageBox.Show("Please drop an image file.", "Error");
                }
            }
        }

        private bool IsImageFile(string extension)
        {
            string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            return supportedExtensions.Contains(extension.ToLower());
        }
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select File",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                textBox1.Text = selectedFilePath;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}