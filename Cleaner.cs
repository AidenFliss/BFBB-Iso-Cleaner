using System;
using System.Diagnostics;
using System.IO;

namespace BFBB_Iso_Cleaner
{
    class Cleaner
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string appVersion = "1.7";
                Console.WriteLine("BFBB Iso Cleaner\nVersion: " + appVersion + "\n\n");

                Console.WriteLine("Select the game path (Type it due to limited knowledge): \n");

                if (Directory.Exists(Console.ReadLine()))
                {

                }else
                {
                    
                }

                string gamePath = "";
                if (Console.ReadLine() != null)
                {
                    gamePath = Console.ReadLine();
                }

                Console.WriteLine("What game is this? (BFBB/TSSM): ");

                string game = "";
                if (Console.ReadLine() == "BFBB")
                {
                    game = "Battle";
                }
                else if (Console.ReadLine() == "TSSM")
                {
                    game = "Movie";
                }
                
                bool putInFolder = false;

                Console.WriteLine("Should deleted files go in a folder? (or get deleted) (Y/N): ");

                if (Console.ReadLine() == "Y")
                {
                    putInFolder = true;
                }
                else if (Console.ReadLine() == "N")
                {
                    putInFolder = false;
                }
                else
                {
                    Console.WriteLine("Invalid Argument! Changing to N");
                }

                CleanIso(game, putInFolder, gamePath);
            }
        }

        public static void CleanIso(string game, bool putInFolder, string gamePath)
        {
            string deletedFilesPath = gamePath;
            if (game == "Battle")
            {
                string[] fileNamesWithPaths = Directory.GetFiles(gamePath);
                foreach (string file in fileNamesWithPaths)
                {
                    if (file.EndsWith(".sdf"))
                    {
                        if (putInFolder == true)
                        {
                            if (Directory.Exists(deletedFilesPath) == false)
                            {
                                Directory.CreateDirectory(deletedFilesPath);
                            }
                        }
                    }
                }
            }
            else if (game == "Movie")
            {

            }
        }

        public static void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
