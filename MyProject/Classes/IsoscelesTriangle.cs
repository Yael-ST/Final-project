using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class IsoscelesTriangle : Triangle
    {
        private string nameShok1;
        private string nameShok2;
        public IsoscelesTriangle()
        {
            //זוויות הבסיס שוות

            //אנך & חוצה זוית & תיכון- מתלכדים
            foreach (LineInTriangle line in this.MoreLines)
            {
                line.DescriptionLine = DescriptionLine.middle;
                line.DescriptionLine = DescriptionLine.plumb;
                line.DescriptionLine = DescriptionLine.bisectsAngle;
            }
        }
        public string NameShok1 { get => nameShok1; set => nameShok1 = value; }
        public string NameShok2 { get => nameShok2; set => nameShok2 = value; }

  
    }
}
