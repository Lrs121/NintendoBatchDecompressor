// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists

using System;
using System.IO;

namespace BatchDecompressor
{
	public class BatchDecompressor
	{
		public static void Main(string[] args)
		{
		    if (args.Length == 0)
		    {
                Console.WriteLine("Please supply one or more file paths as arguments.");
		        Console.ReadLine();
                System.Environment.Exit(1);
		    }

			foreach (var path in args)
			{
			    if (File.Exists(path))
			    {
			        ProcessFile(path);
			        Console.WriteLine("Processing file " + path);
			    }
			    else if (Directory.Exists(path))
			    {
			        ProcessDirectory(path);
			    }
			    else
			    {
			        Console.WriteLine("{0} is not a valid file or directory.", path);
			    }
			}
		    Console.WriteLine("Done.");
			Console.ReadLine(); //Pause
		}

		// Process all files in the directory passed in and process the files they contain.
		public static void ProcessDirectory(string targetDirectory)
		{
			// Process the list of files found in the directory.
			var fileEntries = Directory.GetFiles(targetDirectory);
			foreach (var fileName in fileEntries) { 
			    Console.WriteLine(fileName);
			    ProcessFile(fileName);
			}
		}

		// Insert logic for processing found files here.
		public static void ProcessFile(string path)
		{
		    try
		    {
		        Stream fs = File.OpenRead(path);
		        var newPath = path + ".stex";
		        var decompressed = Nintendo.Decompress(fs);
		        File.WriteAllBytes(newPath, decompressed);
		    }
		    catch (System.ArgumentException)
		    {
                Console.WriteLine(path + " Cannot be decompressed.");
		    }
		}
	}
}