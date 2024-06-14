using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace COG
{
    public static class JasHelper
    {
        static void CreateFilePath(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                // File Path에서 Directory Path를 받아옮
                string dc = Path.GetDirectoryName(FilePath);

                // Directory가 없을 경우 생성
                if (!Directory.Exists(dc))
                    Directory.CreateDirectory(dc);

                using (File.Create(FilePath)) { };
            }
        }
        public static bool Serializing(string Path, Object Obj)
        {
            Stream memoryStream = new MemoryStream();
            try
            {
                CreateFilePath(Path);
                // Binary 형태로 serialzie 하기 위한 포맷 생성
                BinaryFormatter formatter = new BinaryFormatter();
                // obj 개체를 스트림 serialize
                formatter.Serialize(memoryStream, Obj);
                // 파일을 쓰기 위해 스트림 생성
                // using 사용시에 구문 끝나면 자동 스트림 삭제
                using (Stream fileStream = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    memoryStream.Position = 0;
                    memoryStream.CopyTo(fileStream);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[Serializing] " + ex.ToString());
                //  SvLogger.Log.Error("[Serializing] " + ex.ToString());
                return false;
            }
            finally
            {
                if (memoryStream != null) memoryStream.Dispose();
            }
        }
        public static object Deserializing(string Path)
        {
            if (!File.Exists(Path)) return null;

            // 새로운 Object 생성
            Object Obj = new Object();

            // Binary 형태로 serialzie 하기 위한 포맷 생성
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new BindChanger();
            try
            {
                // 파일을 읽기 위해 스트림 생성
                // using 사용시에 구문 끝나면 자동 스트림 삭제
                using (Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    // 스트림을 Deserialize하여 Obj개체 생성
                    Obj = (Object)formatter.Deserialize(stream);
                }

                return Obj;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[Deserializing] " + ex.ToString());
                //     SvLogger.Log.Error("[Deserializing] " + ex.ToString());
                return null;
            }
        }
        class BindChanger : System.Runtime.Serialization.SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                // Define the new type to bind to
                Type typeToDeserialize = null;

                // Get the current assembly
                string currentAssembly = Assembly.GetExecutingAssembly().FullName;

                // Create the new type and return it
                typeToDeserialize = Type.GetType(string.Format("{0}, {1}", typeName, currentAssembly));

                return typeToDeserialize;
            }
        }
    }
}
