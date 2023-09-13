using System.Drawing;

namespace MrHuo.PolyMenu
{
    /// <summary>
    /// 梯形类
    /// </summary>
    internal struct Trapezoid
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

        public override string ToString()
        {
            return $"Trapezoid {{ TopLeft={TopLeft}, TopRight={TopRight}, BottomLeft={BottomLeft}, BottomRight={BottomRight} }}";
        }
    }
}
