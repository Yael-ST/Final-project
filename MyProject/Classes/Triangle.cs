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
        private Side[] sides;
        private Angle[] angles;
        private List<Line> moreLines;
        private List<Angle> moreAngles;

        public string NameTringle { get => nameTringle; set => nameTringle = value; }
        internal Side[] Sides { get => sides; set => sides = value; }
        internal Angle[] Angles { get => angles; set => angles = value; }
        internal List<Line> MoreLines { get => moreLines; set => moreLines = value; }
        internal List<Angle> MoreAngles { get => moreAngles; set => moreAngles = value; }

        public Triangle()
        {

        }
        public Triangle(string nameTringle, Side[] sides, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles)
        {
            NameTringle = nameTringle;
            this.sides = sides;
            Angles = angles;
            MoreLines = moreLines;
            MoreAngles = moreAngles;
        }

        public double findArea;//מציאת שטח
        public double findPerimeter;//מציאת היקף
        public bool checkIsoscelesTriangle;//בדיקה אם המשולש שווה שוקיים
        public bool checkEquilateralTriangle; //בדיקה אם המשולש שווה צלעות
        public bool checkRightTriangle;//בדיקה אם המשולש ישר זווית

        // בתוך כל פונקציה נעשה זימונים של הרבה פונקציות, כל פונקצייה- דרך אחרת להוכיח
        //אולי שכל פונקציה תחזיר מערך של המשפטים שהיא השתמשה בהם בשביל הפלט של ההוכחה
        //או שכל משפט זה פונקציה ואז נעשה טבלה של כל המשפטים, כל משפט עם הפונקצציה שלו
    }
}
