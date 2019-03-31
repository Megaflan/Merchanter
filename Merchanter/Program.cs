using System;
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
            PO po = new PO();
            Dictionary.LoadDictionary(File.ReadAllLines("dic.txt"));
            try
            {
                switch (Path.GetExtension(args[0]).ToLower())
                {
                    case ".tbl":
                        tbl.Initialize(args[0]);
                        break;
                    case ".po":
                        po.POImport(args[0]);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No compatible file found");
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            

        }
    }
}
