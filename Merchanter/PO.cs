using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl;
using Yarhl.FileFormat;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace Merchanter
{
    class PO
    {
        System.Text.Encoding SJIS = System.Text.Encoding.GetEncoding(932);
        Dictionary dic = new Dictionary();

        Po poYarhl = new Po
        {
            Header = new PoHeader("Etrian Odyssey IV", "tradusquare@gmail.com", "es")
            {
                LanguageTeam = "TraduSquare",
            }
        };

        public void POExport(string toPO, int i)
        {
            if (toPO != "")
                poYarhl.Add(new PoEntry(dic.Transform(toPO, "dicFW2HW")) { Context = i.ToString() });
        }

        public void POWrite(string file)
        {
            poYarhl.ConvertTo<BinaryFormat>().Stream.WriteTo(file + ".po");
        }


        public void POImport(string poFile)
        {
            var poInstance = new BinaryFormat(new DataStream(poFile, FileOpenMode.Read)).ConvertTo<Po>();
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path.GetFileNameWithoutExtension(poFile), FileMode.Create), SJIS, true))
            {
                bw.Write((ushort)poInstance.Entries.Count);
                ushort textL = 0;
                foreach (var p in poInstance.Entries)
                {
                    bw.Write((ushort)((p.Text.Length * 2 + 1) + textL));
                    textL += (ushort)(p.Text.Length * 2 + 1);
                }
                foreach (var p in poInstance.Entries)
                {
                    char[] textW = dic.Transform(p.Text, "dicHW2FW").ToCharArray();
                    bw.Write(textW);
                    bw.Write((byte)0x0);
                }
            }
        }
    }
}
