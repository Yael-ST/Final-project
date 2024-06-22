using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class RightTriangle : Triangle
    {
        private string nameRightAngle;

        public RightTriangle()
        {

            var angle = this.Angles.FirstOrDefault(x => x.NameAngle == this.NameRightAngle);
            if (angle != null)
            {
                angle.ValueAngle = 90;
            }

            //התיכון ליתר שווה למחצית היתר
            LineInTriangle middle = this.MoreLines.First(p => p.DescriptionLine == DescriptionLine.middle);
            if(middle != null) 
            {
                var thisRelation = (this.Ribs.First(p => p.DescriptionRib == DescriptionRib.yeter), 0.5);
                if (middle.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = middle, obj2 = this.Ribs.First
                        (p => p.DescriptionRib == DescriptionRib.yeter), relation = 0.5 };
                    this.ListAllRelations.Add(relation1);
                }
            }            
        }

        public string NameRightAngle { get => nameRightAngle; set => nameRightAngle = value; }


        public double Finding_Length_rib_By_Pythagorean_Theorem() //משפט פיתגורס
        {

            double nichav1=this.Ribs.First(x=>x.DescriptionRib==DescriptionRib.nichav).LenLine;
            double nichav2=this.Ribs.Last(x=>x.DescriptionRib==DescriptionRib.nichav).LenLine;
            double yeter=this.Ribs.First(x=>x.DescriptionRib==DescriptionRib.yeter).LenLine;

            if (yeter == 0)
                return Math.Sqrt(nichav1 * nichav1 + nichav2 * nichav2);
            if (nichav1 == 0)
                return Math.Sqrt(yeter * yeter - nichav2 * nichav2 );
            if (nichav2 == 0)
                return Math.Sqrt(yeter * yeter - nichav1 * nichav1 );

            return -1;
        }          
    }
}
