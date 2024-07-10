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
        TextAnalysis CreateElement { get; set; } 
        private Rib shok1;
        private Rib shok2;
        public IsoscelesTriangle()
        {

        }
        public void Set_attributes_of_the_isosceles_triangle()
        {
            //שוקיים שווים
            if (!Is_equal<Rib>(this.Shok1, this.Shok2))
            {
                Relation relation1 = new Relation() { obj1 = this.Shok1, obj2 = this.Shok2, relation = 1 };
                this.ListAllRelations.Add(relation1);
                if (this.shok1.LenLine == 0)
                    this.shok1.LenLine = this.shok2.LenLine;
                if (this.shok2.LenLine == 0)
                    this.shok2.LenLine = this.shok1.LenLine;
            }
           

            //זוויות הבסיס שוות
            string[] angles = new string[2];
            angles = find_names_of_base_angles();
            Angle? angle1 = this.Angles.FirstOrDefault(p => p.NameAngle == angles[0])!;
            Angle? angle2 = this.Angles.FirstOrDefault(p => p.NameAngle == angles[1])!;

            if(Is_equal<Angle>(angle1!, angle2!))
            {
                Relation relation1 = new Relation() { obj1 = angle1!, obj2 = angle2!, relation = 1 };
                this.ListAllRelations.Add(relation1);
                if (angle1.ValueAngle == 0)
                    angle1.ValueAngle = angle2.ValueAngle;
                if (angle2.ValueAngle == 0)
                    angle1.ValueAngle = angle2.ValueAngle;
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
            CreateElement.Create_middle("", 0);
            CreateElement.Create_bisects_angle("", 0);
            CreateElement.Create_plumb("", 0);

        }

        //מציאת שמות זוויות הבסיס
        public string[] find_names_of_base_angles()
        {
            string[] angels = new string[2];
            angels[0] = this.Shok1.NameLine + this.Shok1.NameLine[1].ToString();
            angels[1] = this.Shok2.NameLine + this.Shok1.NameLine[1].ToString();

            return angels;
        }


        internal Rib Shok2 { get => shok2; set => shok2 = value; }
        internal Rib Shok1 { get => shok1; set => shok1 = value; }
    }
}
