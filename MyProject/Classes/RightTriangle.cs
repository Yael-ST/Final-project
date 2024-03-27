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
            //התיכון ליתר שווה למחצית היתר
            LineInTriangle middle = this.MoreLines.First(p => p.DescriptionLine == DescriptionLine.middle);
            if(middle != null) 
            {
                var thisRelation = (this.Ribs.First(p => p.DescriptionRib == DescriptionRib.yeter), 0.5);
                if (middle.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = middle, obj2 = this.Ribs.First(p => p.DescriptionRib == DescriptionRib.yeter), relation = 0.5 };
                    this.ListAllRelations.Add(relation1);
                }
            }

            
        }

        public string NameRightAngle { get => nameRightAngle; set => nameRightAngle = value; }


        public double Finding_Length_rib_By_Pythagorean_Theorem() //משפט פיתגורס
        {
            int j=0,y=0;
            double yeter = 0, nichav1 = 0, nichav2 = 0;//שם במשתנים כל אחד מהצלעות
            for (int i = 0; i < this.Ribs.Length; i++)
            {
                if (Ribs[i].DescriptionRib==DescriptionRib.yeter)
                {
                    yeter = Ribs[i].LenLine;
                    y = i;
                }
                if (Ribs[i].DescriptionRib == DescriptionRib.nichav)
                {
                    nichav1 = Ribs[i].LenLine;
                    j = i;
                }               
            }
            if (y == (j + 1)) //בודק מה הניצב השני
                nichav2 = Ribs[2].LenLine;
            else
                nichav2 = Ribs[3].LenLine;
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
