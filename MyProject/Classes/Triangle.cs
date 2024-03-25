using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Triangle
    {
        private string nameTringle;
        private Rib[] ribs;
        private Angle[] angles;
        private List<Line> moreLines;
        private List<Angle> moreAngles;

        public string NameTringle { get => nameTringle; set => nameTringle = value; }
        internal Angle[] Angles { get => angles; set => angles = value; }
        internal List<Line> MoreLines { get => moreLines; set => moreLines = value; }
        internal List<Angle> MoreAngles { get => moreAngles; set => moreAngles = value; }
        public Rib[] Ribs { get => ribs; set => ribs = value; }

        public Triangle()
        {

        }
        public Triangle(string nameTringle, Rib[] ribs, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles)
        {
            this.nameTringle = nameTringle;
            this.ribs = ribs;
            this.angles = angles;
            this.moreLines = moreLines;
            this.moreAngles = moreAngles;
        }

        public double find_Area()  //מציאת שטח
        {
            foreach (LineInTriangle line in this.moreLines)
            {
                if (line.DescriptionLine==DescriptionLine.plumb) 
                {
                    return (line.RibDest.LenLine * line.LenLine)/2;
                }                
            }
            return 0.0;
        }
        public double find_Perimeter()//מציאת היקף
        {
            double perimeter=0;
            foreach (Rib rib in this.ribs)
            {
                perimeter += rib.LenLine;
            } 
            return perimeter;
        }
        public bool check_Isosceles_Triangle() //בדיקה אם המשולש שווה שוקיים
        {
            foreach (LineInTriangle line in this.moreLines)
            {
                if (line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.bisectsAngle || line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.middle || line.DescriptionLine == DescriptionLine.bisectsAngle && line.DescriptionLine == DescriptionLine.middle)
                {
                    return true;
                }
            }
            //זויות הבסיס שוות
            if (this.angles[0].ValueAngle == this.angles[1].ValueAngle || this.angles[0].ValueAngle == this.angles[2].ValueAngle || this.angles[1].ValueAngle == this.angles[2].ValueAngle)
                return true;

            return false;
        }
        public bool check_Equilateral_Triangle()//בדיקה אם המשולש שווה צלעות
        {
            if (check_Isosceles_Triangle() &&( this.angles[0].ValueAngle == 60.0|| this.angles[1].ValueAngle == 60.0||this.angles[2].ValueAngle == 60.0))
                return true;
            return false;
        }
        public bool check_RightTriangle()
        {
            foreach (Angle angle in this.angles)
            {
                if (angle.ValueAngle == 90.0)
                    return true;
            }
            return false;
        }
        public double Completing_Angles; //השלמת זוויות

            // בתוך כל פונקציה נעשה זימונים של הרבה פונקציות, כל פונקצייה- דרך אחרת להוכיח
            //אולי שכל פונקציה תחזיר מערך של המשפטים שהיא השתמשה בהם בשביל הפלט של ההוכחה
            //או שכל משפט זה פונקציה ואז נעשה טבלה של כל המשפטים, כל משפט עם הפונקצציה שלו
        }
}
