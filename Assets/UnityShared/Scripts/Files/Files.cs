using System.IO;

namespace UnityShared.Files
{
    public class Files
    {
        public static void Save(string message, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.Write(message);
        }

    }
}