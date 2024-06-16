using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Meowsterpiece.Archiving
{
    public class MeowsterpieceChunk
    {
        public string ChunkID { get; set; }
        public int ChunkSize { get; set; }
        public byte[] ChunkData { get; set; }

        public MeowsterpieceChunk(string id, byte[] data)
        {
            ChunkID = id;
            ChunkData = data;
            ChunkSize = data.Length;
        }
    }

    public class MeowsterpieceArchive
    {
        public const int CurrentVersion = 1;

        public string FileType { get; set; } = "MPAF";
        public int Version { get; set; }
        public List<MeowsterpieceChunk> Chunks { get; set; } = new List<MeowsterpieceChunk>();

        public MeowsterpieceArchive(int version)
        {
            Version = version;
        }

        public void AddChunk(MeowsterpieceChunk chunk)
        {
            Chunks.Add(chunk);
        }

        public void SaveToFile(string filePath)
        {
            using (var writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                // Write MPAF header
                writer.Write(Encoding.ASCII.GetBytes(FileType));
                writer.Write(4 + Chunks.Sum(chunk => 8 + chunk.ChunkSize)); // MPAF size
                writer.Write(Encoding.ASCII.GetBytes("PURR")); // Custom file type

                // Write version
                writer.Write(Version);

                // Write chunks
                foreach (var chunk in Chunks)
                {
                    writer.Write(Encoding.ASCII.GetBytes(chunk.ChunkID));
                    writer.Write(chunk.ChunkSize);
                    writer.Write(chunk.ChunkData);
                }
            }
        }

        public static MeowsterpieceArchive LoadFromFile(string filePath)
        {
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                // Read MPAF header
                var fileType = new string(reader.ReadChars(4));
                if (fileType != "MPAF")
                    throw new Exception("Invalid MPAF file");

                var fileSize = reader.ReadInt32();
                var fileFormat = new string(reader.ReadChars(4));
                if (fileFormat != "PURR")
                    throw new Exception("Unsupported file format");

                // Read version
                var version = reader.ReadInt32();

                if (version != CurrentVersion)
                    throw new Exception($"Unsupported version: {version}");

                var meowFile = new MeowsterpieceArchive(version);

                // Read chunks
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var chunkID = new string(reader.ReadChars(4));
                    var chunkSize = reader.ReadInt32();
                    var chunkData = reader.ReadBytes(chunkSize);
                    meowFile.AddChunk(new MeowsterpieceChunk(chunkID, chunkData));
                }

                return meowFile;
            }
        }
    }
}