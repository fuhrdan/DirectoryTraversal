//*****************************************************************************
//** Directory Traversal                                                     **
//** This program creates a text file showing all directories and files on a **
//** system.  This document can be used for a search and find for docs in a  **
//** large file system.                                                      **
//*****************************************************************************


using System;
using System.Diagnostics;
using System.IO;

class DirectoryTraverser
{
    static void Main(string[] args)
    {
        string outputFilePath = "DirectoryListing.txt";

        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            // Get all logical drives on the system
            string[] drives = Environment.GetLogicalDrives();

            foreach (string drive in drives)
            {
                Console.WriteLine($"Processing drive: {drive}");
                writer.WriteLine($"Drive: {drive}");
                TraverseDirectory(drive, writer);
            }
        }

        Console.WriteLine($"Directory traversal completed. Results saved to {outputFilePath}");
    }

    static void TraverseDirectory(string path, StreamWriter writer)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c dir /s /b", // /s for all subdirectories, /b for bare format (just the paths)
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = path
        };

        using (Process process = Process.Start(psi))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
