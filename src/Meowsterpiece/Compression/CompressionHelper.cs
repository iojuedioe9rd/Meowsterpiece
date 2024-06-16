using System.IO;
using System.IO.Compression;

namespace Meowsterpiece.Compression
{
    public static class CompressionHelper
    {
        public static byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    gzipStream.Write(data, 0, data.Length);
                }
                return compressedStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    using (var decompressedStream = new MemoryStream())
                    {
                        gzipStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }
    }
}