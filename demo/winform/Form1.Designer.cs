
using MrHuo.PolyMenu;

namespace PolyMenuDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonForClearBackImage = new System.Windows.Forms.Button();
            this.buttonForSetBlockHoverColorTransparent = new System.Windows.Forms.Button();
            this.buttonForSetBlockColorTransparent = new System.Windows.Forms.Button();
            this.buttonForBiggerSize = new System.Windows.Forms.Button();
            this.buttonForSmallerSize = new System.Windows.Forms.Button();
            this.labelForControlSize = new System.Windows.Forms.Label();
            this.colorForBlockHoverColor = new System.Windows.Forms.Label();
            this.labelForBlockHoverColor = new System.Windows.Forms.Label();
            this.colorDialogForNormalBlockColor = new System.Windows.Forms.Label();
            this.labelForNormalBlockColor = new System.Windows.Forms.Label();
            this.buttonForSelectBackImage = new System.Windows.Forms.Button();
            this.labelForBackImage = new System.Windows.Forms.Label();
            this.labelForGapSize = new System.Windows.Forms.Label();
            this.selectForHasHole = new System.Windows.Forms.ComboBox();
            this.selectForGapSize = new System.Windows.Forms.NumericUpDown();
            this.labelForHasHole = new System.Windows.Forms.Label();
            this.labelForSideNum = new System.Windows.Forms.Label();
            this.selectForNumSide = new System.Windows.Forms.NumericUpDown();
            this.polygonMenu = new MrHuo.PolyMenu.PolygonMenu();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectForGapSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectForNumSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.polygonMenu);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1195, 684);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "演示区";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonForClearBackImage);
            this.groupBox1.Controls.Add(this.buttonForSetBlockHoverColorTransparent);
            this.groupBox1.Controls.Add(this.buttonForSetBlockColorTransparent);
            this.groupBox1.Controls.Add(this.buttonForBiggerSize);
            this.groupBox1.Controls.Add(this.buttonForSmallerSize);
            this.groupBox1.Controls.Add(this.labelForControlSize);
            this.groupBox1.Controls.Add(this.colorForBlockHoverColor);
            this.groupBox1.Controls.Add(this.labelForBlockHoverColor);
            this.groupBox1.Controls.Add(this.colorDialogForNormalBlockColor);
            this.groupBox1.Controls.Add(this.labelForNormalBlockColor);
            this.groupBox1.Controls.Add(this.buttonForSelectBackImage);
            this.groupBox1.Controls.Add(this.labelForBackImage);
            this.groupBox1.Controls.Add(this.labelForGapSize);
            this.groupBox1.Controls.Add(this.selectForHasHole);
            this.groupBox1.Controls.Add(this.selectForGapSize);
            this.groupBox1.Controls.Add(this.labelForHasHole);
            this.groupBox1.Controls.Add(this.labelForSideNum);
            this.groupBox1.Controls.Add(this.selectForNumSide);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(911, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 684);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置区";
            // 
            // buttonForClearBackImage
            // 
            this.buttonForClearBackImage.Location = new System.Drawing.Point(210, 262);
            this.buttonForClearBackImage.Name = "buttonForClearBackImage";
            this.buttonForClearBackImage.Size = new System.Drawing.Size(64, 28);
            this.buttonForClearBackImage.TabIndex = 26;
            this.buttonForClearBackImage.Text = "无";
            this.buttonForClearBackImage.UseVisualStyleBackColor = true;
            this.buttonForClearBackImage.Click += new System.EventHandler(this.buttonForClearBackImage_Click);
            // 
            // buttonForSetBlockHoverColorTransparent
            // 
            this.buttonForSetBlockHoverColorTransparent.Location = new System.Drawing.Point(210, 414);
            this.buttonForSetBlockHoverColorTransparent.Name = "buttonForSetBlockHoverColorTransparent";
            this.buttonForSetBlockHoverColorTransparent.Size = new System.Drawing.Size(64, 28);
            this.buttonForSetBlockHoverColorTransparent.TabIndex = 25;
            this.buttonForSetBlockHoverColorTransparent.Text = "透明";
            this.buttonForSetBlockHoverColorTransparent.UseVisualStyleBackColor = true;
            this.buttonForSetBlockHoverColorTransparent.Click += new System.EventHandler(this.buttonForSetBlockHoverColorTransparent_Click);
            // 
            // buttonForSetBlockColorTransparent
            // 
            this.buttonForSetBlockColorTransparent.Location = new System.Drawing.Point(210, 333);
            this.buttonForSetBlockColorTransparent.Name = "buttonForSetBlockColorTransparent";
            this.buttonForSetBlockColorTransparent.Size = new System.Drawing.Size(64, 28);
            this.buttonForSetBlockColorTransparent.TabIndex = 24;
            this.buttonForSetBlockColorTransparent.Text = "透明";
            this.buttonForSetBlockColorTransparent.UseVisualStyleBackColor = true;
            this.buttonForSetBlockColorTransparent.Click += new System.EventHandler(this.buttonForSetBlockColorTransparent_Click);
            // 
            // buttonForBiggerSize
            // 
            this.buttonForBiggerSize.Location = new System.Drawing.Point(199, 489);
            this.buttonForBiggerSize.Name = "buttonForBiggerSize";
            this.buttonForBiggerSize.Size = new System.Drawing.Size(75, 23);
            this.buttonForBiggerSize.TabIndex = 23;
            this.buttonForBiggerSize.Text = "+50";
            this.buttonForBiggerSize.UseVisualStyleBackColor = true;
            this.buttonForBiggerSize.Click += new System.EventHandler(this.buttonForBiggerSize_Click);
            // 
            // buttonForSmallerSize
            // 
            this.buttonForSmallerSize.Location = new System.Drawing.Point(20, 489);
            this.buttonForSmallerSize.Name = "buttonForSmallerSize";
            this.buttonForSmallerSize.Size = new System.Drawing.Size(75, 23);
            this.buttonForSmallerSize.TabIndex = 22;
            this.buttonForSmallerSize.Text = "-50";
            this.buttonForSmallerSize.UseVisualStyleBackColor = true;
            this.buttonForSmallerSize.Click += new System.EventHandler(this.buttonForSmallerSize_Click);
            // 
            // labelForControlSize
            // 
            this.labelForControlSize.AutoSize = true;
            this.labelForControlSize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForControlSize.Location = new System.Drawing.Point(17, 468);
            this.labelForControlSize.Name = "labelForControlSize";
            this.labelForControlSize.Size = new System.Drawing.Size(87, 15);
            this.labelForControlSize.TabIndex = 21;
            this.labelForControlSize.Text = "控件大小：";
            // 
            // colorForBlockHoverColor
            // 
            this.colorForBlockHoverColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorForBlockHoverColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorForBlockHoverColor.Location = new System.Drawing.Point(17, 416);
            this.colorForBlockHoverColor.Name = "colorForBlockHoverColor";
            this.colorForBlockHoverColor.Size = new System.Drawing.Size(176, 24);
            this.colorForBlockHoverColor.TabIndex = 20;
            this.colorForBlockHoverColor.Text = "#96ffffff";
            this.colorForBlockHoverColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorForBlockHoverColor.Click += new System.EventHandler(this.colorForBlockHoverColor_Click);
            // 
            // labelForBlockHoverColor
            // 
            this.labelForBlockHoverColor.AutoSize = true;
            this.labelForBlockHoverColor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForBlockHoverColor.Location = new System.Drawing.Point(17, 390);
            this.labelForBlockHoverColor.Name = "labelForBlockHoverColor";
            this.labelForBlockHoverColor.Size = new System.Drawing.Size(151, 15);
            this.labelForBlockHoverColor.TabIndex = 19;
            this.labelForBlockHoverColor.Text = "鼠标滑过色块颜色：";
            // 
            // colorDialogForNormalBlockColor
            // 
            this.colorDialogForNormalBlockColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorDialogForNormalBlockColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorDialogForNormalBlockColor.Location = new System.Drawing.Point(17, 335);
            this.colorDialogForNormalBlockColor.Name = "colorDialogForNormalBlockColor";
            this.colorDialogForNormalBlockColor.Size = new System.Drawing.Size(176, 24);
            this.colorDialogForNormalBlockColor.TabIndex = 18;
            this.colorDialogForNormalBlockColor.Text = "#ffff6347";
            this.colorDialogForNormalBlockColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorDialogForNormalBlockColor.Click += new System.EventHandler(this.colorDialogForNormalBlockColor_Click);
            // 
            // labelForNormalBlockColor
            // 
            this.labelForNormalBlockColor.AutoSize = true;
            this.labelForNormalBlockColor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForNormalBlockColor.Location = new System.Drawing.Point(17, 311);
            this.labelForNormalBlockColor.Name = "labelForNormalBlockColor";
            this.labelForNormalBlockColor.Size = new System.Drawing.Size(119, 15);
            this.labelForNormalBlockColor.TabIndex = 17;
            this.labelForNormalBlockColor.Text = "默认色块颜色：";
            // 
            // buttonForSelectBackImage
            // 
            this.buttonForSelectBackImage.Location = new System.Drawing.Point(20, 265);
            this.buttonForSelectBackImage.Name = "buttonForSelectBackImage";
            this.buttonForSelectBackImage.Size = new System.Drawing.Size(173, 23);
            this.buttonForSelectBackImage.TabIndex = 16;
            this.buttonForSelectBackImage.Text = "选择图片";
            this.buttonForSelectBackImage.UseVisualStyleBackColor = true;
            this.buttonForSelectBackImage.Click += new System.EventHandler(this.buttonForSelectBackImage_Click);
            // 
            // labelForBackImage
            // 
            this.labelForBackImage.AutoSize = true;
            this.labelForBackImage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForBackImage.Location = new System.Drawing.Point(17, 244);
            this.labelForBackImage.Name = "labelForBackImage";
            this.labelForBackImage.Size = new System.Drawing.Size(119, 15);
            this.labelForBackImage.TabIndex = 15;
            this.labelForBackImage.Text = "选择背景图片：";
            // 
            // labelForGapSize
            // 
            this.labelForGapSize.AutoSize = true;
            this.labelForGapSize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForGapSize.Location = new System.Drawing.Point(17, 173);
            this.labelForGapSize.Name = "labelForGapSize";
            this.labelForGapSize.Size = new System.Drawing.Size(87, 15);
            this.labelForGapSize.TabIndex = 14;
            this.labelForGapSize.Text = "间隙大小：";
            // 
            // selectForHasHole
            // 
            this.selectForHasHole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectForHasHole.FormattingEnabled = true;
            this.selectForHasHole.Items.AddRange(new object[] {
            "true",
            "false"});
            this.selectForHasHole.Location = new System.Drawing.Point(20, 126);
            this.selectForHasHole.Name = "selectForHasHole";
            this.selectForHasHole.Size = new System.Drawing.Size(254, 23);
            this.selectForHasHole.TabIndex = 13;
            this.selectForHasHole.SelectedIndexChanged += new System.EventHandler(this.selectForHasHole_SelectedIndexChanged);
            // 
            // selectForGapSize
            // 
            this.selectForGapSize.Location = new System.Drawing.Point(20, 194);
            this.selectForGapSize.Name = "selectForGapSize";
            this.selectForGapSize.Size = new System.Drawing.Size(254, 25);
            this.selectForGapSize.TabIndex = 12;
            this.selectForGapSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.selectForGapSize.ValueChanged += new System.EventHandler(this.selectForGapSize_ValueChanged);
            // 
            // labelForHasHole
            // 
            this.labelForHasHole.AutoSize = true;
            this.labelForHasHole.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForHasHole.Location = new System.Drawing.Point(17, 105);
            this.labelForHasHole.Name = "labelForHasHole";
            this.labelForHasHole.Size = new System.Drawing.Size(119, 15);
            this.labelForHasHole.TabIndex = 11;
            this.labelForHasHole.Text = "是否中间开洞：";
            // 
            // labelForSideNum
            // 
            this.labelForSideNum.AutoSize = true;
            this.labelForSideNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForSideNum.Location = new System.Drawing.Point(17, 37);
            this.labelForSideNum.Name = "labelForSideNum";
            this.labelForSideNum.Size = new System.Drawing.Size(150, 15);
            this.labelForSideNum.TabIndex = 10;
            this.labelForSideNum.Text = "边数：(必须 >=3 )";
            // 
            // selectForNumSide
            // 
            this.selectForNumSide.AutoSize = true;
            this.selectForNumSide.Location = new System.Drawing.Point(20, 58);
            this.selectForNumSide.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.selectForNumSide.Name = "selectForNumSide";
            this.selectForNumSide.Size = new System.Drawing.Size(254, 25);
            this.selectForNumSide.TabIndex = 9;
            this.selectForNumSide.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.selectForNumSide.ValueChanged += new System.EventHandler(this.selectForNumSide_ValueChanged);
            // 
            // polygonMenu
            // 
            this.polygonMenu.BackColor = System.Drawing.Color.Tomato;
            this.polygonMenu.BlockColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.polygonMenu.BlockHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.polygonMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.polygonMenu.HasCenterHole = true;
            this.polygonMenu.Location = new System.Drawing.Point(12, 24);
            this.polygonMenu.Name = "polygonMenu";
            this.polygonMenu.PolygonGapSize = 20;
            this.polygonMenu.SideNum = 8;
            this.polygonMenu.Size = new System.Drawing.Size(654, 654);
            this.polygonMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.polygonMenu.TabIndex = 5;
            this.polygonMenu.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 704);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "PolygonMenu Demo";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectForGapSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectForNumSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private PolygonMenu polygonMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonForSelectBackImage;
        private System.Windows.Forms.Label labelForBackImage;
        private System.Windows.Forms.Label labelForGapSize;
        private System.Windows.Forms.ComboBox selectForHasHole;
        private System.Windows.Forms.NumericUpDown selectForGapSize;
        private System.Windows.Forms.Label labelForHasHole;
        private System.Windows.Forms.Label labelForSideNum;
        private System.Windows.Forms.NumericUpDown selectForNumSide;
        private System.Windows.Forms.Label colorDialogForNormalBlockColor;
        private System.Windows.Forms.Label labelForNormalBlockColor;
        private System.Windows.Forms.Label colorForBlockHoverColor;
        private System.Windows.Forms.Label labelForBlockHoverColor;
        private System.Windows.Forms.Button buttonForBiggerSize;
        private System.Windows.Forms.Button buttonForSmallerSize;
        private System.Windows.Forms.Label labelForControlSize;
        private System.Windows.Forms.Button buttonForSetBlockHoverColorTransparent;
        private System.Windows.Forms.Button buttonForSetBlockColorTransparent;
        private System.Windows.Forms.Button buttonForClearBackImage;
    }
}

