using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace SmartNews.Utils
{
    public class LoadResourceText : ContentPage
    {
        public static string GetJsonData(string JsonFileName)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Utils.Storage.{JsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return json;
            }
        }
        public LoadResourceText()
        {
        }
    }
}

