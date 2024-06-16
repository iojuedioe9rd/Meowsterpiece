using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Meowsterpiece.Archiving
{
    using Compression;

    public static class MeowsterpieceArchiveHandler
    {
        public static void SaveToFile(string archivePath, List<string> filePaths)
        {
            var meowFile = new MeowsterpieceArchive(MeowsterpieceArchive.CurrentVersion);

            foreach (var filePath in filePaths)
            {
                var fileInfo = new FileInfo(filePath);
                var fileName = fileInfo.Name;
                var fileData = File.ReadAllBytes(filePath);

                // Compress the file data
                var compressedData = CompressionHelper.Compress(fileData);

                // Create a chunk for each file
                var chunkData = Encoding.ASCII.GetBytes(fileName + "\0").Concat(compressedData).ToArray();
                var chunk = new MeowsterpieceChunk("DATA", chunkData);
                meowFile.AddChunk(chunk);
            }

            meowFile.SaveToFile(archivePath);
        }

        public static void ExtractFromFile(string archivePath, string outputDirectory)
        {
            var meowFile = MeowsterpieceArchive.LoadFromFile(archivePath);

            foreach (var chunk in meowFile.Chunks)
            {
                if (chunk.ChunkID == "DATA")
                {
                    // Extract file name and compressed data
                    var data = chunk.ChunkData;
                    var nullIndex = Array.IndexOf(data, (byte)0);
                    var fileName = Encoding.ASCII.GetString(data, 0, nullIndex);
                    var compressedData = data.Skip(nullIndex + 1).ToArray();

                    // Decompress the data
                    var fileData = CompressionHelper.Decompress(compressedData);

                    var outputPath = Path.Combine(outputDirectory, fileName);
                    File.WriteAllBytes(outputPath, fileData);
                }
            }
        }
    }
}