# File to Base64 Conversion

The `GeminiSharp.Util.FileConverter` is a helper class that provides an easy way to convert files and streams into base64-encoded strings. This is often required when you need to embed file content, such as images or documents, directly into an API request payload.

The class offers both synchronous and asynchronous methods for flexibility.

## Usage

You can convert a file either by providing its path or by passing a `Stream` object.

### From a File Path

```csharp
using GeminiSharp.Util;
using System.Threading.Tasks;

// Synchronously
string base64FromFile = FileConverter.ToBase64("path/to/your/image.jpg");
Console.WriteLine(base64FromFile.Substring(0, 30) + "...");

// Asynchronously
string base64FromFileAsync = await FileConverter.ToBase64Async("path/to/your/image.jpg");
Console.WriteLine(base64FromFileAsync.Substring(0, 30) + "...");
```

### From a Stream

This is useful when you are working with file streams, memory streams, or network streams.

```csharp
using GeminiSharp.Util;
using System.IO;
using System.Threading.Tasks;

// Synchronously
using (FileStream stream = new FileStream("path/to/your/document.pdf", FileMode.Open))
{
    string base64FromStream = FileConverter.ToBase64(stream);
    Console.WriteLine(base64FromStream.Substring(0, 30) + "...");
}

// Asynchronously
using (FileStream stream = new FileStream("path/to/your/document.pdf", FileMode.Open))
{
    string base64FromStreamAsync = await FileConverter.ToBase64Async(stream);
    Console.WriteLine(base64FromStreamAsync.Substring(0, 30) + "...");
}
```
