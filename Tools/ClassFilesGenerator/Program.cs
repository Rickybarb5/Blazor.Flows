using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run -- <source-directory> <destination-directory>");
                return;
            }

            string sourceDirectory = args[0];
            string destinationDirectory = args[1];

            if (!Directory.Exists(sourceDirectory))
            {
                Console.WriteLine($"Error: Source directory '{sourceDirectory}' does not exist.");
                return;
            }

            // Ensure destination directory exists
            Directory.CreateDirectory(destinationDirectory);

            // Filenames to look for (without extensions)
            var targetBaseNames = new List<string>
            {
                "DoubleClickBehaviour",
                "DoubleTapSelectOptions",

                "MyCustomComponent",
                "MyCustomNode",
                "CustomNodeOverview",
                "NodeOverview",

                "CustomGroup",
                "CustomGroupComponent",
                "CustomGroupOverview",
                "GroupOverview",
                "CustomNodeOverview",
                "NodeOverview",

                "CustomPort",
                "CustomPortComponent",
                "CustomPositionPort",
                "CustomPortOverview",
                "PortOverview",

                "OrthogonalLink",
                "CurvedLink",
                "LineLink",
                "ExtendedLink",
                "AnimatedLink",
                "AnimatedLinkComponent",
                "ExtendedLinkComponent",
                "CustomLinkOverview",
            };

            // Collect matching files
            var foundFiles = Directory
                .EnumerateFiles(sourceDirectory, "*", SearchOption.AllDirectories)
                .Where(file =>
                {
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(file)?.Trim();
                    return targetBaseNames.Contains(nameWithoutExt, StringComparer.OrdinalIgnoreCase);
                })
                .ToList();

            if (foundFiles.Count == 0)
            {
                Console.WriteLine("No matching files found.");
                return;
            }

            // Copy each found file into destination as .txt
            int copiedCount = 0;
            foreach (var file in foundFiles)
            {
                try
                {
                    string baseName = Path.GetFileNameWithoutExtension(file)?.Trim();
                    string destFile = Path.Combine(destinationDirectory, baseName + ".txt");

                    File.Copy(file, destFile, overwrite: true);
                    copiedCount++;

                    Console.WriteLine($"Copied: {file}  to  {destFile}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error copying '{file}': {ex.Message}");
                }
            }

            Console.WriteLine($"\n {copiedCount} files copied to '{destinationDirectory}'.");
        }
    }
}
