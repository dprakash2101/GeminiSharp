
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiSharp.Util
{
    public static class FileConverter
    {
        public static byte[] GetBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public static string GetBase64String(string path)
        {
            return Convert.ToBase64String(GetBytes(path));
        }
    }
}
