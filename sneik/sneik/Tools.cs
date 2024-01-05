using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace sneikTools
{
    public static class Tools
    {
        public static Vector2 PointToVector2(Logic.Models.Point point)
        {
            return new Vector2(point.X, point.Y);
        }
        public static Color ModelsColorToFrameworkColor(Logic.Models.Color color)
        {
            Int32 Color = (Int32)color;
            string HexVal = Color.ToString("X");

            string rHex = HexVal.Substring(0, 2);
            string gHex = HexVal.Substring(2, 2);
            string bHex = HexVal.Substring(4, 2);

            int rInt = int.Parse(rHex, System.Globalization.NumberStyles.HexNumber);
            int gInt = int.Parse(gHex, System.Globalization.NumberStyles.HexNumber);
            int bInt = int.Parse(bHex, System.Globalization.NumberStyles.HexNumber);

            return new Color(rInt, gInt, bInt);
        }
        public static Logic.Enums.Keys[] XnaKeysToLogicKeys(Keys[] XnaKeys)
        {
            Logic.Enums.Keys[] result = new Logic.Enums.Keys[XnaKeys.Length];
            int idx = 0;
            foreach(var key in XnaKeys)
            {
                result[idx] = (Logic.Enums.Keys)key;
            }
            return result;
        }
    }
}
