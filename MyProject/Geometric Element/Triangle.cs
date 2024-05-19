using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Triangle : Shape
    {
       
        public Triangle()
        {

        }

        public double find_Area()  //מציאת שטח
        {
            
            LineInTriangle plump = this.MoreLines.First(p => p.DescriptionLine == DescriptionLine.plumb);
            if(plump != null)
                return (plump.RibDest.LenLine * plump.LenLine) / 2;
            return 0;
        }
        public double find_Perimeter()//מציאת היקף
        {
            double perimeter=0;
            foreach (Rib rib in this.Ribs)
            {
                perimeter += rib.LenLine;
            } 
            return perimeter;
        }
        public bool check_Isosceles_Triangle() //בדיקה אם המשולש שווה שוקיים
        {
            foreach (LineInTriangle line in this.MoreLines)
            {
                if (line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.bisectsAngle || line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.middle || line.DescriptionLine == DescriptionLine.bisectsAngle && line.DescriptionLine == DescriptionLine.middle)
                {
                    return true;
                }
            }
            //זויות הבסיס שוות
            if (this.Angles[0].ValueAngle == this.Angles[1].ValueAngle || this.Angles[0].ValueAngle == this.Angles[2].ValueAngle || this.Angles[1].ValueAngle == this.Angles[2].ValueAngle)
                return true;

            //חיפוש זוויות שוות
            foreach (Angle angle1 in this.Angles)
            {
                var thisRelation = (angle1, 1);

                foreach (Angle angle2 in this.Angles)
                {
                    if (angle1!=angle2&&angle2.GetMyRelations().Contains(thisRelation))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool check_Equilateral_Triangle()//בדיקה אם המשולש שווה צלעות
        {
            if (check_Isosceles_Triangle() &&( this.Angles[0].ValueAngle == 60.0|| this.Angles[1].ValueAngle == 60.0||this.Angles[2].ValueAngle == 60.0))
                return true;
            return false;
        }
        public bool check_RightTriangle()
        {
            foreach (Angle angle in this.Angles)
            {
                if (angle.ValueAngle == 90.0)
                    return true;
            }
            return false;
        }
        public void Overlap(Triangle triangle2)
        {
            //צלע זווית צלע
            //חיפוש זוויות שוות
            string nameAngle1, nameAngle2;


            foreach (Angle angle1 in this.Angles)
            {
                var thisRelation = (angle1, 1);

                foreach (Angle angle2 in triangle2.Angles)
                {
                    if (angle2.GetMyRelations().Contains(thisRelation))
                    {
                        nameAngle1 = angle2.NameAngle;
                        nameAngle2 = angle1.NameAngle;
                        break;
                    }
                }
            }

            //חיפוש צלעות שוות
          //  string nameRib1 = nameAngle1[0].ToString() + nameAngle1[1].ToString();
          //  string nameRib2 = nameAngle2[0].ToString() + nameAngle2[1].ToString();
          //  Rib rib1 = this.Ribs.First(x => x.NameLine == nameRib1);
          //  Rib rib2 = this.Ribs.First(x => x.NameLine == nameRib2);
          //  var thisRelation2 = (rib2, 1);
          //  if (rib1.GetMyRelations().Contains(thisRelation2))
          //  {

          //  }

        }

        public double Completing_Angles; //השלמת זוויות

            // בתוך כל פונקציה נעשה זימונים של הרבה פונקציות, כל פונקצייה- דרך אחרת להוכיח
            //אולי שכל פונקציה תחזיר מערך של המשפטים שהיא השתמשה בהם בשביל הפלט של ההוכחה
            //או שכל משפט זה פונקציה ואז נעשה טבלה של כל המשפטים, כל משפט עם הפונקצציה שלו
        }
  
}
