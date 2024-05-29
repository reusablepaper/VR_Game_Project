using UnityEngine;

public class Util
{
    public static Color GetColor(Palette color) => color switch
    {
        Palette.Black => Color.black,
        Palette.Blue => Color.blue,
        Palette.LightGreen => new Color(0.7f, 1f, 0.2f, 1f),
        Palette.Green => Color.green,
        Palette.Yellow => Color.yellow,
        Palette.Orange => new Color(1f, 0.5f, 0f, 1f),
        Palette.Gray => Color.gray,
        Palette.Purple => new Color(1f, 0f, 1f, 1f),
        _ => Color.clear
    };
}
