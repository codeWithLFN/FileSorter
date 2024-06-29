using System;
using System.Collections.Generic;
using System.IO;

namespace FileSorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set the console window size
            Console.SetWindowSize(100, 30); // Width: 100, Height: 30
            Console.BufferWidth = 100;
            Console.BufferHeight = 30;

            string response;

            // Display the name and purpose of the application
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================================================");
            Console.WriteLine("                               File Sorter Program                                ");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("This application sorts files on your Desktop into categorized folders based on their file extensions.");
            Console.WriteLine();
            Console.WriteLine("Supported file extensions:");
            Console.WriteLine(".jpg, .jpeg, .png, .gif, .mp4, .mov, .doc, .docx, .pdf, .txt, .xlsx, .pptx, .zip, .rar, .mp3, .wav");
            Console.WriteLine();
            Console.WriteLine("Note: This program will move files from your Desktop to categorized folders.");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("Do you want to run the program? (yes or no)");
            Console.ResetColor();

            response = Console.ReadLine();

            if (response.ToLower() == "yes")
            {
                SortFiles();
            }
            else
            {
                WaitForExit();
            }
        }

        static void SortFiles()
        {
            // Define Directory to organize
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string directory = Path.Combine(userDirectory, "Desktop");

            // Define the extensions and their corresponding folders
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Directory does not exist");
                Console.ResetColor();
                WaitForExit();
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

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Moved {filename} to {folderName} folder");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Extension {extension} not found");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{filename} is not a file");
                }
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done");
            Console.ResetColor();
            WaitForExit();
        }

        static void WaitForExit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
