using System;
using System.IO;

namespace BFBB_Iso_Cleaner
{
    class Cleaner
    {
        static void Main(string[] args)
        {
            string gamePath = "";
            bool putInFolder = false;
            bool setPutInFolder = false;
            bool publish = false;
            string[] debugExtensions = { ".sdf", ".log", ".mpl", ".lip", ".mpl~", ".scc", ".bat~", ".bat", ".out", ".lop", ".dsf", ".dlf", ".bkp" };
            string[] publishExtensions = { ".elf", ".tpl", ".bnr" };
            if (args.Length == 0)
            {
                string appVersion = "2.3";
                Log("BFBB Iso Cleaner\nVersion: " + appVersion + "\n");
                if (gamePath != null)
                {
                    Log("Select the game path (Type it due to limited knowledge, type path with main files like sb.ini):");

                    if (Directory.Exists(Console.ReadLine()))
                    {
                        gamePath = Console.ReadLine();
                    }
                    else
                    {
                        Log("Invalid Path! Press and key to exit...");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                }

                if (setPutInFolder == true)
                {
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
                }

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

                
                string deletedFilesPath = Convert.ToString(Directory.GetParent(gamePath)) + @"\DeletedFiles";
                string[] fileNamesWithPaths = Directory.GetFiles(gamePath, "*", SearchOption.AllDirectories);
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
            else if (args.Length != 0)
            {
                foreach (string arg in args)
                {
                    string cleanedArg = arg.Trim();
                    if (cleanedArg == "-gamePath" && args[0] == "-path")
                    {
                        if (args[1].Length != 0)
                        {
                            gamePath = args[1];
                        }
                        else
                        {
                            Log("No valid input! Press any key to exit");
                            Console.ReadKey();
                            Environment.Exit(1);
                        }
                    }
                    else if (cleanedArg == "-putFolder" && args[2] == "-putFolder")
                    {
                        if (args[3].Length != 0)
                        {
                            if (args[3] == "true")
                            {
                                putInFolder = true;
                            }
                            else if (args[3] == "false")
                            {
                                putInFolder = false;
                            }
                            else
                            {
                                Log("No valid input! Press any key to exit");
                                Console.ReadKey();
                                Environment.Exit(1);
                            }
                            setPutInFolder = true;
                        }
                    }
                    else if (cleanedArg == "-mode" && args[2] == "-mode")
                    {
                        if (args[3].Length != 0)
                        {
                            if (args[3] == "publish")
                            {
                                publish = true;
                            }
                            else if (args[3] == "debug")
                            {
                                publish = false;
                            }
                            else
                            {
                                Log("No valid input! Press any key to exit");
                                Console.ReadKey();
                                Environment.Exit(1);
                            }
                        }
                    }
                }
            }

            string deletedFilesPath = Convert.ToString(Directory.GetParent(gamePath)) + @"\DeletedFiles";
            string[] fileNamesWithPaths = Directory.GetFiles(gamePath, "*", SearchOption.AllDirectories);
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

        public static void Log(string input)
        {
            try
            {
                Console.WriteLine(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
