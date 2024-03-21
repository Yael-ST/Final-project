using System;
using System.Collections.Generic;
using System.Linq;
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
        internal Rib[] Ribs { get => ribs; set => ribs = value; }

        public Triangle()
        {

        }
        public Triangle(string nameTringle, Rib[] ribs, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles)
        {
            this.nameTringle = nameTringle;
            this.Ribs = ribs;
            this.angles = angles;
            this.moreLines = moreLines;
            this.moreAngles = moreAngles;
        }

        public double find_Area;//מציאת שטח
        public double find_Perimeter;//מציאת היקף
        public bool check_Isosceles_Triangle;//בדיקה אם המשולש שווה שוקיים
        public bool check_Equilateral_Triangle; //בדיקה אם המשולש שווה צלעות
        public bool check_RightTriangle;//בדיקה אם המשולש ישר זווית
        public double Completing_Angles;//השלמת זוויות

        // בתוך כל פונקציה נעשה זימונים של הרבה פונקציות, כל פונקצייה- דרך אחרת להוכיח
        //אולי שכל פונקציה תחזיר מערך של המשפטים שהיא השתמשה בהם בשביל הפלט של ההוכחה
        //או שכל משפט זה פונקציה ואז נעשה טבלה של כל המשפטים, כל משפט עם הפונקצציה שלו
    }
}
