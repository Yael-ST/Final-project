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
                if (!shok1.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = shok1, obj2 = shok2, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                }
            }
            //זוויות הבסיס שוות
            string[] angles=new string[2];
            angles = find_names_of_base_angles();
            Angle angle1 = this.Angles.First(p=>p.NameAngle==angles[0]);
            Angle angle2 = this.Angles.First(p => p.NameAngle == angles[1]);

            var thisRelation2 = (angle1, 1);
            if (!angle1.GetMyRelations().Contains(thisRelation2))
            {
                Relation relation1 = new Relation() { obj1 = angle1, obj2 = angle2, relation = 1 };
                this.ListAllRelations.Add(relation1);
            }


            //אנך & חוצה זוית & תיכון- מתלכדים
            /*לבדוק מה עם זה*/
            foreach (LineInTriangle line in this.MoreLines)
            {
                line.DescriptionLine = DescriptionLine.middle;
                line.DescriptionLine = DescriptionLine.plumb;
                line.DescriptionLine = DescriptionLine.bisectsAngle;
            }
        }
        public string NameShok1 { get => nameShok1; set => nameShok1 = value; }
        public string NameShok2 { get => nameShok2; set => nameShok2 = value; }

        //מציאת שמות זוויות הבסיס
        public string[] find_names_of_base_angles()
        {
            string[] angels = new string[2];
            angels[0] = this.nameShok1[0].ToString() + this.nameShok1[1].ToString() + this.nameShok2[1].ToString(); // ABC
            angels[1] = this.nameShok2[0].ToString() + this.nameShok2[1].ToString() + this.NameShok1[1].ToString(); // ACB

            return angels;
        }
    }
}
