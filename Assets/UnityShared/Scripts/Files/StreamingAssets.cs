using System.Text;
using UnityEngine;

namespace UnityShared.Files
{
    public class StreamingAssets
    {
        /// <summary>
        /// Save a bytes array to file
        /// </summary>
        /// <param name="bytes">bytes to save</param>
        /// <param name="path">relative path of the file including name with extension</param>
        public static void Save(byte[] bytes, params string[] path)
        {
            string fullPath = GetStreamingAssetsFullPath(path);
            Files.Save(bytes, fullPath);
        }
        /// <summary>
        /// Save an object to file
        /// </summary>
        /// <typeparam name="T">Type of the object to save</typeparam>
        /// <param name="obj">Object to save</param>
        /// <param name="path">relative path of the file including name with extension</param>
        public static void Save<T>(T obj, params string[] path) where T : class
        {
            Save<T>(obj, false, Encoding.Default, path);
        }
        /// <summary>
        /// Save a text file
        /// </summary>
        /// <typeparam name="T">Type of the object to save</typeparam>
        /// <param name="obj">Object to save</param>
        /// <param name="append">Append text to the same file</param>
        /// <param name="encoding">Encoding code</param>
        /// <param name="path">relative path of the file including name with extension</param>
        public static void Save<T>(T obj, bool append, Encoding encoding, params string[] path) where T : class
        {
            string fullPath = GetStreamingAssetsFullPath(path);
            Files.Save<T>(obj, fullPath, append, encoding);
        }
        /// <summary>
        /// Load and read an external file
        /// </summary>
        /// <param name="path">relative path of the file including name with extension</param>
        /// <returns>returns the bytes of the file</returns>
        public static byte[] Load(params string[] path)
        {
            string fullPath = GetStreamingAssetsFullPath(path);
            return Files.ReadBytes(fullPath);
        }
        /// <summary>
        /// Loads an external file and deserialize it to the indicated type
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize</typeparam>
        /// <param name="path">relative path of the file including name with extension</param>
        /// <returns>Gets a new instance of an object with the data deseralized</returns>
        public static T Load<T>(params string[] path)
        {
            string fullPath = GetStreamingAssetsFullPath(path);
            return Files.Load<T>(fullPath);
        }

        /// <summary>
        /// Load an image from Streaming Assets directory
        /// </summary>
        /// <param name="path">relative path of the file including name with extension</param>
        /// <returns>returns a Texture from the image file</returns>
        public static Texture2D LoadTexture(params string[] path)
        {
            var bytes = StreamingAssets.Load(path);
            if (bytes != null)
            {
                var texture = new Texture2D(2, 2);
                texture.LoadImage(bytes); //..this will auto-resize the texture dimensions.
                return texture;
            }

            return null;
        }
        /// <summary>
        /// Save texture as file
        /// </summary>
        /// <param name="texture">texture to save</param>
        /// <param name="encodeType">Type of format in which the image will be saved</param>
        /// <param name="path">relative path of the file including name with extension</param>
        public static void SaveTexture(Texture2D texture, TextureEncodeType encodeType, params string[] path)
        {
            byte[] bytes =
                encodeType == TextureEncodeType.PNG ? texture.EncodeToPNG() :
                encodeType == TextureEncodeType.EXR ? texture.EncodeToEXR() :
                encodeType == TextureEncodeType.TGA ? texture.EncodeToTGA() :
                texture.EncodeToJPG();

            StreamingAssets.Save(bytes, path);
        }

        /// <summary>
        /// Determine if the file exists in the streaming assets folder
        /// </summary>
        /// <param name="path">relative path of the file including name with extension</param>
        /// <returns></returns>
        public static bool Exist_File(params string[] path)
        {
            string fullPath = GetStreamingAssetsFullPath(path);
            return Files.Exists(fullPath);
        }
        /// <summary>
        /// Gets the full path of a file inside the streaming assets folder
        /// </summary>
        /// <param name="path">relative path of the file including name with extension</param>
        /// <returns></returns>
        public static string GetStreamingAssetsFullPath(params string[] path)
        {
            string fullPath = System.IO.Path.Combine(UnityEngine.Application.streamingAssetsPath, System.IO.Path.Combine(path));
            string directory = System.IO.Path.GetDirectoryName(fullPath);
            if (!System.IO.Directory.Exists(directory))
                System.IO.Directory.CreateDirectory(directory);

            return fullPath;
        }


        public enum TextureEncodeType
        {
            JPG,
            PNG,
            TGA,
            EXR
        }
    }
}