namespace Votations.NSurvey.Designer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Windows.Forms;

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class AutoFormat : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        public ListBox lbScheme;
        private PictureBox Preview;

        public AutoFormat()
        {
            this.InitializeComponent();
        }

        private void AutoFormat_Load(object sender, EventArgs e)
        {
            this.lbScheme.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.button2 = new Button();
            this.lbScheme = new ListBox();
            this.label1 = new Label();
            this.Preview = new PictureBox();
            this.label2 = new Label();
            base.SuspendLayout();
            this.button1.DialogResult = DialogResult.OK;
            this.button1.Location = new Point(0x88, 0x120);
            this.button1.Name = "button1";
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0xd8, 0x120);
            this.button2.Name = "button2";
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.Click += new EventHandler(this.button2_Click);
            this.lbScheme.Items.AddRange(new object[] { "Remove Auto Format", "Colorful 1", "Colorful 2", "Colorful 3", "Colorful 4", "Colorful 5", "Professional 1", "Professional 2", "Professional 3", "Simple 1", "Simple 2", "Simple 3", "Classic 1", "Classic 2" });
            this.lbScheme.Location = new Point(8, 0x20);
            this.lbScheme.Name = "lbScheme";
            this.lbScheme.Size = new Size(120, 0xee);
            this.lbScheme.TabIndex = 1;
            this.lbScheme.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 0x10);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select a scheme:";
            this.Preview.BorderStyle = BorderStyle.Fixed3D;
            this.Preview.Location = new Point(0x88, 0x20);
            this.Preview.Name = "Preview";
            this.Preview.Size = new Size(0x98, 240);
            this.Preview.TabIndex = 4;
            this.Preview.TabStop = false;
            this.Preview.Click += new EventHandler(this.Preview_Click);
            this.label2.Location = new Point(0x88, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x10);
            this.label2.TabIndex = 5;
            this.label2.Text = "Preview:";
//            base.AutoScale = false;
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None ;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x132, 0x147);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.Preview);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lbScheme);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AutoFormat";
            base.ShowInTaskbar = false;
            this.Text = "Auto Format";
            base.Load += new EventHandler(this.AutoFormat_Load);
            base.ResumeLayout(false);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            switch (this.lbScheme.SelectedIndex)
            {
                case 1:
                    str = "ColorFul1.bmp";
                    break;

                case 2:
                    str = "ColorFul2.bmp";
                    break;

                case 3:
                    str = "ColorFul3.bmp";
                    break;

                case 4:
                    str = "ColorFul4.bmp";
                    break;

                case 5:
                    str = "ColorFul5.bmp";
                    break;

                case 6:
                    str = "Professional1.bmp";
                    break;

                case 7:
                    str = "Professional2.bmp";
                    break;

                case 8:
                    str = "Professional3.bmp";
                    break;

                case 9:
                    str = "Simple1.bmp";
                    break;

                case 10:
                    str = "Simple2.bmp";
                    break;

                case 11:
                    str = "Simple3.bmp";
                    break;

                case 12:
                    str = "Classic1.bmp";
                    break;

                case 13:
                    str = "Classic2.bmp";
                    break;

                default:
                    str = "default.bmp";
                    break;
            }
            new ResourceManager("items", Assembly.GetExecutingAssembly());
            Stream manifestResourceStream = Assembly.GetAssembly(base.GetType()).GetManifestResourceStream("Votations.NSurvey.Bitmaps." + str);
            this.Preview.Image = new Bitmap(manifestResourceStream);
            manifestResourceStream.Close();
        }

        private void Preview_Click(object sender, EventArgs e)
        {
        }
    }
}

