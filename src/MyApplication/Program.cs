using System;
using System.Collections.Generic;

using Meowsterpiece.Archiving;

public class Program
{
    public static void Main()
    {
        // List of files to be archived
        var filePaths = new List<string>
        {
            "hi.txt",
            "bye.txt"
        };

        string archivePath = "game_assets.purrs";
        string outputDirectory = "extracted_assets";

        // Create a Meowsterpiece Archive with compression
        MeowsterpieceArchiveHandler.SaveToFile(archivePath, filePaths);

        Console.WriteLine("Files have been purr-fectly archived!");

        // Extract files from the Meowsterpiece Archive
        MeowsterpieceArchiveHandler.ExtractFromFile(archivePath, outputDirectory);

        Console.WriteLine("Files have been purr-fectly extracted!");

        Console.WriteLine("Meowsterpiece Archive operations completed!");
    }
}