using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchanter
{
    class TBL
    {
        System.Text.Encoding SJIS = System.Text.Encoding.GetEncoding(932);
        List<UInt16> pointerList = new List<UInt16>();
        List<string> textList = new List<string>();
        public void Initialize(string f)
        {
            Interpeter(f);
            WriteToPo(f);
        }

        private void Interpeter(string f)
        {
            using (var br = new BinaryReader(new FileStream(f, FileMode.Open), SJIS, true))
            {
                int pointerCount = br.ReadUInt16();
                long textPosition = (pointerCount * 2) + 2;
                for (int i = 0; i < pointerCount; i++)
                {
                    pointerList.Add(br.ReadUInt16());
                }
                br.BaseStream.Position = textPosition;

                
                for (int i = 0; i < pointerCount; i++)
                {
                    var sb = new StringBuilder();
                    while (br.BaseStream.Position < pointerList[i] + textPosition)
                    {
                        sb.Append(br.ReadChar());
                    }
                    textList.Add(sb.ToString());
                }
            }
        }

        private void WriteToPo(string f)
        {
            PO po = new PO();
            int c = 0;
            foreach (var t in textList)
            {
                po.POExport(t, c);
                c++;
            }
            po.POWrite(f);
        }
    }
}
