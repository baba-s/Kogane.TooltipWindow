# Kogane Tooltip Window

マウスカーソルの場所に Tooltip を表示できるエディタ拡張

## 使用例

```cs
using Kogane;
using UnityEditor;

public static class Example
{
    [MenuItem( "Assets/Hoge" )]
    public static void Hoge()
    {
        TooltipWindow.Open( "ピカチュウ" );
    }
}
```

![icon464](https://user-images.githubusercontent.com/6134875/195969576-efc146e3-aa28-4c42-9114-5f798086338e.gif)
