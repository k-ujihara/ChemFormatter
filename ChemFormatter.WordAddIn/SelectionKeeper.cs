using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;

namespace ChemFormatter.WordAddIn
{
    public struct SelectionKeeper
    {
        public int Start { get; set; }
        public int End { get; set; }

        public SelectionKeeper(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
