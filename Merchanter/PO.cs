using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace Merchanter
{
    class PO
    {
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
            poYarhl.Add(new PoEntry(dic.Transform(toPO, "dicFW2HW" )) { Context = i.ToString() });
        }

        public void POWrite(string file)
        {
            poYarhl.ConvertTo<BinaryFormat>().Stream.WriteTo(file + ".po");
        }
    }
}
