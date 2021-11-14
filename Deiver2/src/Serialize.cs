using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Text.RegularExpressions;

namespace Deiver2
{
    public class Serialize2<T, R> 
        where T : class, IAddData<R> where R : class
    {
        public static string SerializeJson(T obj)
        {

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize<T>(obj, options);
            return json;
        }

        private static T Deserialize(string str_json)
        {
            return JsonSerializer.Deserialize<T>(str_json);
        }

        public static void Write(T obj, string path)
        {
            string str_json = SerializeJson(obj);
            if (File.Exists(path)) File.Delete(path);
            //using (StreamWriter sw = new StreamWriter(path))
            //    sw.Write(str_json);

            File.WriteAllText(path, str_json, Encoding.UTF8);
        }

        public static void AddData(T obj, string path)
        {
            if (File.Exists(path))
            {
                var old_data = Reader(path);

                var nw = old_data as T;
                if (nw != null)
                {
                    var nwd = obj as T;
                    if (nwd != null)
                    {
                        nw.Data.AddRange(nwd.Data);
                        var d = nw as T;
                        Write(d, path);
                    }
                }
            }
            else
            {
                Write(obj, path);
            }
        }

        //public static void AddData(T obj, string path)
        //{
        //    if (File.Exists(path))
        //    {
        //        var old_data = Reader(path);

        //        var nw = old_data as AllNews;
        //        if(nw != null)
        //        {
        //            var nwd = obj as AllNews;
        //            if (nwd != null)
        //            {
        //                nw.Data.AddRange(nwd.Data);
        //                var d = nw as T;
        //                Write(d, path);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Write(obj, path);
        //    }
        //}


        public static T Reader(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Данного файла {path} не существует");
                return null;
            }

            string str_json = File.ReadAllText(path);
            return Deserialize(str_json);

        }
        

    }
}
