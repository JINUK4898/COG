using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nrt;

using System.Windows.Forms;

namespace COG
{
    public class SystemFile
    {
        public static string FileNamePath = Application.StartupPath;
        public  string FileName { get; } = FileNamePath + "\\Systemdata_Cognex.json";


        [JsonProperty]
        public string Toolpath1 { get; set; } = "";

        [JsonProperty]
        public string Toolpath2 { get; set; } = "";

        [JsonProperty]
        public string Toolpath3 { get; set; } = "";

        [JsonProperty]
        public string ImageFolderPath { get; set; } = "";



        [JsonIgnore]
        [JsonProperty]
        public bool Use { get; set; } = false;

    }

    public static class JsonConvertHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="_object"></param>
        public static void Save(string path, object _object)
        {
            JsonSerializerSettings _JsonSerializerSet = new JsonSerializerSettings();
            _JsonSerializerSet.TypeNameHandling = TypeNameHandling.All;
            // _JsonSerializerSet.TypeNameHandling = TypeNameHandling.None;


            string stringobject = JsonConvert.SerializeObject(_object, Newtonsoft.Json.Formatting.Indented, _JsonSerializerSet);
            File.WriteAllText(path, stringobject);
        }
        //public static void LoadToExistingTarget<T>(string path, T target) where T : class
        //{
        //    if (!File.Exists(path))
        //        return;
        //
        //    JsonSerializerSettings _JsonSerializerSet = new JsonSerializerSettings();
        //    _JsonSerializerSet.TypeNameHandling = TypeNameHandling.Auto;
        //
        //    //   string stringobject = File.ReadAllText(path);
        //    //   JsonConvert.PopulateObject(stringobject, target, _JsonSerializerSet);
        //
        //    var stringobject2 = File.ReadAllText(path);
        //    target = JsonConvert.DeserializeObject<T>(stringobject2);
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="target"></param>
        public static void LoadToExistingTarget<T>(string path, ref T target) where T : class
        {
            if (!File.Exists(path)) return;

            JsonSerializerSettings _JsonSerializerSet = new JsonSerializerSettings();
            _JsonSerializerSet.TypeNameHandling = TypeNameHandling.Auto;
            //      JsonConvert.PopulateObject(File.ReadAllText(path), target, _JsonSerializerSet);

            target = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

            //     target = (T)JsonConvert.DeserializeObject(path , JsonSerializerSettings );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public static object DeepCopy(object _object)
        {
            JsonSerializerSettings _JsonSerializerSet = new JsonSerializerSettings();
            _JsonSerializerSet.TypeNameHandling = TypeNameHandling.All;

            string stringobject = JsonConvert.SerializeObject(_object, _JsonSerializerSet);
            return JsonConvert.DeserializeObject(stringobject, _object.GetType(), _JsonSerializerSet);
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        public static void DeepCopyToExistingTarget<T>(T source, T target) where T : class
        {
            //if (source == null) return;
            //JsonSerializerSettings _JsonSerializerSet = new JsonSerializerSettings();
            //_JsonSerializerSet.TypeNameHandling = TypeNameHandling.All;
            //
            //string stringobject = JsonConvert.SerializeObject(source, Newtonsoft.Json.Formatting.Indented, _JsonSerializerSet);
            //JsonConvert.PopulateObject(source, target, _JsonSerializerSet);
        }

    }
}
