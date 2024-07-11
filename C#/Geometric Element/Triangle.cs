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
            this.Ribs = new Rib[3];
            this.Angles = new Angle[3];

            for (int i = 0; i < 3; i++)
            {
                Ribs[i] = new Rib();
                Angles[i] = new Angle();
            }

        }


        /// <summary>
        /// מציאת שטח
        /// </summary>
        /// <returns></returns>
        public double find_Area()
        {
            LineInShape? plump = this.MoreLines.FirstOrDefault(p => p.DescriptionLine == DescriptionLine.plumb);
            if (plump != null)
                return (plump.RibDest.LenLine * plump.LenLine ) / 2;
            return 0;
        }

        /// <summary>
        /// בדיקה אם המשולש שווה שוקיים
        /// </summary>
        /// <returns></returns>
        public bool check_Isosceles_Triangle()
        {
            foreach (LineInShape line in this.MoreLines)
            {
                if (line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.bisectsAngle
                    || line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.middle
                    || line.DescriptionLine == DescriptionLine.bisectsAngle && line.DescriptionLine == DescriptionLine.middle)
                {
                    return true;
                }
            }
            
            //זוויות הבסיס שוות
            foreach (Angle angle1 in this.Angles)
            {
                foreach (Angle angle2 in this.Angles)
                {
                    if (angle1 != angle2 && Is_equal<Angle>(angle1, angle2) ||angle1.ValueAngle==angle2.ValueAngle)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// בדיקה אם המשולש שווה צלעות
        /// </summary>
        /// <returns></returns>
        public bool check_Equilateral_Triangle()
        {
            if (check_Isosceles_Triangle()
                && (this.Angles[0].ValueAngle == 60.0
                || this.Angles[1].ValueAngle == 60.0
                || this.Angles[2].ValueAngle == 60.0))
                return true;
            if (Is_equal<Angle>(this.Angles[0], this.Angles[1])
                && Is_equal<Angle>(this.Angles[1], this.Angles[2]))
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
        /// <summary>
        /// חפיפה
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public bool Overlap(Triangle triangle2)
        {
            if (this.Overlap_Rib_Rib_Rib(triangle2)
                || this.Overlap_Rib_Angle_Rib(triangle2)
                || this.Overlap_Angle_Rib_Angle(triangle2))
                return true;
            return false;
        }

        /// <summary>
        /// חפיפה לפי צלע צלע צלע
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public  bool Overlap_Rib_Rib_Rib(Triangle triangle2)
        {
            //בניית מילון של זוגות של צלעות שוות
            Dictionary<Rib, Rib> overlapRibs = new Dictionary<Rib, Rib>();
            Triangle triangle1 = this;
            //מעבר על המשולש הראשון ובדיקה אם קיימות צלעות שוות בשתי המשולשים
            foreach (Rib rib_in_t1 in triangle1.Ribs.Except(overlapRibs.Keys))
            {
                Rib? eq_rib_in_t2 = triangle2.Ribs.Except(overlapRibs.Values).FirstOrDefault(x => x.LenLine == rib_in_t1.LenLine);
                Rib? re_rib_in_t2 = triangle2.Ribs.Except(overlapRibs.Values).FirstOrDefault(x => x.Is_equal<Rib>(rib_in_t1, x));

                if (eq_rib_in_t2 == null && re_rib_in_t2 == null)
                    return false;
                overlapRibs.Add(rib_in_t1, eq_rib_in_t2 ?? re_rib_in_t2!);
            }
            //אם קיימים 3 זוגות אזי המשולשים חופפים
            if (overlapRibs.Count() == 3)
                return true;
            return false;
        }

        /// <summary>
        /// חפיפה לפי צלע זווית צלע
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public bool Overlap_Rib_Angle_Rib(Triangle triangle2)
        {
            //בניית מילון של זוגות מסוג מחלקת העל אלמנט: או צלע או זווית
            Dictionary<Element, Element> overlapElement = new Dictionary<Element, Element>();
            Triangle triangle1 = this;
            //מוסיפים למילון את כל הזוגות של הצלעות השוות
            foreach (Rib rib_in_t1 in triangle1.Ribs.Except(overlapElement.Keys))
            {
                Rib? eq_rib_in_t2 = triangle2.Ribs.Except(overlapElement.Values.Where(a => a is Rib).Select(a => a as Rib)).FirstOrDefault(x => x!.LenLine == rib_in_t1.LenLine);
                Rib? re_rib_in_t2 = triangle2.Ribs.Except(overlapElement.Values.Where(a => a is Rib).Select(a => a as Rib)).FirstOrDefault(x => x!.Is_equal<Rib>(rib_in_t1, x));

                if (eq_rib_in_t2 == null && re_rib_in_t2 == null)
                    return false;
                overlapElement.Add(rib_in_t1, eq_rib_in_t2 ?? re_rib_in_t2!);
            }
            //מוסיפים למליון את כל הזוגות של הזוויות השוות
            foreach (Angle rib_in_t1 in triangle1.Angles.Except(overlapElement.Keys))
            {
                Angle? eq_rib_in_t2 = triangle2.Angles.Except(overlapElement.Values.Where(a => a is Angle).Select(a => a as Angle)).FirstOrDefault(x => x!.ValueAngle == rib_in_t1.ValueAngle);
                Angle? re_rib_in_t2 = triangle2.Angles.Except(overlapElement.Values.Where(a => a is Angle).Select(a => a as Angle)).FirstOrDefault(x => x!.Is_equal<Angle>(rib_in_t1, x));

                if (eq_rib_in_t2 == null && re_rib_in_t2 == null)
                    return false;
                overlapElement.Add(rib_in_t1, eq_rib_in_t2 ?? re_rib_in_t2!);
            }
            //בודקים אם יש ברצף צלע זווית צלע
            foreach (Element item in overlapElement.Keys)
            {
                if (item is Angle)
                {
                    if (overlapElement.ContainsKey(((Angle)item).Rib1) || overlapElement.ContainsValue(((Angle)item).Rib1))
                    {
                        if (overlapElement.ContainsKey(((Angle)item).Rib2) || overlapElement.ContainsValue(((Angle)item).Rib2))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// חפיפה לפי זווית צלע זווית
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public bool Overlap_Angle_Rib_Angle(Triangle triangle2)
        {
            //בניית מילון של זוגות מסוג מחלקת העל אלמנט: או צלע או זווית
            Dictionary<Element, Element> overlapElement = new Dictionary<Element, Element>();
            Triangle triangle1 = this;
            //מוסיפים למילון את כל הזוגות של הצלעות השוות
            foreach (Rib rib_in_t1 in triangle1.Ribs.Except(overlapElement.Keys))
            {
                Rib? eq_rib_in_t2 = triangle2.Ribs.Except(overlapElement.Values.Where(a => a is Rib).Select(a => a as Rib)).FirstOrDefault(x => x!.LenLine == rib_in_t1.LenLine);
                Rib? re_rib_in_t2 = triangle2.Ribs.Except(overlapElement.Values.Where(a => a is Rib).Select(a => a as Rib)).FirstOrDefault(x => x!.Is_equal<Rib>(rib_in_t1, x));

                if (eq_rib_in_t2 == null && re_rib_in_t2 == null)
                    return false;
                overlapElement.Add(rib_in_t1, eq_rib_in_t2 ?? re_rib_in_t2!);
            }
            //מוסיפים למליון את כל הזוגות של הזוויות השוות
            foreach (Angle rib_in_t1 in triangle1.Angles.Except(overlapElement.Keys))
            {
                Angle? eq_rib_in_t2 = triangle2.Angles.Except(overlapElement.Values.Where(a => a is Angle).Select(a => a as Angle)).FirstOrDefault(x => x!.ValueAngle == rib_in_t1.ValueAngle);
                Angle? re_rib_in_t2 = triangle2.Angles.Except(overlapElement.Values.Where(a => a is Angle).Select(a => a as Angle)).FirstOrDefault(x => x!.Is_equal<Angle>(rib_in_t1, x));

                if (eq_rib_in_t2 == null && re_rib_in_t2 == null)
                    return false;
                overlapElement.Add(rib_in_t1, eq_rib_in_t2 ?? re_rib_in_t2!);
            }
            //בודקים אם יש ברצף זווית צלע זווית
            foreach (Element item in overlapElement.Keys)
            {
                if (item is Rib)
                {
                    var angle1 = Find_matching_Angle(overlapElement, ((Rib)item).NameLine[0]);
                    if (angle1 != null)
                    {
                        var angle2 = Find_matching_Angle(overlapElement, ((Rib)item).NameLine[1]);
                        if (angle2 != null)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// בדיקה אם זווית שליד צלע מסוימת קיימת במילון
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="charOfLine"></param>
        /// <returns></returns>
        public Element Find_matching_Angle(Dictionary<Element, Element> dictionary, char charOfLine)
        {
            // בדיקת מפתחות
            var keyMatch = dictionary.Keys.Where(a => a is Angle).FirstOrDefault(x => ((Angle)x).NameAngle[1] == charOfLine);
            if (keyMatch != null)
                return keyMatch!;
            // בדיקת ערכים
            var valueMatch = dictionary.Values.Where(a => a is Angle).FirstOrDefault(x => ((Angle)x).NameAngle[1] == charOfLine);
            return valueMatch!;
        }
        /// <summary>
        /// השלמת זוויות במשולש
        /// </summary>
        public void Completing_Angles()
        {
            if (this.Angles[0].ValueAngle > 0 && this.Angles[1].ValueAngle > 0)
                this.Angles[2].ValueAngle = 180 - (this.Angles[2].ValueAngle + this.Angles[1].ValueAngle);
            else if (this.Angles[1].ValueAngle > 0 && this.Angles[2].ValueAngle > 0)
                this.Angles[0].ValueAngle = 180 - (this.Angles[2].ValueAngle + this.Angles[1].ValueAngle);
            else if (this.Angles[2].ValueAngle > 0 && this.Angles[0].ValueAngle > 0)
                this.Angles[1].ValueAngle = 180 - (this.Angles[2].ValueAngle + this.Angles[0].ValueAngle);
        }
    }
}
