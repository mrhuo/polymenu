#### 多边形菜单组件（PolygonMenu）

这是一款用于 C# winform 的多边形菜单组件，可以通过简单的配置，很方便的生成多边形菜单。
基于 .net framework 2.0 开发，可以方便的引入到各个版本的项目中。

开源地址：[https://github.com/mrhuo/polymenu](https://github.com/mrhuo/polymenu)

#### 相关介绍文章

`TODO: 待补充`

#### 使用说明

1. 引入 `release/MrHuo.PolyMenu.dll`
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
