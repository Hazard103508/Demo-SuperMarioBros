using System.Threading.Tasks;
using UnityEngine;

namespace UnityShared.Extensions.Unity3D
{
    public static class Texture2DExtensions
    {
        /// <summary>
        /// convierte la textura en un sprite
        /// </summary>
        /// <param name="texture">Convert the texture to a sprite</param>
        /// <returns></returns>
        public static Sprite ConvertToSprite(this Texture2D texture)
        {
            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
        /// <summary>
        /// convierte la textura en un sprite
        /// </summary>
        /// <param name="texture">Convert the texture to a sprite</param>
        /// <param name="PixelsPerUnit">number of pixels to display per unit of measure</param>
        /// <param name="spriteType">sprite mesh type</param>
        /// <returns></returns>
        public static Sprite ConvertToSprite(this Texture2D texture, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
        {
            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0, 0),
                PixelsPerUnit,
                0,
                spriteType
            );
        }
        /// <summary>
        /// Clone the texture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Texture2D Clone(this Texture2D source)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTexture);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;

            Texture2D newTexture = new Texture2D(source.width, source.height);
            newTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            newTexture.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);

            return newTexture;
        }

        /// <summary>
        /// Flip the pixels of an image
        /// </summary>
        /// <param name="texture">Texture to flip</param>
        /// <param name="flipHorizontally">flip horizontally</param>
        /// <param name="flipVertically">flip Vertically</param>
        /// <returns></returns>
        public static async Task<Texture2D> Flip(this Texture2D texture, bool flipHorizontally, bool flipVertically)
        {
            var pixelArray = texture.GetPixels();
            int width = texture.width;
            int height = texture.height;

            await Task.Run(() =>
            {
                if (flipVertically)
                {
                    for (int col = 0; col < width; col++)
                    {
                        for (int row = 0; row < height / 2; row++)
                        {
                            int indexBegin = row * width + col;
                            int indexEnd = (height - 1 - row) * width + col;

                            Color _begin = pixelArray[indexBegin];
                            Color _end = pixelArray[indexEnd];

                            pixelArray[indexBegin] = _end;
                            pixelArray[indexEnd] = _begin;
                        }
                    }
                }

                if (flipHorizontally)
                {
                    for (int row = 0; row < height; ++row)
                        System.Array.Reverse(pixelArray, row * width, width);
                }
            });

            texture.SetPixels(pixelArray);
            return texture;
        }
    }
}
