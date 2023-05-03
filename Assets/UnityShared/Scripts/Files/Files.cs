using System.IO;
using System.Text;

namespace UnityShared.Files
{
    public class Files
    {
        /// <summary>
        /// Loads an external file and deserialize it to the indicated type
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize</typeparam>
        /// <param name="path">file path</param>
        /// <returns>Gets a new instance of the object with the data deseralized</returns>
        public static T Load<T>(string path)
        {
            if (System.IO.File.Exists(path))
            {
                string json = ReadText(path);
                return UnityShared.Files.Json.Deserialize<T>(json);
            }

            return default(T);
        }
        /// <summary>
        /// Loads an external text file
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns></returns>
        public static string Load(string path)
        {
            if (System.IO.File.Exists(path))
                return ReadText(path);

            return string.Empty;
        }
        /// <summary>
        /// Save an external file
        /// </summary>
        /// <param name="bytes">Byte array to save</param>
        /// <param name="path">path to save the file</param>
        public static void Save(byte[] bytes, string path)
        {
            File.WriteAllBytes(path, bytes);
        }
        /// <summary>
        /// Serialize and save an external file
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize and save</typeparam>
        /// <param name="obj">Object to save</param>
        /// <param name="append">Append text to the same file</param>
        /// <param name="encoding">Encoding code</param>
        /// <param name="path">Path to save the file</param>
        public static void Save<T>(T obj, string path, bool append, Encoding encoding)
        {
            string json = Json.Serialize(obj);
            using (StreamWriter sw = new StreamWriter(path, append, encoding))
                sw.Write(json);
        }
        /// <summary>
        /// Determines if an external file is locked by another process
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns></returns>
        public static bool IsFileLocked(string path)
        {
            FileInfo file = new FileInfo(path);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }
        /// <summary>
        /// Determine if the file exists
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Open and read an external text file
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns>returns the text inside the file</returns>
        private static string ReadText(string path)
        {
            using (System.IO.StreamReader sr = new StreamReader(path))
                return sr.ReadToEnd();
        }
        /// <summary>
        /// Open and read an external file
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns>returns the bytes inside the file</returns>
        public static byte[] ReadBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}