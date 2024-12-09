using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.CustomControl
{
    public partial class SidebarButton : UserControl
    {
      //  [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
      //  private static extern IntPtr CreateRoundRectRgn
      //(
      //    int nLeftRect,     // x-coordinate of upper-left corner
      //    int nTopRect,      // y-coordinate of upper-left corner
      //    int nRightRect,    // x-coordinate of lower-right corner
      //    int nBottomRect,   // y-coordinate of lower-right corner
      //    int nWidthEllipse, // height of ellipse
      //    int nHeightEllipse // width of ellipse
      //);
        public SidebarButton()
        {
            InitializeComponent();
            btnSide.FlatStyle = FlatStyle.Flat;
            btnSide.FlatAppearance.BorderSize = 0;
            btnSide.Paint += btnSide_Paint;

            //this.BorderStyle = BorderStyle.None;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            //btnSide.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public EventHandler buttonClicked;
        public string lblButtonModName;
        public string lblBtnName;
        public string lblBtnModId;
        public string ButtonName
        {
            get { return btnSide.Text; }
            set { btnSide.Text = value; }
        }
        public string ButtonModId
        {
            get { return lblBtnModId ?? ""; }
            set { lblBtnModId = value; }
        }
        private void btnSide_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnSide.ClientRectangle,
            Color.FromArgb(34, 112, 173), 1, ButtonBorderStyle.Outset,
            Color.FromArgb(34, 112, 173), 1, ButtonBorderStyle.Outset,
            Color.FromArgb(34, 112, 173), 1, ButtonBorderStyle.Outset,
            Color.FromArgb(34, 112, 173), 1, ButtonBorderStyle.Outset);
        }
        public void btnSide_Click(object sender, EventArgs e)
        {
            if (buttonClicked != null)
            {
                buttonClicked(this, EventArgs.Empty);
                this.panelHighlight.BackColor = Color.FromArgb(255, 213, 39);
                this.btnSide.BackColor = Color.FromArgb(34, 112, 173);
                // Revert the background color of the previously-colored button, if any
                if (MdlMain.lastButtonClicked != null && MdlMain.lastButtonName != MdlMain.CurrButtonName)
                    MdlMain.lastButtonClicked.BackColor = Color.FromArgb(34, 112, 173);
                // Update the previously-colored button
                MdlMain.lastButtonClicked = this.panelHighlight;
                MdlMain.lastButtonName = MdlMain.CurrButtonName;
            }
        }

    }
}
