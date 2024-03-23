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
        public RightTriangle(string nameTringle, Rib[] ribs, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles, string nameRightAngle) : base(nameTringle, ribs, angles, moreLines, moreAngles)
        {
           this.NameRightAngle = nameRightAngle;

        }

        public string NameRightAngle { get => nameRightAngle; set => nameRightAngle = value; }

        public void middle_of_yeter_equal_middle_of_yeter()
        {

        }
        public double Finding_Length_rib_By_Pythagorean_Theorem() //משפט פיתגורס
        {
            int j=0,y=0;
            double yeter = 0, nichav1 = 0, nichav2 = 0;//שם במשתנים כל אחד מהצלעות
            for (int i = 0; i < this.Ribs.Length; i++)
            {
                if (Convert.ToInt16(Ribs[i].DescriptionRib) == 1)
                {
                    yeter = Ribs[i].LenLine;
                    y = i;
                }
                if (Convert.ToInt16(Ribs[i].DescriptionRib) == 2)
                {
                    nichav1 = Ribs[i].LenLine;
                    j = i;
                }               
            }
            if (y == (j + 1))
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
