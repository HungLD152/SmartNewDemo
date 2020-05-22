using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SmartNews.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;

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
        private static string PreparePath(string fileName)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            return path;
        }
        //Save json file
        public static bool SaveJsonData(object tabItem, string JsonFileName)
        {
            var path = PreparePath(JsonFileName);
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(jw, tabItem);
                        return true;
                    }
                }
            }
        }
        //Load json file
        public static string LoadJsonData(string JsonFileName)
        {
            var path = PreparePath(JsonFileName);
            if (!File.Exists(path))
            {
            }
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (JsonTextReader jr = new JsonTextReader(sr))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        var json = sr.ReadToEnd();
                        return json;
                        //return serializer.Deserialize<string>(jr);
                    }
                }
            }
        }
    }
}

