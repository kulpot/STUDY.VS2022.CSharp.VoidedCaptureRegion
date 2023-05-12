using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace VoidedCaptureRegion
{
    public partial class Form1 : Form
    {
        private Point _lastPos;
        private bool _isVoid;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _lastPos = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Top += e.Y - _lastPos.Y;
                this.Left += e.X - _lastPos.X;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            using(GraphicsPath GP = new GraphicsPath())
            {
                if (_isVoid)
                {
                    GP.AddRectangle(this.ClientRectangle);
                    _isVoid = false;
                }
                else
                {
                    GP.AddRectangle(this.ClientRectangle);
                    GP.AddRectangle(panel.Bounds);
                    _isVoid = true;
                }
                
                this.Region = new Region(GP);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            using (Bitmap bitmap = new Bitmap(panel.Width, panel.Height))
            { 
                using (Graphics GFX = Graphics.FromImage(bitmap))
                {
                    GFX.CopyFromScreen(this.PointToScreen(panel.Location), Point.Empty, panel.Size);
                }

                bitmap.Save("hey.bmp");
                Process.Start("hey.bmp");
            }
        }
    }
}
