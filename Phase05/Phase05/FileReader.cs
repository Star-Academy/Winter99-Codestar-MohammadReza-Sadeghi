﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Phase05
{
    public class FileReader
    {
        public static List<string> ReadFromFolder(string pathFolder)
        {
            var documents = new List<string>();
            try
            {
                foreach (string file in Directory.EnumerateFiles(pathFolder, "*"))
                {
                    string content = File.ReadAllText(file);
                    if (!content.Equals(""))
                        documents.Add(content);
                }
            }
            catch 
            {
                Console.WriteLine("Invalid path :)");
            }
            return documents;
        }
    }
}
