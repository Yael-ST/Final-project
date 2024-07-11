using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class RightTriangle : Triangle
    {
        private Angle rightAngle;

        public RightTriangle()
        {

        }

        internal Angle RightAngle { get => rightAngle; set => rightAngle = value; }

        public void Set_attributes_of_the_Right_triangle()
        {
            this.rightAngle.ValueAngle = 90;

            //התיכון ליתר שווה למחצית היתר
            The_middle_for_yeter();
        }
        public double? Finding_Length_rib_By_Pythagorean_Theorem() //משפט פיתגורס
        {
            double? nichav1 = this.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.nichav)?.LenLine;
            double? nichav2 = this.Ribs.LastOrDefault(x => x.DescriptionRib == DescriptionRib.nichav)?.LenLine;
            double? yeter = this.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.yeter)?.LenLine;
            double? temp = -1;
            if (yeter == 0)
                temp = nichav1 * nichav1 + nichav2 * nichav2;
            if (nichav1 == 0)
                temp = yeter * yeter - nichav2 * nichav2;
            if (nichav2 == 0)
                temp = yeter * yeter - nichav1 * nichav1;
            return Math.Sqrt((double)temp!);
        }
        public void The_middle_for_yeter()
        {
            LineInShape middle = this.MoreLines.FirstOrDefault(p => p.DescriptionLine == DescriptionLine.middle)!;
            if (middle != null)
            {
                Rib yeter = this.Ribs.FirstOrDefault(p => p.DescriptionRib == DescriptionRib.yeter)!;
                if (yeter != null)
                {
                    var thisRelation = (yeter, 0.5);
                    if (middle.GetMyRelations().Contains(thisRelation))
                    {
                        Relation relation1 = new Relation()
                        {
                            obj1 = middle,
                            obj2 = yeter,
                            relation = 0.5
                        };
                        GlobalVariable.ListAllRelations.Add(relation1);
                    }
                }
            }
        }
    }
}
