using GeminiSharp.Models;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeminiSharp.Helpers;

/// <summary>
/// Provides utility methods for converting documents into Gemini API request content.
/// </summary>
public static class DocumentHelper
{
    /// <summary>
    /// Creates a <see cref="RequestContentPart"/> from a file path.
    /// </summary>
    /// <param name="filePath">The path to the file.</param>
    /// <param name="mimeType">
    /// Optional MIME type. If not provided, it is inferred from the file extension.
    /// </param>
    /// <returns>A <see cref="RequestContentPart"/> that contains base64-encoded file data.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
    /// <exception cref="InvalidOperationException">Thrown when MIME type cannot be determined.</exception>
    public static async Task<RequestContentPart> ToRequestContentPart(string filePath, string? mimeType = null)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        var bytes = await File.ReadAllBytesAsync(filePath);
        var base64 = Convert.ToBase64String(bytes);

        var finalMimeType = mimeType ?? GetMimeType(Path.GetExtension(filePath));

        if (string.IsNullOrWhiteSpace(finalMimeType))
        {
            throw new InvalidOperationException($"Could not determine MIME type for file: {filePath}");
        }

        return new RequestContentPart
        {
            InlineData = new InlineData
            {
                MimeType = finalMimeType,
                Data = base64
            }
        };
    }

    /// <summary>
    /// Infers the MIME type based on file extension.
    /// </summary>
    private static string GetMimeType(string extension)
    {
        return extension.ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".json" => "application/json",
            ".csv" => "text/csv",
            _ => "application/octet-stream", // fallback
        };
    }
}
