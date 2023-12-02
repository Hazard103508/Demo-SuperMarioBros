using System.IO;

namespace UnityShared.Files
{
    public class Files
    {
        public static void Save(string message)
        {
            string path = $"C:\\Libraries\\Agustin\\Desktop\\Logs\\LOG.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
                sw.WriteLine(message);
        }

    }
}