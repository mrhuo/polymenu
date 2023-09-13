#### 背景
闲来无事逛群聊，看到一网友发来这么一个需求：
![image.png](https://cdn.nlark.com/yuque/0/2023/png/21405578/1694529169985-93ea03da-8a86-4e67-84ea-4bac7bc7d7bf.png#averageHue=%23426662&clientId=ub483049f-b6a3-4&from=paste&height=489&id=ua5d7eb18&originHeight=840&originWidth=840&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=51557&status=done&style=none&taskId=u2588a7d1-6cef-4592-8410-d567ebe99c7&title=&width=489)
纯色图下面是一张六边形布局的背景图层，具体图片背景内容不便透露，所以这里采用色块形式模拟，ZF类的项目都喜欢这种给人新奇的东西，这每一个色块，都是一个可以点击进入的菜单。
#### 分析
乍一看这图，就是三个正六边形嵌套。所以就根据公式 `2 * Math.PI / 6`计算出每个6边形每个顶点偏移角度，再根据公式计算出每个顶点的坐标位置。算法如下：
```csharp
private static Point[] CalculateHexagonVertices(int sideLength, int offset = 0)
{
    Point[] vertices = new Point[6];
    double angle = 2 * Math.PI / 6;
    for (int i = 0; i < 6; i++)
    {
        int x = offset + (int)(sideLength * Math.Cos(i * angle));
        int y = offset + (int)(sideLength * Math.Sin(i * angle));
        vertices[i] = new Point(x, y);
    }
    return vertices;
}
```
首先根据 sideLength 边长，计算出**最外层正六边形**6个顶点的位置。注意这个时候因为顶点是从 (0,0) 计算的，所以这里要加入 offset 设置顶点位置，让每一顶点平移指定的像素。
再计算**中间那个小正六边形（包括蓝色间隙）**的顶点位置。
最后再计算**最中间不包括间隙的紫色小正六边形**顶点位置。
这时候有人可能要问中间那些色块的位置怎么确定，这是个好问题，仔细看就知道了，这每一块都是一个等腰梯形，而且每个等腰梯形，都可以根据**最外层的大正六边形**和**中间那个小正六边形（包括蓝色间隙）**顶点位置计算出来。
#### 实现
先定义一个梯形类：
```csharp
internal sealed class Trapezoid
{
    public Point TopLeft { get; set; }
    public Point TopRight { get; set; }
    public Point BottomLeft { get; set; }
    public Point BottomRight { get; set; }
    public Point[] Points
    {
        get
        {
            return new Point[] { TopLeft, TopRight, BottomRight, BottomLeft };
        }
    }
    public Trapezoid(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
    }
}
```
然后通过外层的两个正六边形顶点位置计算梯形位置，演示代码如下：
```csharp
private static Trapezoid[] CalculateTrapezoids(Point[] hexagonVertices, Point[] smallHexagonVertices)
{
    Trapezoid[] trapezoids = new Trapezoid[6];
    for (int i = 0; i < 6; i++)
    {
        Point topLeft = hexagonVertices[i];
        Point topRight = hexagonVertices[(i + 1) % 6];
        Point bottomLeft = smallHexagonVertices[i];
        Point bottomRight = smallHexagonVertices[(i + 1) % 6];
        trapezoids[i] = new Trapezoid(topLeft, topRight, bottomLeft, bottomRight);
    }
    return trapezoids;
}
```
我写了一个继承自 PictureBox 的控件类，重写了 OnPaint 方法，实现了以上色块的展示。演示代码如下：
```csharp
protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);
    var g = e.Graphics;
    //centerHexagon 是中间最小的不包括间隙的紫色的六边形顶点位置
    g.FillPolygon(new SolidBrush(Color.FromArgb(180, Color.Red)), centerHexagon);
    for (int i = 0; i < trapezoids.Length; i++)
    {
        //trapezoids 是6个梯形位置
        var trapezoid = trapezoids[i];
        g.FillPolygon(new SolidBrush(Color.FromArgb(10 * (i + 1), Color.Yellow)), trapezoid.Points);
    }
}
```
需要实现鼠标点击事件，我们需要定义一个事件，记录鼠标当前位置，然后在 OnMouseClick 方法中检测鼠标位置是否在某个梯形或者最中间位置，然后触发事件。演示代码如下：
```csharp
//0-6 每个梯形位置，-1 中间位置
//          4 
//     3         5
//         -1
//     2         0
//          1
/// <summary>
/// 菜单点击事件处理
/// </summary>
public event Action<object, int> OnMenuClicked;
/// <summary>
/// 鼠标当前位置
/// </summary>
private Point? mouseHoverLocation = null;
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
}
/// <summary>
/// 菜单点击事件
/// </summary>
/// <param name="e"></param>
protected override void OnMouseClick(MouseEventArgs e)
{
    var mouseLocation = e.Location;
    //检测鼠标是否在中间的六边形中：
    if (IsPointInPolygon(mouseLocation, centerHexagon))
    {
        OnMenuClicked?.Invoke(this, -1);
        return;
    }
    //检测是否在某个梯形内部
    for (int i = 0; i < trapezoids.Length; i++)
    {
        var trapezoid = trapezoids[i];
        if (IsPointInTrapezoid(mouseLocation, trapezoid))
        {
            OnMenuClicked?.Invoke(this, i);
            return;
        }
    }
}
```
里面有一个 IsPointInPolygon 方法，用于检测某一点是否在某个多边形内，这里的算法抄袭了某N上的代码，如下：
```csharp
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
```
大致就是这样，再就是缩放窗体时自动计算位置的细节处理，这里先贴一下全部代码：
```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HexagonButton
{
    public partial class HButton : PictureBox
    {
        //0-6 每个梯形位置，-1 中间位置
        //          4 
        //     3         5
        //         -1
        //     2         0
        //          1

        /// <summary>
        /// 菜单点击事件处理
        /// </summary>
        public event Action<object, int> OnMenuClicked;

        /// <summary>
        /// 每个梯形位置
        /// </summary>
        private Trapezoid[] trapezoids = new Trapezoid[6];

        /// <summary>
        /// 中间的小正六边形位置
        /// </summary>
        private Point[] centerHexagon = new Point[6];

        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        private Point? mouseHoverLocation = null;

        /// <summary>
        /// 鼠标滑过时的层背景
        /// </summary>
        private SolidBrush mouseHoverLayerBrush = new SolidBrush(Color.FromArgb(50, Color.White));

        public HButton()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// 缩放窗体时（调用），自动修正位置
        /// </summary>
        /// <param name="formWidth"></param>
        /// <param name="formHeight"></param>
        public void ResetSizeByForm(int formWidth, int formHeight)
        {
            var hHeight = (int)(formHeight * 0.8);
            var hWidth = hHeight;

            var hLeft = (formWidth - hWidth) / 2;
            var hTop = (formHeight - hHeight) / 2;

            this.Location = new Point(hLeft, hTop);
            this.Width = hWidth;
            this.Height = hHeight;
        }

        private void InitHexagonMenus()
        {
            this.BackColor = Color.Transparent;
            //this.Image = Properties.Resources.button_bg;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.Cursor = Cursors.Hand;

            //计算图片缩放级别
            //var scale = Properties.Resources.button_bg.Width / this.Width;
            //计算原始图片高度和宽度之差，因为该背景非正六边形，所以计算一下宽度和高度之差，用以计算正确得位置
            //var diffOfImageSize = (Properties.Resources.button_bg.Width - Properties.Resources.button_bg.Height) / scale;
            var diffOfImageSize = 0;

            var sideWidth = (this.Width - diffOfImageSize) / 2;
            var offset = this.Width / 2;

            //计算最外层大六边形顶点位置
            var big = CalculateHexagonVertices((this.Width + diffOfImageSize / 2) / 2, offset);

            //计算内部小六边形顶点位置
            var small = CalculateHexagonVertices(sideWidth / 2, offset);

            //计算两个六边形相交之后，形成得六边形环，分割为6个等腰梯形，用以检测点击事件
            trapezoids = CalculateTrapezoids(big, small);

            //计算内部小的正六边形，用以检测点击事件
            centerHexagon = CalculateHexagonVertices(sideWidth / 2 - 20, offset);
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
        }

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            var mouseLocation = e.Location;
            //检测鼠标是否在中间的六边形中：
            if (IsPointInPolygon(mouseLocation, centerHexagon))
            {
                OnMenuClicked?.Invoke(this, -1);
                return;
            }
            //检测是否在某个梯形内部
            for (int i = 0; i < trapezoids.Length; i++)
            {
                var trapezoid = trapezoids[i];
                if (IsPointInTrapezoid(mouseLocation, trapezoid))
                {
                    OnMenuClicked?.Invoke(this, i);
                    return;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
#if DEBUG
            //以下的代码可以直接删除，这里是作为标识多边形位置
            g.FillPolygon(new SolidBrush(Color.FromArgb(180, Color.Red)), centerHexagon);
            for (int i = 0; i < trapezoids.Length; i++)
            {
                var trapezoid = trapezoids[i];
                g.FillPolygon(new SolidBrush(Color.FromArgb(10 * (i + 1), Color.Yellow)), trapezoid.Points);
            }
#endif
            if (mouseHoverLocation == null)
            {
                return;
            }
            //检测鼠标是否在中间的六边形中：
            if (IsPointInPolygon(mouseHoverLocation.Value, centerHexagon))
            {
                g.FillPolygon(mouseHoverLayerBrush, centerHexagon);
                return;
            }
            //检测是否在某个梯形内部
            for (int i = 0; i < trapezoids.Length; i++)
            {
                var trapezoid = trapezoids[i];
                if (IsPointInTrapezoid(mouseHoverLocation.Value, trapezoid))
                {
                    g.FillPolygon(mouseHoverLayerBrush, trapezoid.Points);
                    return;
                }
            }
        }

        /// <summary>
        /// 计算正六边形的顶点坐标
        /// </summary>
        /// <param name="sideLength"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static Point[] CalculateHexagonVertices(int sideLength, int offset = 0)
        {
            Point[] vertices = new Point[6];
            double angle = 2 * Math.PI / 6;

            for (int i = 0; i < 6; i++)
            {
                int x = offset + (int)(sideLength * Math.Cos(i * angle));
                int y = offset + (int)(sideLength * Math.Sin(i * angle));
                vertices[i] = new Point(x, y);
            }

            return vertices;
        }

        /// <summary>
        /// 计算每个梯形的坐标
        /// </summary>
        /// <param name="hexagonVertices"></param>
        /// <param name="smallHexagonVertices"></param>
        /// <returns></returns>
        private static Trapezoid[] CalculateTrapezoids(Point[] hexagonVertices, Point[] smallHexagonVertices)
        {
            Trapezoid[] trapezoids = new Trapezoid[6];
            for (int i = 0; i < 6; i++)
            {
                Point topLeft = hexagonVertices[i];
                Point topRight = hexagonVertices[(i + 1) % 6];
                Point bottomLeft = smallHexagonVertices[i];
                Point bottomRight = smallHexagonVertices[(i + 1) % 6];
                trapezoids[i] = new Trapezoid(topLeft, topRight, bottomLeft, bottomRight)
                {
                    Index = i
                };
            }
            return trapezoids;
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

        /// <summary>
        /// 当窗体改变时，自动计算大小
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            InitHexagonMenus();
        }
    }

    /// <summary>
    /// 梯形类
    /// </summary>
    internal sealed class Trapezoid
    {
        public int Index { get; set; }
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomLeft { get; set; }
        public Point BottomRight { get; set; }

        public Point[] Points
        {
            get
            {
                return new Point[] { TopLeft, TopRight, BottomRight, BottomLeft };
            }
        }

        public Trapezoid(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }

        public override string ToString()
        {
            return $"Trapezoid {{ Index={Index}, TopLeft={TopLeft}, TopRight={TopRight}, BottomLeft={BottomLeft}, BottomRight={BottomRight} }}";
        }
    }
}
```
窗体代码：
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HexagonButton
{
    public partial class Form1 : Form
    {
        private HButton HButton;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //这个窗体里面把那个纯色的背景图放进去
            this.BackColor = Color.FromArgb(9, 56, 128);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (HButton == null)
            {
                HButton = new HButton();
                HButton.OnMenuClicked += (s, index) =>
                {
                    MessageBox.Show($"点击了菜单：#{index}");
                };
                this.Controls.Add(HButton);
            }
            else
            {
                HButton.ResetSizeByForm(this.Width, this.Height);
            }
        }
    }
}
```
就是在窗体缩放时，把六边形菜单动态加进去。
#### 扩展（！！！大量代码预警）
代码发给网友之后，网友表示很满意，然后经过我的指导，然后自由发挥改成了7边形，然后我又扩展了下面这一版，更加智能了。
截一些图片在这里：
![dae9f1bc8936fe07e95cebde8441127.png](https://cdn.nlark.com/yuque/0/2023/png/21405578/1694539459441-0f535bb1-f22a-4c7e-b17d-78d4b29ec85a.png#averageHue=%23f5afa3&from=paste&id=rpqO2&originHeight=574&originWidth=829&originalType=url&ratio=1.25&size=42036&status=done&title=)![293d8dbed1e758847a874c4c57823a5.png](https://cdn.nlark.com/yuque/0/2023/png/21405578/1694539480396-58e5b5b3-cdd1-4567-994c-f1bfab0a222e.png#averageHue=%23f5bab1&from=paste&id=OVtkE&originHeight=574&originWidth=829&originalType=url&ratio=1.25&size=46454&status=done&title=)![image.png](https://cdn.nlark.com/yuque/0/2023/png/21405578/1694539805487-25af56b7-139f-4d0c-8da5-5cce281acb55.png#averageHue=%23f0d998&from=paste&id=LbjFC&originHeight=753&originWidth=1159&originalType=url&ratio=1.25&size=101288&status=done&title=)
最终版演示 DEMO：
![image.png](https://cdn.nlark.com/yuque/0/2023/png/21405578/1694589801186-0df5292a-2e9d-4946-a1eb-14f6d679374b.png#averageHue=%23edcfc9&clientId=uf5bf144f-ea89-4&from=paste&height=596&id=udf07b7c7&originHeight=745&originWidth=1145&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=114920&status=done&style=none&taskId=u3733071a-b6cb-4b18-a7a1-5b7a32e7d5d&title=&width=916)

做着做着，就一发不可收拾了，做的越来越顺眼了...
最后，该菜单通过增加了一些配置，使得可以自定义边数，是否包含中间菜单等功能，功能更加强大了一些。该项目已全部开源，开源地址：[https://github.com/mrhuo/polymenu](https://github.com/mrhuo/polymenu)
NUGET 安装：
```bash
Install-Package MrHuo.PolyMenu -Version 1.0.23.913
```
