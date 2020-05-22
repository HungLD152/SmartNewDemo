using System;
using Xamarin.Forms;

namespace SmartNews.Utils
{
    public class ConvertColorStr
    {
       public Color ConvertColorString(string colorname)
        {
            var colorconvert = new ColorTypeConverter();

            return (Xamarin.Forms.Color)colorconvert.ConvertFromInvariantString(colorname);
        }
    }
}
