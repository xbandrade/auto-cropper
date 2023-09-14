using System.Diagnostics;
using System.Drawing.Imaging;

namespace ImgAutoCropper
{
    public partial class AutoCropper : Form
    {
        private Image[] _imageToSave;
        public AutoCropper()
        {
            InitializeComponent();
            panel2.AllowDrop = true;
            panel2.DragEnter += PictureBoxDragEnter;
            panel2.DragDrop += PictureBoxDragDrop;
            _imageToSave = new Image[4];
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            int nonNullCount = _imageToSave.Count(image => image != null);
            if (nonNullCount == 0)
            {
                MessageBox.Show("No cropped image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label2.Text = "Select or drag and drop an image first";
                return;
            }
            using SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog.Title = "Save Cropped Images";
            saveFileDialog.FileName = "output.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string? selectedPath = Path.GetDirectoryName(saveFileDialog.FileName);
                if (selectedPath != null)
                {
                    string baseFileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                    string fileExtension = Path.GetExtension(saveFileDialog.FileName);
                    for (int i = 0; i < nonNullCount; ++i)
                    {
                        var imageToSave = _imageToSave[i];
                        if (imageToSave != null && (imageToSave.Width > 1 || imageToSave.Height > 1))
                        {
                            string fileName = $"{baseFileName}_{i + 1}{fileExtension}";
                            string filePath = Path.Combine(selectedPath, fileName);
                            ImageFormat imageFormat = ImageFormat.Png;
                            imageToSave.Save(filePath, imageFormat);
                        }
                    }
                    label2.Text = $"Saved!";
                }
            }
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            string msg = "Crop Border Removal:\n\nRemove unwanted borders and focus on the main content of an image.\n" +
                "Select or drag and drop up to four images, and the app will detect and remove any possible borders," +
                "leaving only the central content and eliminating the unnecessary border.\n";
            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Select or drag and drop an image first!");
                return;
            }
            DropDownBox.Visible = false;
            string[] files = textBox1.Text.Split(';');
            if (files.Length > 0)
            {
                LoadImages(files);
            }
        }

        private void button3_Click(object? sender, EventArgs e)
        {
            string url = "https://github.com/xbandrade/auto-cropper";
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

        private void PictureBoxDragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PictureBoxDragDrop(object? sender, DragEventArgs e)
        {
            Array.Clear(_imageToSave, 0, _imageToSave.Length);
            if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                LoadImages(files);
            }
        }

        private void LoadImages(string[] files)
        {
            string mergedPath = "";
            pictureBox1.Controls.Clear();
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            int numRows = 2;
            int numCols = 2;
            if (files.Length == 1)
            {
                numRows = 1;
                numCols = 1;
            }
            else if (files.Length == 2)
            {
                numRows = 1;
                numCols = 2;
            }
            else if (files.Length == 3)
            {
                numRows = 1;
                numCols = 3;
            } 
            int sectionWidth = pictureBox1.Width / numCols;
            int sectionHeight = pictureBox1.Height / numRows;
            for (int i = 0; i < Math.Min(4, files.Length); ++i)
            {
                string imagePath = files[i];
                string extension = Path.GetExtension(imagePath);
                if (IsImageFile(extension))
                {
                    Bitmap image;
                    try
                    {
                        image = new(imagePath);
                    }
                    catch (Exception ex)
                    {
                        label2.Text = $"Error loading image {i}: {ex.Message}";
                        continue;
                    }
                    PictureBox pictureBox = new()
                    {
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Width = sectionWidth,
                        Height = sectionHeight
                    };
                    int row = i / numCols;
                    int col = i % numCols;
                    int centerX = col * sectionWidth + sectionWidth / 2 - pictureBox.Width / 2;
                    int centerY = row * sectionHeight + sectionHeight / 2 - pictureBox.Height / 2;
                    pictureBox.Location = new Point(centerX, centerY);
                    (_imageToSave[i], Image imageToDisplay) = ImageCropper.CropImage(image, label2, (int)numericUpDown1.Value);
                    pictureBox.Image = imageToDisplay;
                    pictureBox1.Controls.Add(pictureBox);
                    mergedPath += imagePath + ';';
                }
            }
            DropDownBox.Visible = false;
            if (string.IsNullOrWhiteSpace(mergedPath))
            {
                label2.Text = "No valid image paths found!";
                return;
            }
            label2.Text = "Images loaded!";
            textBox1.Text = mergedPath[..^1];
        }

        private static bool IsImageFile(string extension)
        {
            string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            return supportedExtensions.Contains(extension.ToLower());
        }

        private void button5_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Select Files",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = true,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFilePaths = openFileDialog.FileNames;
                string selectedFiles = string.Join(";", selectedFilePaths.Take(4));
                textBox1.Text = selectedFiles;
            }
        }

        private void label2_Click(object? sender, EventArgs e)
        {

        }

        private void panel2_Paint(object? sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object? sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object? sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object? sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object? sender, EventArgs e)
        {

        }
        private void Form1_Load(object? sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object? sender, MouseEventArgs e)
        {

        }

        private void DropDownBox_Click(object sender, EventArgs e)
        {

        }
    }
}