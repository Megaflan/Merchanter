﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchanter
{
    class Program
    {
        static void Main(string[] args)
        {
            TBL tbl = new TBL();
            Dictionary.LoadDictionary(File.ReadAllLines("dic.txt"));
            try
            {
                switch (Path.GetExtension(args[0]).ToLower())
                {
                    case ".tbl":
                        tbl.Initialize(args[0]);
                        break;
                    case ".po":
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No compatible file found");
                Console.ReadLine();
            }
            

        }
    }
}
