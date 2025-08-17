
using System;
using System.IO;

namespace GeminiSharp.Helpers
{
    /// <summary>
    /// A helper class for file conversions.
    /// </summary>
    public static class FileConverter
    {
        /// <summary>
        /// Converts a file to a base64 encoded string.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>The base64 encoded string.</returns>
        public static string ToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }
    }
}
