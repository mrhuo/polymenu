#### 多边形菜单组件（PolygonMenu）

![polymenu.gif](./screeshots/polymenu.gif)

包含 winform 和 wpf 两种多边形组件，可以通过简单的配置，很方便的生成多边形菜单。

winform 版基于 .net framework 2.0 开发，可以方便的引入到各个版本的项目中。

wpf 版基于 .net framework 3.5 开发，可以方便的引入到各个版本的项目中。

- 开源地址：[https://github.com/mrhuo/polymenu](https://github.com/mrhuo/polymenu)
- Nuget地址：[https://www.nuget.org/packages/MrHuo.PolyMenu/](https://www.nuget.org/packages/MrHuo.PolyMenu/)

#### 相关介绍文章

- [C# WinForm 写一个六（多）边形菜单](https://mp.weixin.qq.com/s/bateU5gtJVABkdIyc9s8WA)

#### Winform 版使用说明

1. 引入 `release/MrHuo.PolyMenu.dll` 或使用 nuget 安装包 `MrHuo.PolyMenu`
2. 将 `MrHuo.PolyMenu.dll` 引入到工具箱，然后拖到界面上。或用代码创建多边形菜单，如下：

```csharp
var polygonMenu = new MrHuo.PolyMenu.PolygonMenu()
{
    Width = 800,
    HasCenterHole = true,
    PolygonGapSize = 20,
    SideNum = 6
};
this.Controls.Add(polygonMenu);
```
#### Wpf 版使用说明

1. 引入 `release/MrHuo.PolyMenu.Wpf.dll` 或使用 nuget 安装包 `MrHuo.PolyMenu.Wpf`
2. 在 `Window` 中引入名称空间，如下：
```xml
<Window x:Class="PolyMenuWpfDemo.MainWindow"
        ...
        xmlns:c="clr-namespace:MrHuo.PolyMenu.Wpf;assembly=MrHuo.PolyMenu.Wpf">
        ...
</Window>
```

```xml
<c:PolygonMenu SideNum="6" Background="DarkOrange" />
```

