using System.IO;

namespace ExtensionMethods
{
    public static class ByteExtension
    {
        public static MemoryStream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }
    }
}