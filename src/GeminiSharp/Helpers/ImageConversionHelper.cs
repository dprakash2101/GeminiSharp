using Serilog;

namespace GeminiSharp.Helpers
{
    /// <summary>
    /// A helper class for handling image generation output and converting base64-encoded image data into a memory stream.
    /// </summary>
    public static class ImageConversionHelper
    {
        /// <summary>
        /// Converts base64-encoded image data to a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="base64Data">The base64-encoded image data.</param>
        /// <param name="mimeType">The MIME type of the image (e.g., "image/png", "image/jpeg").</param>
        /// <returns>A <see cref="MemoryStream"/> containing the image data, or null if conversion fails.</returns>
        public static MemoryStream? ConvertBase64ToImageStream(string base64Data, string mimeType)
        {
            try
            {
                // Remove the "data:image/png;base64," or other prefix if it exists
                string prefix = $"data:{mimeType};base64,";
                if (base64Data.StartsWith(prefix))
                {
                    base64Data = base64Data.Substring(prefix.Length);
                }

                // Convert base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(base64Data);

                Log.Information("Successfully converted base64 data to MemoryStream. MimeType: {MimeType}, Size: {Size} bytes", mimeType, imageBytes.Length);

                return new MemoryStream(imageBytes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error converting base64 to MemoryStream: {Message}", ex.Message);
                return null;
            }
        }
    }
}
