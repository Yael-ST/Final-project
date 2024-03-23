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
        public IsoscelesTriangle(string nameTringle, Rib[] ribs, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles, string nameShok1, string nameShok2) :base(nameTringle, ribs, angles, moreLines, moreAngles)
        {
            this.nameShok1 = nameShok1;
            this.nameShok2 = nameShok2;
            //זוויות הבסיס שוות
            foreach (LineInTriangle line in this.MoreLines) //אנך & חוצה זוית & תיכון- מתלכדים
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
