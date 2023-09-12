namespace ImgAutoCropper
{
    partial class AutoCropper
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
            components = new System.ComponentModel.Container();
            notifyIcon1 = new NotifyIcon(components);
            SaveButton = new Button();
            panel1 = new Panel();
            label3 = new Label();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            button5 = new Button();
            LoadButton = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Roboto Slab ExtraBold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            SaveButton.Location = new Point(30, 668);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(149, 55);
            SaveButton.TabIndex = 0;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gray;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(numericUpDown1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(LoadButton);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(SaveButton);
            panel1.Location = new Point(14, 13);
            panel1.Name = "panel1";
            panel1.Size = new Size(1137, 751);
            panel1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto Slab SemiBold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(909, 41);
            label3.Name = "label3";
            label3.Size = new Size(123, 31);
            label3.TabIndex = 9;
            label3.Text = "Tolerance";
            label3.Click += label3_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(1038, 44);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(60, 27);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.DarkGray;
            label2.Font = new Font("Roboto Slab ExtraBold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(538, 668);
            label2.Name = "label2";
            label2.Size = new Size(0, 24);
            label2.TabIndex = 8;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Roboto Slab Medium", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Image = global::AutoCropper.Properties.Resources.search1;
            button5.Location = new Point(724, 41);
            button5.Name = "button5";
            button5.Size = new Size(34, 30);
            button5.TabIndex = 7;
            button5.TextAlign = ContentAlignment.TopCenter;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // LoadButton
            // 
            LoadButton.Font = new Font("Roboto Slab Medium", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LoadButton.Location = new Point(764, 41);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(106, 30);
            LoadButton.TabIndex = 6;
            LoadButton.Text = "Load";
            LoadButton.TextAlign = ContentAlignment.TopCenter;
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkGray;
            panel2.BackgroundImage = global::AutoCropper.Properties.Resources.dropdown;
            panel2.BackgroundImageLayout = ImageLayout.Center;
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(30, 110);
            panel2.Name = "panel2";
            panel2.Size = new Size(1079, 540);
            panel2.TabIndex = 5;
            panel2.Paint += panel2_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(29, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1021, 486);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto Slab SemiBold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(15, 39);
            label1.Name = "label1";
            label1.Size = new Size(111, 31);
            label1.TabIndex = 4;
            label1.Text = "File Path";
            // 
            // button2
            // 
            button2.Font = new Font("Roboto Slab ExtraBold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Image = global::AutoCropper.Properties.Resources.info;
            button2.Location = new Point(1003, 677);
            button2.Name = "button2";
            button2.Size = new Size(47, 46);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Roboto Slab ExtraBold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Image = global::AutoCropper.Properties.Resources.github;
            button3.Location = new Point(1062, 677);
            button3.Name = "button3";
            button3.Size = new Size(47, 46);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(154, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(564, 27);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // AutoCropper
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(1163, 776);
            Controls.Add(panel1);
            Font = new Font("Roboto Slab SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AutoCropper";
            Text = "AutoCropper";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private Button SaveButton;
        private Panel panel1;
        private Button button2;
        private Button button3;
        private TextBox textBox1;
        private Button LoadButton;
        private Panel panel2;
        private Label label1;
        private PictureBox pictureBox1;
        private Button button5;
        private Label label2;
        private NumericUpDown numericUpDown1;
        private Label label3;
    }
}