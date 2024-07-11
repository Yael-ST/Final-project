using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject;
using MyProject.Text_Of_Exercise;

namespace MyProject.Classes
{
   
    internal class IsoscelesTriangle : Triangle
    {
        
        public IsoscelesTriangle()
        {

        }
        public void Set_attributes_of_the_isosceles_triangle()
        {
            Rib shok1 = this.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.shok1)!;
            Rib shok2 = this.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.shok2)!;

            //שוקיים שווים
            if (!Is_equal<Rib>(shok1, shok2))
            {
                Relation relation1 = new Relation() { obj1 = shok1, obj2 = shok2, relation = 1 };
                GlobalVariable.ListAllRelations.Add(relation1);

                shok1.LenLine = shok1.LenLine == 0 ? shok2.LenLine : shok1.LenLine;
                shok2.LenLine = shok2.LenLine == 0 ? shok1.LenLine : shok2.LenLine;
            }
           
            //זוויות הבסיס שוות
            string[] angles = new string[2];
            angles = find_names_of_base_angles();
            Angle? angle1 = this.Angles.FirstOrDefault(p => p.NameAngle == angles[0])!;
            Angle? angle2 = this.Angles.FirstOrDefault(p => p.NameAngle == angles[1])!;

            if(!Is_equal<Angle>(angle1!, angle2!))
            {
                Relation relation1 = new Relation() { obj1 = angle1!, obj2 = angle2!, relation = 1 };
                GlobalVariable. ListAllRelations.Add(relation1);
                angle1.ValueAngle = angle1.ValueAngle == 0 ? angle2.ValueAngle : angle1.ValueAngle;
                angle2.ValueAngle = angle2.ValueAngle == 0 ? angle1.ValueAngle : angle2.ValueAngle;
            }
            LinesInIsoscelesTriangle();

        }
        /// <summary>
        /// אנך חוצה זווית ותיכון מתלכדים במשו"ש
        /// </summary>
        public void LinesInIsoscelesTriangle()
        {
            foreach (LineInShape line in this.MoreLines)
            {
                line.DescriptionLine = DescriptionLine.plumb | DescriptionLine.middle
                    | DescriptionLine.bisectsAngle;
            }
            
        }

        //מציאת שמות זוויות הבסיס
        public string[] find_names_of_base_angles()
        {
            Rib shok1 = this.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.shok1)!;
            Rib shok2 = this.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.shok2)!;
            string[] angels = new string[2];
            angels[0] = string.Concat(shok1.NameLine, shok2.NameLine[1]);
            angels[1] = string.Concat(shok2.NameLine, shok1.NameLine[1]);
            return angels;
        }


     
    }
}
