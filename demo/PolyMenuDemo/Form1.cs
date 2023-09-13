using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PolyMenuDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.polygonMenu.OnMenuItemClicked += (sender, index) =>
            {
                MessageBox.Show($"点击了 #{index}");
            };
        }

        private void selectForNumSide_ValueChanged(object sender, EventArgs e)
        {
            this.polygonMenu.SideNum = (int)selectForNumSide.Value;
        }

        private void selectForHasHole_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.polygonMenu.HasCenterHole = selectForHasHole.SelectedItem.ToString() == "true";
        }

        private void selectForGapSize_ValueChanged(object sender, EventArgs e)
        {
            this.polygonMenu.PolygonGapSize = (int)selectForGapSize.Value;
        }

        private void buttonForSelectBackImage_Click(object sender, EventArgs e)
        {
            var opd = new OpenFileDialog();
            if (opd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var image = Image.FromFile(opd.FileName);
                    this.polygonMenu.Image = image;
                    this.polygonMenu.RefreshLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("选取了错误的图片：\n\n" + ex.ToString());
                }
            }
        }

        private void colorDialogForNormalBlockColor_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.polygonMenu.BlockColor = colorDialog.Color;
                this.colorDialogForNormalBlockColor.Text = ColorToHexString(colorDialog.Color);
            }
        }

        private void colorForBlockHoverColor_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.polygonMenu.BlockHoverColor = colorDialog.Color;
                this.colorForBlockHoverColor.Text = ColorToHexString(colorDialog.Color);
            }
        }

        private static string ColorToHexString(Color color)
        {
            return $"#{Convert.ToString(color.A, 16)}{Convert.ToString(color.R, 16)}{Convert.ToString(color.G, 16)}{Convert.ToString(color.B, 16)}";
        }

        private void buttonForSmallerSize_Click(object sender, EventArgs e)
        {
            this.polygonMenu.Width -= 50;
        }

        private void buttonForBiggerSize_Click(object sender, EventArgs e)
        {
            this.polygonMenu.Width += 50;
        }

        private void buttonForSetBlockColorTransparent_Click(object sender, EventArgs e)
        {
            this.polygonMenu.BlockColor = Color.Transparent;
        }

        private void buttonForSetBlockHoverColorTransparent_Click(object sender, EventArgs e)
        {
            this.polygonMenu.BlockHoverColor = Color.Transparent;
        }

        private void buttonForClearBackImage_Click(object sender, EventArgs e)
        {
            this.polygonMenu.Image = null;
            this.polygonMenu.RefreshLayout();
        }
    }
}
