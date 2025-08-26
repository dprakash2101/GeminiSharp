
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeminiSharp.Util
{
    /// <summary>
    /// A helper class to convert files to base64 encoding.
    /// </summary>
    public static class FileConverter
    {
        /// <summary>
        /// Converts the file at the specified path to a base64 encoded string.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>The base64 encoded string representation of the file.</returns>
        public static string ToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }

        /// <summary>
        /// Asynchronously converts the file at the specified path to a base64 encoded string.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the base64 encoded string.</returns>
        public static async Task<string> ToBase64Async(string filePath)
        {
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(fileBytes);
        }

        /// <summary>
        /// Converts a stream to a base64 encoded string.
        /// </summary>
        /// <param name="stream">The stream to convert.</param>
        /// <returns>The base64 encoded string representation of the stream.</returns>
        public static string ToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// Asynchronously converts a stream to a base64 encoded string.
        /// </summary>
        /// <param name="stream">The stream to convert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the base64 encoded string.</returns>
        public static async Task<string> ToBase64Async(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
