using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PolyMenuWpfDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            pm.OnMenuItemClicked += (s, e) =>
            {
                System.Windows.MessageBox.Show($"点击了 #{e}");
            };
        }

        private void configSideNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pm.SideNum = (int)e.NewValue;
        }

        private void configHasCenterHole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pm.HasCenterHole = (configHasCenterHole.SelectedItem as ComboBoxItem).Content.ToString() == "true";
        }

        private void configGapSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pm.PolygonGapSize = (int)e.NewValue;
        }

        private void configImage_Click(object sender, RoutedEventArgs e)
        {
            var opd = new OpenFileDialog();
            if (opd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pm.BackgroundImage = new BitmapImage(new Uri(opd.FileName));
            }
        }

        private void configBlockColorToTransparent_Click(object sender, RoutedEventArgs e)
        {
            pm.BlockColor = Colors.Transparent;
            configBlockColor.Text = ColorToHexString(Colors.Transparent);
        }

        private static string ColorToHexString(Color color)
        {
            string hexColor(byte b)
            {
                return $"{Convert.ToString(b, 16)}".PadLeft(2, '0');
            }
            return $"#{hexColor(color.A)}{hexColor(color.R)}{hexColor(color.G)}{hexColor(color.B)}".ToUpper();
        }

        private static Color? HexStringToColor(string hexString)
        {
            if (hexString == null || !hexString.StartsWith("#") || hexString.Length != 9)
            {
                return null;
            }
            hexString = hexString.Substring(1);
            if (!new Regex("[0-9A-F]").IsMatch(hexString))
            {
                return null;
            }
            byte hexColorToByte(string hex)
            {
                return (byte)Convert.ToInt32(hex, 16);
            }
            byte a = 255;
            if (hexString.Length == 8)
            {
                a = hexColorToByte(hexString.Substring(0, 2));
                hexString = hexString.Substring(2);
            }
            byte r = hexColorToByte(hexString.Substring(0, 2));
            byte g = hexColorToByte(hexString.Substring(2, 2));
            byte b = hexColorToByte(hexString.Substring(4, 2));
            return Color.FromArgb(a, r, g, b);
        }

        private SolidColorBrush errorInputBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush successInputBrush = new SolidColorBrush(Colors.Transparent);
        private void configBlockColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hexColor = HexStringToColor(configBlockColor.Text.Trim());
            if (hexColor == null)
            {
                configBlockColor.Background = errorInputBrush;
            }
            else
            {
                configBlockColor.Background = successInputBrush;
                pm.BlockColor = hexColor.Value;
                configBlockColor.Text = ColorToHexString(hexColor.Value);
            }
        }

        private void configBlockHoverColorToTransparent_Click(object sender, RoutedEventArgs e)
        {
            pm.BlockHoverColor = Colors.Transparent;
            configBlockHoverColor.Text = ColorToHexString(Colors.Transparent);
        }

        private void configBlockHoverColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hexColor = HexStringToColor(configBlockHoverColor.Text.Trim());
            if (hexColor == null)
            {
                configBlockHoverColor.Background = errorInputBrush;
            }
            else
            {
                configBlockHoverColor.Background = successInputBrush;
                pm.BlockHoverColor = hexColor.Value;
                configBlockHoverColor.Text = ColorToHexString(hexColor.Value);
            }
        }

        private void minusControlSize_Click(object sender, RoutedEventArgs e)
        {
            pm.Width -= 50;
        }

        private void plusControlSize_Click(object sender, RoutedEventArgs e)
        {
            pm.Width += 50;
        }

        private SolidColorBrush tomatoBg = new SolidColorBrush(Colors.DarkOrange);
        private SolidColorBrush transparentBg = new SolidColorBrush(Colors.Transparent);
        private void setControlBgTransparent_Click(object sender, RoutedEventArgs e)
        {
            pm.Background = transparentBg;
        }

        private void restoreControlBg_Click(object sender, RoutedEventArgs e)
        {
            pm.Background = tomatoBg;
        }

        private void clearImage_Click(object sender, RoutedEventArgs e)
        {
            pm.BackgroundImage = null;
        }

        private void configHoleSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pm.HoleSize = (int)e.NewValue;
        }
    }
}
