using System;
using System.Collections.Generic;
using System.IO;


namespace FileSorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Define Directory to organize
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string directory = Path.Combine(userDirectory, "Desktop");

            //Define the extensions and their corresponding folders

            var extensions = new Dictionary<string, string>
            {
                { ".jpg", "Images" },
                { ".jpeg", "Images" },
                { ".png", "Images" },
                { ".gif", "Images" },
                { ".mp4", "Videos" },
                { ".mov", "Videos" },
                { ".doc", "Documents" },
                { ".docx", "Documents" },
                { ".pdf", "Documents" },
                { ".txt", "Documents" },
                { ".xlsx", "Documents" },
                { ".pptx", "Documents" },
                { ".zip", "Compressed" },
                { ".rar", "Compressed" },
                { ".mp3", "Music" },
                { ".wav", "Music" }
            };

            // Check if the directory exists
            if (!Directory.Exists(directory))
            {
                Console.WriteLine("Directory does not exist");
                return;
            }

            // Loop through each file in the directory
            foreach (string filePath in Directory.GetFiles(directory))
            {
                string filename = Path.GetFileName(filePath);

                // Check if it is a file
                if (File.Exists(filePath))
                {
                    // Get the file extension
                    string extension = Path.GetExtension(filePath).ToLower();

                    // Check if the extension is in the dictionary
                    if (extensions.ContainsKey(extension))
                    {
                        string folderName = extensions[extension];
                        string folderPath = Path.Combine(directory, folderName);

                        // Create the folder if it doesn't exist
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Move the file to the appropriate folder
                        string destinationPath = Path.Combine(folderPath, filename);
                        File.Move(filePath, destinationPath);

                        Console.WriteLine($"Moved {filename} to {folderName} folder");
                    }
                    else
                    {
                        Console.WriteLine($"Extension {extension} not found");
                    }
                }
                else
                {
                    Console.WriteLine($"{filename} is not a file");
                }
            }

            Console.WriteLine("Done");
        }
    }
}
