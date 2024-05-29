using UnityEngine;

public class Util
{
    public static Color GetColor(PenColor color) => color switch
    {
        PenColor.Black => Color.black,
        PenColor.Blue => Color.blue,
        PenColor.LightGreen => new Color(0.7f, 1f, 0.2f, 1f),
        PenColor.Green => Color.green,
        PenColor.Yellow => Color.yellow,
        PenColor.Orange => new Color(1f, 1f, 0f, 1f),
        PenColor.Gray => Color.gray,
        PenColor.Purple => new Color(1f, 0f, 1f, 1f),
        _ => Color.clear
    };
}
