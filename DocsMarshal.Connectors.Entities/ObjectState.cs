using System;

namespace DocsMarshal.Connectors.Entities
{
    public class ObjectState
    {
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool EnableDocumentVersioning { get; set; }
        public string GridBackColor { get; set; }
        public string GridForeColor { get; set; }

        public ObjectState()
        {

        }
        public static string GetCssColorFromDmColor(string color, bool removeAlpha = false)
        {
            if (String.IsNullOrWhiteSpace(color))
                return null;
            // se è in formato #RRGGBB lo ritorno com'è
            if (color.Length == 7 && color[0] == '#')
                return color;
            if (color.Length == 9 && color[0] == '#')
            {
                // ARGB --> RGB
                if (removeAlpha)
                    return '#' + color.Substring(3);

                // ARGB --> rgba(r, g, b, a)
                var redHex = color.Substring(3, 2);
                int redDec = Int32.Parse(redHex, System.Globalization.NumberStyles.HexNumber);

                var greenHex = color.Substring(5, 2);
                int greenDec = Int32.Parse(greenHex, System.Globalization.NumberStyles.HexNumber);

                var blueHex = color.Substring(7, 2);
                int blueDec = Int32.Parse(blueHex, System.Globalization.NumberStyles.HexNumber);

                var alphaHex = color.Substring(1, 2);
                decimal alphaDec = Int32.Parse(alphaHex, System.Globalization.NumberStyles.HexNumber);

                return String.Format(new System.Globalization.CultureInfo("en-US"), "rgba({0}, {1}, {2}, {3})", redDec, greenDec, blueDec, alphaDec / 255);
            }
            return null;
        }
    }
}