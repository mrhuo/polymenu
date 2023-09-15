using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MrHuo.PolyMenu.Wpf
{
    /// <summary>
    /// 点击事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="index"></param>
    public delegate void PolygonMenuClickEvent(object sender, int index);

    public class PolygonMenu : Canvas
    {
        //如6边形的话，点击事件中的 index 0-6 每个梯形位置，-1 中间位置
        //          4 
        //     3         5
        //         -1
        //     2         0
        //          1

        /// <summary>
        /// 菜单点击事件处理
        /// </summary>
        public event PolygonMenuClickEvent OnMenuItemClicked;

        #region [依赖属性：SideNum, HasCenterHole, BlockColor, BlockHoverColor, PolygonGapSize, BackgroundImage, HoleSize]
        public int SideNum
        {
            get { return (int)GetValue(SideNumProperty); }
            set
            {
                SetValue(SideNumProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty SideNumProperty =
            DependencyProperty.Register("SideNum", typeof(int), typeof(PolygonMenu), new PropertyMetadata(3));

        public bool HasCenterHole
        {
            get { return (bool)GetValue(HasCenterHoleProperty); }
            set
            {
                SetValue(HasCenterHoleProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty HasCenterHoleProperty =
            DependencyProperty.Register("HasCenterHole", typeof(bool), typeof(PolygonMenu), new PropertyMetadata(true));

        public Color BlockColor
        {
            get { return (Color)GetValue(BlockColorProperty); }
            set
            {
                SetValue(BlockColorProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty BlockColorProperty =
            DependencyProperty.Register("BlockColor", typeof(Color), typeof(PolygonMenu), new PropertyMetadata(Color.FromArgb(50, 255, 255, 255)));

        public Color BlockHoverColor
        {
            get { return (Color)GetValue(BlockHoverColorProperty); }
            set
            {
                SetValue(BlockHoverColorProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty BlockHoverColorProperty =
            DependencyProperty.Register("BlockHoverColor", typeof(Color), typeof(PolygonMenu), new PropertyMetadata(Color.FromArgb(150, 255, 255, 255)));

        public int PolygonGapSize
        {
            get { return (int)GetValue(PolygonGapSizeProperty); }
            set
            {
                SetValue(PolygonGapSizeProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty PolygonGapSizeProperty =
            DependencyProperty.Register("PolygonGapSize", typeof(int), typeof(PolygonMenu), new PropertyMetadata(20));

        public ImageSource BackgroundImage
        {
            get
            {
                return (ImageSource)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("BackgroundImage", typeof(ImageSource), typeof(PolygonMenu), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnSourceChanged, null), null);

        /// <summary>
        /// 中间多边形洞大小
        /// </summary>
        public int? HoleSize
        {
            get { return (int)GetValue(HoleSizeProperty); }
            set
            {
                SetValue(HoleSizeProperty, value);
                this.RefreshLayout();
            }
        }
        public static readonly DependencyProperty HoleSizeProperty =
            DependencyProperty.Register("HoleSize", typeof(int), typeof(PolygonMenu), new PropertyMetadata(null));
        #endregion

        #region [内部属性]
        private Brush normalBlockBrush
        {
            get { return new SolidColorBrush(BlockColor); }
        }
        private Brush hoverBlockBrush
        {
            get { return new SolidColorBrush(BlockHoverColor); }
        }
        #endregion

        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        private Point? mouseHoverLocation = null;

        public void RefreshLayout()
        {
            var sideLength = (int)(this.Width / 2);
            var offset = (int)(this.Width / 2);

            //计算最外层多边形顶点位置
            var bigPolygon = CalculatePolygonVertices(SideNum, sideLength, offset);
            //计算中间多边形顶点位置，这里判断了如果不需要中间的按钮，则计算一个边长为0的坐标位置，用以计算每一块
            if (HoleSize == null)
            {
                HoleSize = sideLength / 2;
            }
            var smallPolygon = HasCenterHole ?
                CalculatePolygonVertices(SideNum, HoleSize.Value, offset) :
                CalculatePolygonVertices(SideNum, 0, offset);

            //计算两个多边形相交之后，形成得多边形环，分割为多个等腰梯形，用以检测点击事件
            var trapezoids = CalculateTrapezoids(bigPolygon, smallPolygon);

            this.Children.Clear();
            if (BackgroundImage != null)
            {
                this.Children.Add(new Image()
                {
                    Source = BackgroundImage,
                    Width = this.RenderSize.Width,
                    Height = this.RenderSize.Height,
                });
            }
            var index = 0;
            foreach (var trapezoid in trapezoids)
            {
                var polygon = new Polygon()
                {
                    Points = new PointCollection(trapezoid.Points),
                    Tag = index,
                };
                polygon.MouseLeftButtonUp += (s, e) =>
                {
                    OnMenuItemClicked?.Invoke(s, int.Parse((s as Polygon).Tag.ToString()));
                };
                if (
                    mouseHoverLocation != null &&
                    IsPointInTrapezoid(mouseHoverLocation.Value, trapezoid)
                )
                {
                    polygon.Fill = hoverBlockBrush;
                }
                else
                {
                    polygon.Fill = normalBlockBrush;
                }
                this.Children.Add(polygon);
                index++;
            }
            if (HasCenterHole)
            {
                //计算内部小的多边形，用以检测点击事件
                var centerPolygonVertices = CalculatePolygonVertices(SideNum, HoleSize.Value - PolygonGapSize, offset);
                var centerPolygon = new Polygon()
                {
                    Points = new PointCollection(centerPolygonVertices),
                    Tag = -1,
                };
                centerPolygon.MouseLeftButtonUp += (s, e) =>
                {
                    OnMenuItemClicked?.Invoke(s, -1);
                };
                if (
                    mouseHoverLocation != null &&
                    IsPointInPolygon(mouseHoverLocation.Value, centerPolygonVertices)
                )
                {
                    centerPolygon.Fill = hoverBlockBrush;
                }
                else
                {
                    centerPolygon.Fill = normalBlockBrush;
                }
                this.Children.Add(centerPolygon);
            }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageSource imageSource = (ImageSource)e.NewValue;
            UpdateBaseUri(d, imageSource);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (this.Width != this.Height)
            {
                this.Height = this.Width;
            }
            if (HoleSize == null || HoleSize == 0)
            {
                HoleSize = (int)this.Width / 2 / 2;
            }
            this.RefreshLayout();
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            mouseHoverLocation = e.GetPosition(this);
            this.RefreshLayout();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            mouseHoverLocation = null;
            this.RefreshLayout();
        }

        private static void UpdateBaseUri(DependencyObject d, ImageSource source)
        {
            if (source is IUriContext && !source.IsFrozen && ((IUriContext)source).BaseUri == null)
            {
                Uri baseUriCore = BaseUriHelper.GetBaseUri(d);
                if (baseUriCore != null)
                {
                    ((IUriContext)source).BaseUri = BaseUriHelper.GetBaseUri(d);
                }
            }
        }

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
    }
}
