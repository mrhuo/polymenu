using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MrHuo.PolyMenu
{
    /// <summary>
    /// 点击事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="index"></param>
    public delegate void PolygonMenuClickEvent(object sender, int index);

    /// <summary>
    /// 多边形菜单类
    /// 作者github：https://github.com/mrhuo
    /// </summary>
    public class PolygonMenu : PictureBox
    {
        /// <summary>
        /// 菜单点击事件处理
        /// </summary>
        public event PolygonMenuClickEvent OnMenuItemClicked;
        /// <summary>
        /// 调试：正常色块刷子
        /// </summary>
        private SolidBrush normalBlockBrush = null;
        /// <summary>
        /// 调试：鼠标滑过刷子
        /// </summary>
        private SolidBrush hoverBlockBrush = null;
        /// <summary>
        /// 外侧分割的多个多边形（梯形，如果不包括中间菜单为三角形）
        /// </summary>
        private Trapezoid[] trapezoids;
        /// <summary>
        /// 中间多边形顶点坐标
        /// </summary>
        private Point[] centerPolygonVertices;
        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        private Point? mouseHoverLocation = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PolygonMenu()
        {
            DoubleBuffered = true;
        }

        private int _PolygonGapSize = 20;
        [Browsable(true), Category("绘图"), Description("获取或设置一个值，表示中间多边形距离外侧多边形的边距，默认20")]
        public int PolygonGapSize
        {
            get
            {
                return _PolygonGapSize;
            }
            set
            {
                if (value < 0)
                {
                    _PolygonGapSize = 0;
                }
                else
                {
                    _PolygonGapSize = value;
                }
                this.ResetMenus();
            }
        }

        private int _SideNum = 3;
        [Browsable(true), Category("绘图"), Description("获取或设置一个值，表示多边形的边数，请确保 >= 3")]
        public int SideNum
        {
            get
            {
                return _SideNum;
            }
            set
            {
                if (value < 3)
                {
                    _SideNum = 3;
                }
                else
                {
                    _SideNum = value;
                }
                this.ResetMenus();
            }
        }

        private bool _ContainsCenterHole = true;
        [Browsable(true), Category("绘图"), Description("获取或设置一个值，表示是否需要绘制中间的菜单")]
        public bool HasCenterHole
        {
            get
            {
                return _ContainsCenterHole;
            }
            set
            {
                _ContainsCenterHole = value;
                this.ResetMenus();
            }
        }

        private Color _BlockColor = Color.FromArgb(50, Color.White);
        [Browsable(true), Category("绘图"), Description("获取或设置一个值，表示是菜单默认底色，默认为透明")]
        public Color BlockColor
        {
            get
            {
                return _BlockColor;
            }
            set
            {
                _BlockColor = value;
                normalBlockBrush = new SolidBrush(_BlockColor);
                this.ResetMenus();
            }
        }

        private Color _BlockHoverColor = Color.FromArgb(150, Color.White);
        [Browsable(true), Category("绘图"), Description("获取或设置一个值，表示是菜单鼠标滑过时的默认底色，默认为白色半透明")]
        public Color BlockHoverColor
        {
            get
            {
                return _BlockHoverColor;
            }
            set
            {
                _BlockHoverColor = value;
                hoverBlockBrush = new SolidBrush(_BlockHoverColor);
                this.ResetMenus();
            }
        }

        /// <summary>
        /// 重新设置菜单位置
        /// </summary>
        private void ResetMenus()
        {
            if (normalBlockBrush == null)
            {
                normalBlockBrush = new SolidBrush(BlockColor);
            }
            if (hoverBlockBrush == null)
            {
                hoverBlockBrush = new SolidBrush(BlockHoverColor);
            }

            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.Cursor = Cursors.Hand;
            var sideLength = this.Width / 2;
            var offset = this.Width / 2;

            //计算最外层多边形顶点位置
            var bigPolygon = CalculatePolygonVertices(SideNum, sideLength, offset);
            //计算中间多边形顶点位置，这里判断了如果不需要中间的按钮，则计算一个边长为0的坐标位置，用以计算每一块
            var smallPolygon = HasCenterHole ?
                CalculatePolygonVertices(SideNum, sideLength / 2, offset) :
                CalculatePolygonVertices(SideNum, 0, offset);

            //计算两个多边形相交之后，形成得多边形环，分割为多个等腰梯形，用以检测点击事件
            trapezoids = CalculateTrapezoids(bigPolygon, smallPolygon);

            //计算内部小的多边形，用以检测点击事件
            centerPolygonVertices = HasCenterHole ?
                CalculatePolygonVertices(SideNum, sideLength / 2 - PolygonGapSize, offset) :
                null;

            this.Refresh();
        }

        /// <summary>
        /// 刷新视图
        /// </summary>
        public void RefreshLayout()
        {
            this.ResetMenus();
        }

        /// <summary>
        /// 大小改变时自动绘制
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.Width != this.Height)
            {
                this.Height = this.Width;
            }
            this.ResetMenus();
        }

        /// <summary>
        /// 鼠标滑过的时候，重绘界面，然后设置鼠标位置
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mouseHoverLocation = e.Location;
            Invalidate();
        }

        /// <summary>
        /// 鼠标移除
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseHoverLocation = null;
            Invalidate();
        }

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            var mouseLocation = e.Location;
            if (centerPolygonVertices != null && centerPolygonVertices.Length > 0)
            {
                //检测鼠标是否在中间的六边形中：
                if (IsPointInPolygon(mouseLocation, centerPolygonVertices))
                {
                    OnMenuItemClicked?.Invoke(this, -1);
                    return;
                }
            }
            if (trapezoids != null && trapezoids.Length > 0)
            {
                //检测是否在某个梯形内部
                for (int i = 0; i < trapezoids.Length; i++)
                {
                    var trapezoid = trapezoids[i];
                    if (IsPointInTrapezoid(mouseLocation, trapezoid))
                    {
                        OnMenuItemClicked?.Invoke(this, i);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 重写绘图
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            var g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            if (HasCenterHole && centerPolygonVertices != null && centerPolygonVertices.Length > 0)
            {
                if (
                    mouseHoverLocation != null &&
                    IsPointInPolygon(mouseHoverLocation.Value, centerPolygonVertices))
                {
                    g.FillPolygon(hoverBlockBrush, centerPolygonVertices);
                }
                else
                {
                    g.FillPolygon(normalBlockBrush, centerPolygonVertices);
                }
            }
            if (trapezoids != null && trapezoids.Length > 0)
            {
                for (int i = 0; i < trapezoids.Length; i++)
                {
                    var trapezoid = trapezoids[i];
                    if (
                        mouseHoverLocation != null &&
                        IsPointInTrapezoid(mouseHoverLocation.Value, trapezoid))
                    {
                        g.FillPolygon(hoverBlockBrush, trapezoid.Points);
                    }
                    else
                    {
                        g.FillPolygon(normalBlockBrush, trapezoid.Points);
                    }
                }
            }
        }

        #region [静态工具方法]
        /// <summary>
        /// 计算每个梯形的坐标
        /// </summary>
        /// <param name="bigPolygoneVertices"></param>
        /// <param name="smallPolygonVertices"></param>
        /// <returns></returns>
        private static Trapezoid[] CalculateTrapezoids(Point[] bigPolygoneVertices, Point[] smallPolygonVertices)
        {
            var sideNum = bigPolygoneVertices.Length;
            if (sideNum <= 0)
            {
                return null;
            }
            var trapezoids = new Trapezoid[sideNum];
            for (int i = 0; i < sideNum; i++)
            {
                var topLeft = bigPolygoneVertices[i];
                var topRight = bigPolygoneVertices[(i + 1) % sideNum];
                var bottomLeft = smallPolygonVertices[i];
                var bottomRight = smallPolygonVertices[(i + 1) % sideNum];
                trapezoids[i] = new Trapezoid(topLeft, topRight, bottomLeft, bottomRight);
            }
            return trapezoids;
        }
        /// <summary>
        /// 根据多边形边数，边长长度，坐标点中心偏移量计算多边形顶点位置
        /// </summary>
        /// <param name="sideNum"></param>
        /// <param name="sideLength"></param>
        /// <param name="pointOffset"></param>
        /// <returns></returns>
        private static Point[] CalculatePolygonVertices(int sideNum, int sideLength, int pointOffset = 0)
        {
            var vertices = new Point[sideNum];
            var angle = 2 * Math.PI / sideNum;
            for (var i = 0; i < sideNum; i++)
            {
                var x = pointOffset + (int)(sideLength * Math.Cos(i * angle));
                var y = pointOffset + (int)(sideLength * Math.Sin(i * angle));
                vertices[i] = new Point(x, y);
            }
            return vertices;
        }
        /// <summary>
        /// 判断点是否在梯形内
        /// </summary>
        /// <param name="checkPoint"></param>
        /// <param name="trapezoid"></param>
        /// <returns></returns>
        private static bool IsPointInTrapezoid(Point checkPoint, Trapezoid trapezoid)
        {
            return IsPointInPolygon(checkPoint, trapezoid.Points);
        }
        /// <summary>
        /// 判断点是否在多边形内.
        /// 来源：https://blog.csdn.net/xxdddail/article/details/49093635
        /// ----------原理----------
        /// 注意到如果从P作水平向左的射线的话，如果P在多边形内部，那么这条射线与多边形的交点必为奇数，
        /// 如果P在多边形外部，则交点个数必为偶数(0也在内)。
        /// </summary>
        /// <param name="checkPoint">要判断的点</param>
        /// <param name="polygonPoints">多边形的顶点</param>
        /// <returns></returns>
        private static bool IsPointInPolygon(Point checkPoint, Point[] polygonPoints)
        {
            bool inside = false;
            int pointCount = polygonPoints.Length;
            Point p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...
            {
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if (checkPoint.Y < p2.Y)
                {//p2在射线之上
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。
                            //由于inside初始值为false，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，此时为inside=(!inside)
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为inside=(!inside)
                            inside = (!inside);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方，p1在射线上
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧
                    {
                        inside = (!inside);
                    }
                }
            }
            return inside;
        }
        #endregion

    }
}