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
            //שוקיים שווים
            Rib shok1 = this.Ribs.First(p => p.NameLine ==nameShok1);
            Rib shok2 = this.Ribs.First(p=>p.NameLine ==nameShok2);
            if (shok1 != null)
            {
                var thisRelation = (shok2, 1);
                if (shok1.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = shok1, obj2 = shok2, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                }
            }
            //זוויות הבסיס שוות



            //מציאת שמות הזוויות


            var uniqueChars = nameShok1.Except(nameShok2).Union(nameShok2.Except(nameShok1));
            string nameAngle1,nameAngle2;



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
