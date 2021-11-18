using System;
using System.IO;

namespace BFBB_Iso_Cleaner
{
    class Cleaner
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string appVersion = "2.2";
                Log("BFBB Iso Cleaner\nVersion: " + appVersion + "\n");

                Log("Select the game path (Type it due to limited knowledge, type path with main files like sb.ini):");

                string gamePath = "";
                var line = Console.ReadLine();
                if (Directory.Exists(line))
                {
                    gamePath = line;
                }
                else
                {
                    Log("Invalid Path! Exiting...");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                bool putInFolder = false;

                Log("Should deleted files go in a folder? (or get deleted Y = folder, N = delete) (Y/N): ");

                var ln = Console.ReadLine().ToLower();

                if (ln == "y")
                {
                    putInFolder = true;
                }
                else if (ln == "n")
                {
                    putInFolder = false;
                }
                else
                {
                    Log("Invalid Argument! Changing to N");
                    putInFolder = false;
                }

                bool publish = false;
                Log("Is this folder for a mod publish or a debug test? (Y = publish N = debug) (Y/N): ");

                var l = Console.ReadLine().ToLower();

                if (l == "y")
                {
                    publish = true;
                }
                else if (l == "n")
                {
                    publish = false;
                }
                else
                {
                    Log("Invalid Argument! Changing to N");
                    publish = false;
                }

                string[] debugExtensions = {".sdf", ".log", ".mpl", ".lip", ".mpl~", ".scc", ".bat~", ".bat", ".out", ".lop", ".dsf", ".dlf", ".bkp"};
                string[] publishExtensions = {".elf", ".tpl", ".bnr"};
                string deletedFilesPath = Convert.ToString(Directory.GetParent(gamePath)) + @"\DeletedFiles";
                string[] fileNamesWithPaths = Directory.GetFiles(gamePath,"*",SearchOption.AllDirectories);
                foreach (string file in fileNamesWithPaths)
                {
                    foreach (string ext in debugExtensions)
                    {
                        var extensionOfFile = Path.GetExtension(file);
                        if (extensionOfFile.ToLower() == ext)
                        {
                            if (putInFolder == true)
                            {
                                if (Directory.Exists(deletedFilesPath) == false)
                                {
                                    Log("Could not find deleted path folder!");
                                    Directory.CreateDirectory(deletedFilesPath);
                                    Log("Created deleted path folder!");
                                }
                                else
                                {
                                    if (File.Exists(file))
                                    {
                                        MoveFile(file, deletedFilesPath);
                                        Log("Moved file: " + file + " To: " + deletedFilesPath);
                                    }
                                }
                            }
                            else
                            {
                                if (File.Exists(file))
                                {
                                    DeleteFile(file);
                                    Log("Deleted file: " + file);
                                }
                            }
                        }
                    }
                }

                if (publish == true)
                {
                    foreach (string file in fileNamesWithPaths)
                    {
                        foreach (string ext in publishExtensions)
                        {
                            var extensionOfFile = Path.GetExtension(file);
                            if (extensionOfFile.ToLower() == ext)
                            {
                                if (putInFolder == true)
                                {
                                    if (Directory.Exists(deletedFilesPath) == false)
                                    {
                                        Log("Could not find deleted path folder!");
                                        Directory.CreateDirectory(deletedFilesPath);
                                        Log("Created deleted path folder!");
                                    }
                                    else
                                    {
                                        MoveFile(file, deletedFilesPath);
                                        Log("Moved file: " + file + " To: " + deletedFilesPath);
                                    }
                                }
                                else
                                {
                                    DeleteFile(file);
                                    Log("Deleted file: " + file);
                                }
                            }
                        }
                    }
                }

                Log("Iso Sucessfully Cleaned! Press any key to close...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public static void DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path) == true)
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        public static void MoveFile(string path, string dest)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Move(path, dest);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        public static void Log(string text)
        {
            try
            {
                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
