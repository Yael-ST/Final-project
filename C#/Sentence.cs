using IronXL.Formatting;
using MyProject.Classes;
using MyProject.Text_Of_Exercise;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
//using static IronOcr.OcrResult;
//using static IronPython.Modules._ast;

namespace MyProject
{
    internal class Sentence : Element
    {
        Solving solving = new Solving();

        public int Id { get; set; }
        public string Text { get; set; }
        public Delegate FunctionPointer { get; set; }
        public bool IsFinalReached { get; set; }
        public Element GeometricElement { get; set; }
        public List<Sentence> myChildren;

        public Sentence()
        {
            myChildren = new List<Sentence>();
        }

        #region פונקציות ליצירת המשפטים
        public Sentence Create_sentence_Completing_Angles(Element geometric_Element)
        {
            return new Sentence() { Id = 0, Text = "השלמת זוויות במשולש", FunctionPointer = Completing_Angles, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Shoks_is_equal(Element geometric_Element)
        {
            return new Sentence() { Id = 1, Text = "אם השוקיים שווים המשולש הוא שוה שוקיים", FunctionPointer = Shoks_is_equal, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Basic_angles_is_equal(Element geometric_Element)
        {
            return new Sentence() { Id = 2, Text = "אם זוויות הבסיס שוות המשולש הוא שווה שוקיים", FunctionPointer = Basic_angles_is_equal, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Lines_in_IsoscelesTriangle_is_converge(Element geometric_Element)
        {
            return new Sentence() { Id = 3, Text = "תיכון גובה וחוצה זווית מתלכדים במשולש שווה שוקיים", FunctionPointer = Lines_in_triangle_is_converge, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_if_middle_and_bisectsAngle_is_converge(Element geometric_Element)
        {
            return new Sentence() { Id = 4, Text = "אם תיכון וחוצה זווית מתלכדים אזי המשולש הוא שוה שוקיים", FunctionPointer = if_middle_and_bisectsAngle_is_converge_the_triangle_is_IsoscelesTriangle, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_if_middle_and_plumb_is_converge(Element geometric_Element)
        {
            return new Sentence() { Id = 5, Text = "אם תיכון וגובה מתלכדים אזי המשולש הוא שוה שוקיים", FunctionPointer = if_middle_and_plumb_is_converge_the_triangle_is_IsoscelesTriangle, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_if_bisectsAngle_and_plumb_is_converge(Element geometric_Element)
        {
            return new Sentence() { Id = 6, Text = "אם גובה וחוצה זווית מתלכדים אזי המשולש הוא שוה שוקיים", FunctionPointer = if_bisectsAngle_and_plumb_is_converge_the_triangle_is_IsoscelesTriangle, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Overlap_Rib_Angle_Rib(Element geometric_Element)
        {
            return new Sentence() { Id = 7, Text = "המשולשים חופפים לפי צלע זווית צלע", FunctionPointer = Overlap_Rib_Angle_Rib, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Overlap_Rib_Rib_Rib(Element geometric_Element)
        {
            return new Sentence() { Id = 8, Text = "המשולשים חופפים לפי צלע צלע צלע", FunctionPointer = Overlap_Rib_Rib_Rib, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Overlap_Angle_Rib_Angle(Element geometric_Element)
        {
            return new Sentence() { Id = 9, Text = "המשולשים חופפים לפי זווית צלע זווית", FunctionPointer = Overlap_Angle_Rib_Angle, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_The_middle_for_yeter(Element geometric_Element)
        {
            return new Sentence() { Id = 10, Text = "במשולש ישר זווית התיכון ליתר שווה למחצית היתר", FunctionPointer = The_middle_for_yeter, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_The_middle_for_yeter_the_reverse_sentence(Element geometric_Element)
        {
            return new Sentence() { Id = 11, Text = "אם התיכון ליתר שווה למחצית היתר אזי המשולש ישר זווית", FunctionPointer = The_middle_for_yeter_the_reverse_sentence, IsFinalReached = false };
        }
        public Sentence Create_sentence_If_triangle_is_IsoscelesTriangle_and_oneOfThEAngles_equal_to_60(Element geometric_Element)
        {
            return new Sentence() { Id = 12, Text = "אם משולש הוא שווה שוקיים ואחת הזוויות שלו שווה 60 אזי המשולש הוא שווה צלעות", FunctionPointer = If_triangle_is_IsoscelesTriangle_and_oneOfThEAngles_equal_to_60, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_If_2_of_angles_equal_to60(Element geometric_Element)
        {
            return new Sentence() { Id = 13, Text = "אם במשולש קיימות לפחות 2 זוויות ששות 60 המשולש הוא שווה צלעות", FunctionPointer = If_2_of_angles_equal_to60, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Pythagorean_Theorem(Element geometric_Element)
        {
            return new Sentence() { Id = 14, Text = "משפט פיתגורס", FunctionPointer = Finding_Length_rib_By_Pythagorean_Theorem, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_An_angle_equal_to_90(Element geometric_Element)
        {
            return new Sentence() { Id = 16, Text = "אם אחת מזויות משולש שווה 90 המשולש הוא משולש ישר זווית...", FunctionPointer = An_angle_equal_to_90, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_A_nice_triangle(Element geometric_Element)
        {
            return new Sentence() { Id = 17, Text = "במשולש ישר זווית שזויותיו החדות שוות 60 ו-30 הניצב שמול הזווית שגודלה 30 שווה למחצית היתר  ", FunctionPointer = Nice_triangle, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_A_nice_triangle__reverse_sentence(Element geometric_Element)
        {
            return new Sentence() { Id = 18, Text = "אם במשולש ישר זווית אחד הניצבים שווה באורכו למחצית היתר אז הזווית שמול הניצב שווה 30", FunctionPointer = Nice_triangle_the__reverse_sentence, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_find_Perimeter(Element geometric_Element)
        {
            return new Sentence() { Id = 19, Text = "מציאת היקף", FunctionPointer = Find_Perimeter, GeometricElement = geometric_Element, IsFinalReached = false };
        }
        public Sentence Create_sentence_Complete_all_my_relations(Element geometric_Element)
        {
            return new Sentence() { Id = 19, Text = "הוכח שהם שווים", FunctionPointer = CompleteAllMyRelations<Element>, GeometricElement = geometric_Element, IsFinalReached = false };

        }
        #endregion

        /// <summary>
        /// מציאת היקף
        /// </summary>
        /// <returns></returns>
        public bool Find_Perimeter(Shape shape)
        {
            if (shape.Perimeter != 0) { return false; }
            double perimeter = 0;
            foreach (Rib rib in shape.Ribs)
            {
                if (rib.LenLine == 0)
                    return false;
                perimeter += rib.LenLine;
            }
            shape.Perimeter = perimeter;

            Console.WriteLine("the perimeter of shape " + shape.Name + " is: " + shape.Perimeter);
            return true;
        }

        /// <summary>
        /// השלמת זוויות במשולש
        /// </summary>
        public void Completing_Angles(Triangle t)
        {
            if (t.Angles[0].ValueAngle > 0 && t.Angles[1].ValueAngle > 0)
                t.Angles[2].ValueAngle = 180 - (t.Angles[2].ValueAngle + t.Angles[1].ValueAngle);
            else if (t.Angles[1].ValueAngle > 0 && t.Angles[2].ValueAngle > 0)
                t.Angles[0].ValueAngle = 180 - (t.Angles[2].ValueAngle + t.Angles[1].ValueAngle);
            else if (t.Angles[2].ValueAngle > 0 && t.Angles[0].ValueAngle > 0)
                t.Angles[1].ValueAngle = 180 - (t.Angles[2].ValueAngle + t.Angles[0].ValueAngle);
        }

        /// <summary>
        /// בדיקה אם יש שתי צלעות שוות במשולש
        /// </summary>
        /// <param name="t"></param>
        public void Shoks_is_equal(Triangle t)
        {
            if (t is IsoscelesTriangle)
                return;
            foreach (Rib rib1 in t.Ribs)
            {
                foreach (Rib rib2 in t.Ribs)
                {
                    if (Is_equal<Rib>(rib1, rib2))
                    {
                        Create_Isosceles_Triangle(t, null!, rib1, rib2);
                    }
                }
            }
        }

        /// <summary>
        /// בדיקה אם זוויות הבסיס שוות
        /// </summary>
        /// <param name="t"></param>
        public void Basic_angles_is_equal(Triangle t)
        {
            if (t is IsoscelesTriangle)
                return;
            foreach (Angle angle1 in t.Angles)
            {
                foreach (Angle angle2 in t.Angles)
                {
                    if (angle1 != angle2 && Is_equal<Angle>(angle1, angle2) || angle1.ValueAngle == angle2.ValueAngle)
                    {
                        string name_basic_rib = (angle1.NameAngle[1] + angle2.NameAngle[1]).ToString();
                        Rib r = t.Ribs.FirstOrDefault(x => x.NameLine == name_basic_rib)!;
                        Create_Isosceles_Triangle(t, r, null!, null!);
                    }
                }
            }
        }

        /// <summary>
        /// במשולש שווה שוקיים הישרים מתלכדים
        /// </summary>
        /// <param name="t"></param>
        public bool Lines_in_triangle_is_converge(Triangle t)
        {
            if (t is IsoscelesTriangle)
            {
                foreach (LineInShape line in t.MoreLines)
                {
                    if (line!.DescriptionLine == DescriptionLine.middle)
                    {
                        if (line.DescriptionLine != DescriptionLine.plumb)
                            Create_plumb(line);
                        if (line.DescriptionLine != DescriptionLine.bisectsAngle)
                            Create_bisectsAngle(line);
                        return true;
                    }
                    if (line!.DescriptionLine == DescriptionLine.bisectsAngle)
                    {
                        if (line.DescriptionLine != DescriptionLine.plumb)
                            Create_plumb(line);
                        if (line.DescriptionLine != DescriptionLine.middle)
                            Create_middle(line);
                        return true;
                    }
                    if (line!.DescriptionLine == DescriptionLine.plumb)
                    {
                        if (line.DescriptionLine != DescriptionLine.bisectsAngle)
                            Create_bisectsAngle(line);
                        if (line.DescriptionLine != DescriptionLine.middle)
                            Create_middle(line);
                        return true;
                    }

                }
            }
            return false;
        }

        /// <summary>
        /// אם תיכון וגובה מתלכדים המשולש הוא שוש
        /// </summary>
        /// <param name="t"></param>
        public void if_middle_and_plumb_is_converge_the_triangle_is_IsoscelesTriangle(Triangle t)
        {
            if (t is IsoscelesTriangle)
                return;
            foreach (LineInShape line in t.MoreLines)
            {
                if (line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.middle)
                {
                    Create_Isosceles_Triangle(t, line.RibDest, null!, null!);
                    Create_bisectsAngle(line);
                }
            }
        }

        /// <summary>
        /// אם תיכון וחוצה זווית מתלכדים המשולש הוא שוש
        /// </summary>
        /// <param name="t"></param>
        public void if_middle_and_bisectsAngle_is_converge_the_triangle_is_IsoscelesTriangle(Triangle t)
        {
            if (t is IsoscelesTriangle)
                return;
            foreach (LineInShape line in t.MoreLines)
            {
                if (line.DescriptionLine == DescriptionLine.bisectsAngle && line.DescriptionLine == DescriptionLine.middle)
                {
                    Create_Isosceles_Triangle(t, line.RibDest, null!, null!); ;
                    Create_plumb(line);
                }
            }
        }

        /// <summary>
        /// אם חוצה זווית וגובה מתלכדים המשולש הוא שוש
        /// </summary>
        /// <param name="t"></param>
        public void if_bisectsAngle_and_plumb_is_converge_the_triangle_is_IsoscelesTriangle(Triangle t)
        {
            if (t is IsoscelesTriangle)
                return;
            foreach (LineInShape line in t.MoreLines)
            {
                if (line.DescriptionLine == DescriptionLine.plumb && line.DescriptionLine == DescriptionLine.bisectsAngle)
                {
                    Create_Isosceles_Triangle(t, line.RibDest, null!, null!);
                    Create_middle(line);
                }
            }
        }

        /// <summary>
        ///   אם המשולש הוא שוש ואחת מהזויות היא 60 המושלש הוא משוצ
        /// </summary>
        /// <returns></returns>
        public void If_triangle_is_IsoscelesTriangle_and_oneOfThEAngles_equal_to_60(Triangle t)
        {
            if (t is EquilateralTriangle)
                return;
            bool one_of_angles_is_equal_60 = t.Angles[0].ValueAngle == 60.0 || t.Angles[1].ValueAngle == 60.0 || t.Angles[2].ValueAngle == 60.0;
            if (t is IsoscelesTriangle && one_of_angles_is_equal_60)
            {
                Create_Equilateral_Triangle(t);
            }
        }

        /// <summary>
        /// אם 2 מזויות המשולש שוות 60 המשולש הוא משוצ
        /// </summary>
        /// <returns></returns>
        public void If_2_of_angles_equal_to60(Triangle t)
        {
            if (t is EquilateralTriangle)
                return;
            if (Is_equal<Angle>(t.Angles[0], t.Angles[1]) && Is_equal<Angle>(t.Angles[1], t.Angles[2]))
            {
                Create_Equilateral_Triangle(t);
            }
        }

        /// <summary>
        /// התיכון ליתר שווה למחצית היתר
        /// </summary>
        /// <param name="t"></param>
        public void The_middle_for_yeter(Triangle t)
        {
            LineInShape middle = t.MoreLines.FirstOrDefault(p => p.DescriptionLine == DescriptionLine.middle)!;
            if (middle != null)
            {
                Rib yeter = t.Ribs.FirstOrDefault(p => p.DescriptionRib == DescriptionRib.yeter)!;
                if (yeter != null)
                {
                    var thisRelation = (yeter, 0.5);
                    if (!middle.GetMyRelations().Contains(thisRelation))
                    {
                        Relation relation1 = new Relation()
                        {
                            obj1 = middle,
                            obj2 = yeter,
                            relation = 0.5
                        };
                        GlobalVariable.ListAllRelations.Add(relation1);
                        yeter.LenLine = middle.LenLine != 0 ? 2 * middle.LenLine : yeter.LenLine;
                        middle.LenLine = yeter.LenLine != 0 ? 0.5 * yeter.LenLine : middle.LenLine;
                    }
                }
            }
        }

        /// <summary>
        /// התיכון ליתר שווה למחצית היתר- משפט הפוך
        /// </summary>
        /// <param name="t"></param>
        public void The_middle_for_yeter_the_reverse_sentence(Triangle t)
        {
            LineInShape middle = t.MoreLines.FirstOrDefault(x => x.DescriptionLine == DescriptionLine.middle)!;
            var thisRelation = (middle, 2);
            foreach (Rib rib in t.Ribs)
            {
                if (rib.GetMyRelations().Contains(thisRelation) || (rib.LenLine * 2) == middle.LenLine)
                {
                    Create_Equilateral_Triangle(t);
                }
            }
        }

        /// <summary>
        /// אם אחת מזוויות המשושל שווה 90 המשולש הוא משיז
        /// </summary>
        /// <param name="t"></param>
        public void An_angle_equal_to_90(Triangle t)
        {
            if (t is RightTriangle)
                return;
            foreach (Angle angle in t.Angles)
            {
                if (angle.ValueAngle == 90)
                {
                    Create_Right_Triangle(t, null!, angle);
                }
            }
        }

        /// <summary>
        /// משפט פיתגורס
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Finding_Length_rib_By_Pythagorean_Theorem(Triangle t)
        {
            if (t is not RightTriangle)
                return false;
            Rib ribNichav1 = t.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.nichav)!;
            Rib ribNichav2 = t.Ribs.LastOrDefault(x => x.DescriptionRib == DescriptionRib.nichav)!;
            Rib ribYeter = t.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.yeter)!;
            double nichav1 = ribNichav1.LenLine, nichav2 = ribNichav2.LenLine, yeter = ribYeter.LenLine;
            if ((nichav1 == 0 && nichav2 == 0) || (nichav1 == 0 && yeter == 0) || (nichav2 == 0 && yeter == 0))
                return false;
            ribYeter.LenLine = yeter == 0 ? Math.Sqrt(nichav1 * nichav1 + nichav2 * nichav2) : ribYeter.LenLine;
            if (ribYeter.LenLine != 0) { ribYeter.CompleteAllMyRelations(); };
            ribNichav1.LenLine = nichav1 == 0 ? Math.Sqrt(yeter * yeter - nichav2 * nichav2) : ribNichav1.LenLine;
            if (ribNichav1.LenLine != 0) { ribNichav1.CompleteAllMyRelations(); };
            ribNichav2.LenLine = nichav2 == 0 ? Math.Sqrt(yeter * yeter - nichav1 * nichav1) : ribNichav2.LenLine;
            if (ribNichav2.LenLine != 0) { ribNichav2.CompleteAllMyRelations(); };

            #region הוספת משפטים רלוונטים למילון
            if (!GlobalVariable.stopAddingSentences)
            {
                //מזמן םונקציה של היקף עבור כל אחד מהמשולשים שהצלעות יכולות להיות שייכות אליהם
                foreach (Rib rib in t.Ribs)
                {
                    if (!GlobalVariable.stopAddingSentences)
                    {
                        var shapesWithYeter = GlobalVariable.shapes.Where(s => s.Ribs.Any(r => r.NameLine == rib.NameLine)).ToList();
                        foreach (var shape in shapesWithYeter)
                        {
                            if (!GlobalVariable.stopAddingSentences)
                            {
                                Sentence s1 = Create_sentence_find_Perimeter(shape);
                                // הוספת המשפט החדש לרשימה של מי שזימן אותו
                                if (GlobalVariable.callStack.Count > 0)
                                {
                                    Sentence parentSentence = GlobalVariable.callStack.Peek(); // קבלת המשפט המזמן
                                    Sentence ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                                    if (ss == null)
                                        parentSentence.myChildren.Add(s1); // הוספת המשפט החדש לרשימת תתי המשפטים שלו
                                }

                                // המשך הסריקה עבור המשפט החדש
                                solving.ExploreTree(s1);
                            }
                        }
                    }
                }
            }
            #endregion
            return true;
        }

        /// <summary>
        /// משולש יפה
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public void Nice_triangle(Triangle t)
        {
            if (t is not RightTriangle) { return; }
            Angle angle30 = new Angle();
            foreach (Angle a in t.Angles)
            {
                if (a.ValueAngle != 90 && a.ValueAngle != 30 && a.ValueAngle != 60) { return; }
                if (a.ValueAngle == 30)
                    angle30 = a;
            }
            Rib the_perpendicular_opposite_the_angle30 = t.Ribs.FirstOrDefault(x => x.NameLine == (angle30.NameAngle[0] + angle30.NameAngle[2]).ToString())!;
            Rib yeter = t.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.yeter)!;
            var thisRelation = (yeter, 0.5);
            if (!the_perpendicular_opposite_the_angle30.GetMyRelations().Contains(thisRelation))
            {
                Relation relation1 = new Relation()
                {
                    obj1 = the_perpendicular_opposite_the_angle30,
                    obj2 = yeter,
                    relation = 0.5
                };
                GlobalVariable.ListAllRelations.Add(relation1);
                if (the_perpendicular_opposite_the_angle30.LenLine != 0)
                    yeter.LenLine = 2 * the_perpendicular_opposite_the_angle30.LenLine;
                if (yeter.LenLine != 0)
                    the_perpendicular_opposite_the_angle30.LenLine = 0.5 * yeter.LenLine;
            }
        }

        /// <summary>
        /// משפט הפוך למשולש יפה
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public void Nice_triangle_the__reverse_sentence(Triangle t)
        {
            if (t is not RightTriangle) { return; }
            foreach (Rib rib in t.Ribs)
            {
                Rib yeter = t.Ribs.FirstOrDefault(x => x.DescriptionRib == DescriptionRib.yeter)!;
                var thisRelation = (yeter, 0.5);
                if (rib.GetMyRelations().Contains(thisRelation) || (rib.LenLine * 2) == yeter.LenLine)//אם קיים יחס של תיכון ליתר
                {
                    char temp = rib.NameShape.FirstOrDefault(x => x != rib.NameLine[0] && x != rib.NameLine[1])!;
                    string nameAngle30 = rib.NameLine[0] + temp + rib.NameLine[1].ToString();
                    Angle a = t.Angles.FirstOrDefault(x => x.NameAngle == nameAngle30)!;
                    a.ValueAngle = 30;
                    Completing_Angles(t);
                }
            }
        }

        /// <summary>
        /// חפיפה
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public bool Overlap(Triangle triangle1, Triangle triangle2)
        {
            if (triangle1.Overlap_Rib_Rib_Rib(triangle2)
                || triangle1.Overlap_Rib_Angle_Rib(triangle2)
                || triangle1.Overlap_Angle_Rib_Angle(triangle2))
                return true;
            return false;
        }

        /// <summary>
        /// חפיפה לפי צלע צלע צלע
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public bool Overlap_Rib_Rib_Rib(Triangle triangle1, Triangle triangle2)
        {
            //בניית מילון של זוגות של צלעות שוות
            Dictionary<Rib, Rib> overlapRibs = new Dictionary<Rib, Rib>();
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
            {
                foreach (KeyValuePair<Rib, Rib> item in overlapRibs)
                {
                    //לסדר
                }
            }
            return false;
        }

        /// <summary>
        /// חפיפה לפי צלע זווית צלע
        /// </summary>
        /// <param name="triangle2"></param>
        /// <returns></returns>
        public bool Overlap_Rib_Angle_Rib(Triangle triangle1, Triangle triangle2)
        {
            //בניית מילון של זוגות מסוג מחלקת העל אלמנט: או צלע או זווית
            Dictionary<Element, Element> overlapElement = new Dictionary<Element, Element>();
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
        public bool Overlap_Angle_Rib_Angle(Triangle triangle1, Triangle triangle2)
        {
            //בניית מילון של זוגות מסוג מחלקת העל אלמנט: או צלע או זווית
            Dictionary<Element, Element> overlapElement = new Dictionary<Element, Element>();
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
        /// בדיקה אם זווית שליד צלע מסוימת קיימת במילון -פונקצית עזר לחפיפה
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
        /// אם נמצא יחס חדש על אלמנט, מוסיפים אתו לכל האלמנטים שקשורים אליו
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        public void CompleteAllMyRelations<T>(T element) where T : Element
        {
            List<(IRelated, double)> myRelations = element.GetMyRelations();
            Type type = typeof(T);
            foreach ((IRelated, double) item in myRelations)
            {
                if (type == typeof(Line))
                    ((Line)item.Item1).LenLine = (element as Line)!.LenLine * item.Item2;
                if (type == typeof(Angle))
                    ((Angle)item.Item1).ValueAngle = (element as Angle)!.ValueAngle * item.Item2;
            }
        }

        /// <summary>
        /// פונקצית עזר ליצירת משולש שווה שוקיים
        /// </summary>
        /// <param name="t"></param>
        /// <param name="thirdRib"></param>
        /// <param name="rib1"></param>
        /// <param name="rib2"></param>
        public void Create_Isosceles_Triangle(Triangle t, Rib thirdRib, Rib rib1, Rib rib2)
        {
            IsoscelesTriangle it = new IsoscelesTriangle();
            it.Angles = t.Angles;
            it.Ribs = t.Ribs;
            it.Name = t.Name;
            it.MoreAngles = t.MoreAngles;
            it.MoreAngles = t.MoreAngles;
            it.Set_attributes_of_the_isosceles_triangle();
            if (thirdRib != null)
            {
                Rib shok1 = t.Ribs.FirstOrDefault(x => x.NameLine != thirdRib.NameLine)!;
                Rib shok2 = t.Ribs.LastOrDefault(x => x.NameLine != thirdRib.NameLine)!;
                shok1.DescriptionRib = DescriptionRib.shok1;
                shok1.DescriptionRib = DescriptionRib.shok2;
            }
            else
            {
                rib1.DescriptionRib = DescriptionRib.shok1;
                rib2.DescriptionRib = DescriptionRib.shok2;
            }
            GlobalVariable.shapes.Remove(t);
            GlobalVariable.shapes.Add(it);

            #region הוספת משפטים רלוונטים למילון
            if (!GlobalVariable.stopAddingSentences)
            {
                Sentence s1 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(it);
                // הוספת המשפט החדש לרשימה של מי שזימן אותו
                if (GlobalVariable.callStack.Count > 0)
                {
                    Sentence parentSentence = GlobalVariable.callStack.Peek(); // קבלת המשפט המזמן
                    Sentence ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s1); // הוספת המשפט החדש לרשימת תתי המשפטים שלו
                }
                // המשך הסריקה עבור המשפט החדש
                solving.ExploreTree(s1);
            }
            #endregion
        }

        /// <summary>
        /// פונקצית עזר ליצירת משולש ישר זווית
        /// </summary>
        /// <param name="t"></param>
        /// <param name="line"></param>
        /// <param name="a"></param>
        public void Create_Right_Triangle(Triangle t, LineInShape line, Angle a)
        {
            RightTriangle rt = new RightTriangle();
            rt.Angles = t.Angles;
            rt.Ribs = t.Ribs;
            rt.Name = t.Name;
            rt.MoreLines = t.MoreLines;
            rt.MoreAngles = t.MoreAngles;

            if (line == null)
                rt.RightAngle = a;

            else
            {
                char ch = rt.Name.FirstOrDefault(x => x != line.NameLine[0] && x != line.NameLine[1]);
                string nameRightAngle = line.NameLine + ch;
                if (nameRightAngle[0] > nameRightAngle[2])
                    nameRightAngle = nameRightAngle[2] + nameRightAngle[1] + nameRightAngle[0].ToString();
                rt.RightAngle = rt.Angles.FirstOrDefault(x => x.NameAngle == nameRightAngle)!;
            }
            SetSetRibsOfLittleRightTri(rt);
            rt.Set_attributes_of_the_Right_triangle();
            GlobalVariable.shapes.Remove(t);
            GlobalVariable.shapes.Add(rt);


            #region הוספת משפטים רלונטיים לעץ
            if (!GlobalVariable.stopAddingSentences)
            {
                Sentence s1 = Create_sentence_Pythagorean_Theorem(rt);
                Sentence s2 = Create_sentence_The_middle_for_yeter(rt);
                Sentence s3 = Create_sentence_A_nice_triangle(rt);
                // הוספת המשפט החדש לרשימה של מי שזימן אותו
                if (GlobalVariable.callStack.Count > 0)
                {
                    Sentence parentSentence = GlobalVariable.callStack.Peek(); // קבלת המשפט המזמן
                    Sentence ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s1); // הוספת המשפט החדש לרשימת תתי המשפטים שלו
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s2);
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s3);
                }
                // המשך הסריקה עבור המשפט החדש
                solving.ExploreTree(s1);
                solving.ExploreTree(s2);
                solving.ExploreTree(s2);
            }

            #endregion
            Console.WriteLine("the right triangle " + rt.Name + " is built");
        }

        public void SetSetRibsOfLittleRightTri(RightTriangle t1)
        {
            string nameNichav1 = string.Concat(t1.RightAngle.NameAngle[0], t1.RightAngle.NameAngle[1]);
            string nameNichav2 = string.Concat(t1.RightAngle.NameAngle[1], t1.RightAngle.NameAngle[2]);
            string nameYeter = string.Concat(t1.RightAngle.NameAngle[2], t1.RightAngle.NameAngle[0]);
            nameNichav1 = new string(nameNichav1.OrderBy(char.ToLower).ToArray());
            nameNichav2 = new string(nameNichav2.OrderBy(char.ToLower).ToArray());
            nameYeter = new string(nameYeter.OrderBy(char.ToLower).ToArray());
            Rib ribNichav1 = t1.Ribs.FirstOrDefault(x => x.NameLine == nameNichav1)!;
            Rib ribNichav2 = t1.Ribs.FirstOrDefault(x => x.NameLine == nameNichav2)!;
            Rib ribYeter = t1.Ribs.FirstOrDefault(x => x.NameLine == nameYeter)!;
            ribNichav1.DescriptionRib = DescriptionRib.nichav;
            ribNichav2.DescriptionRib = DescriptionRib.nichav;
            ribYeter.DescriptionRib = DescriptionRib.yeter;
            foreach (Rib item in t1.Ribs)
            {
                Console.WriteLine("the length of rib " + item.NameLine + " is " + item.LenLine);
            }
        }

        /// <summary>
        /// פונקצית עזר ליצרת גובה
        /// </summary>
        /// <param name="line"></param>
        public void Create_plumb(LineInShape line)
        {
            line.DescriptionLine = DescriptionLine.plumb | DescriptionLine.bisectsAngle | DescriptionLine.middle;
            Triangle littleTriangle1 = GlobalVariable.shapes.Where(x => x is Triangle).Select(a => a as Triangle).FirstOrDefault(x => x.Ribs[0].NameLine == line.NameLine || x.Ribs[1].NameLine == line.NameLine || x.Ribs[2].NameLine == line.NameLine)!;
            Triangle littleTriangle2 = GlobalVariable.shapes.Where(x => x is Triangle).Select(a => a as Triangle).LastOrDefault(x => x.Ribs[0].NameLine == line.NameLine || x.Ribs[1].NameLine == line.NameLine || x.Ribs[2].NameLine == line.NameLine)!;
            Create_Right_Triangle(littleTriangle1, line, null!);
            Create_Right_Triangle(littleTriangle2, line, null!);
            Shape shape = GlobalVariable.shapes.FirstOrDefault(x => x.Name == line.NameShape)!;

            #region הוספת המשפטים הרלוונטים לעץ
            if (!GlobalVariable.stopAddingSentences)
            {
                Sentence s1 = Create_sentence_if_middle_and_plumb_is_converge(shape);
                Sentence s2 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(shape);
                Sentence s3 = Create_sentence_if_bisectsAngle_and_plumb_is_converge(shape);

                // הוספת המשפטים החדשים לתת חוליה של מי שזימן אותו
                if (GlobalVariable.callStack.Count > 0)
                {
                    Sentence parentSentence = GlobalVariable.callStack.Peek(); // קבלת המשפט המזמן
                    Sentence ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s1); // הוספת המשפט החדש לרשימת תתי המשפטים שלו
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s2);
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s3);
                }
                // המשך הסריקה עבור המשפט החדש
                solving.ExploreTree(s1);
                solving.ExploreTree(s2);
                solving.ExploreTree(s2);
            }
            #endregion

        }

        /// <summary>
        /// פונקצית עזר ליצרת חוצה זווית
        /// </summary>
        /// <param name="line"></param>
        public void Create_bisectsAngle(LineInShape line)
        {
            line.DescriptionLine = DescriptionLine.plumb | DescriptionLine.bisectsAngle | DescriptionLine.middle;
            Triangle littleTriangle1 = GlobalVariable.shapes.Where(x => x is Triangle).Select(a => a as Triangle).FirstOrDefault(x => x.Ribs[0].NameLine == line.NameLine || x.Ribs[1].NameLine == line.NameLine || x.Ribs[2].NameLine == line.NameLine)!;
            Triangle littleTriangle2 = GlobalVariable.shapes.Where(x => x is Triangle).Select(a => a as Triangle).FirstOrDefault(x => x != littleTriangle1 && (x.Ribs[0].NameLine == line.NameLine || x.Ribs[1].NameLine == line.NameLine || x.Ribs[2].NameLine == line.NameLine))!;
            string nameAngle1 = string.Concat(line.NameAngleSource[0], line.NameAngleSource[1], line.CutPoint);
            string nameAngle2 = string.Concat(line.NameAngleSource[2], line.NameAngleSource[1], line.CutPoint);
            nameAngle1 = nameAngle1[0] > nameAngle1[2] ? string.Concat(nameAngle1[2], nameAngle1[1], nameAngle1[0]) : nameAngle1;
            nameAngle2 = nameAngle2[0] > nameAngle2[2] ? string.Concat(nameAngle2[2], nameAngle2[1], nameAngle2[0]) : nameAngle2;

            Angle a1 = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == nameAngle1)!;
            Angle a2 = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == nameAngle2)!;
            if (!Is_equal<Angle>(a1, a2))
            {
                Relation relation1 = new Relation() { obj1 = a1, obj2 = a2, relation = 1 };
                GlobalVariable.ListAllRelations.Add(relation1);
                a1.ValueAngle = a1.ValueAngle == 0 ? a2.ValueAngle : a1.ValueAngle;
                a2.ValueAngle = a2.ValueAngle == 0 ? a1.ValueAngle : a2.ValueAngle;

            }
            Shape shape = GlobalVariable.shapes.FirstOrDefault(x => x.Name == line.NameShape)!;

            #region הוספת המשפטים הרלוונטים לעץ
            if (!GlobalVariable.stopAddingSentences)
            {
                Sentence s1 = Create_sentence_if_middle_and_bisectsAngle_is_converge(shape);
                Sentence s2 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(shape);
                Sentence s3 = Create_sentence_if_bisectsAngle_and_plumb_is_converge(shape);

                // הוספת המשפטים החדשים לתת חוליה של מי שזימן אותו
                if (GlobalVariable.callStack.Count > 0)
                {
                    Sentence parentSentence = GlobalVariable.callStack.Peek(); // קבלת המשפט המזמן
                    Sentence ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s1); // הוספת המשפט החדש לרשימת תתי המשפטים שלו
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s2);
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s3);
                }
                // המשך הסריקה עבור המשפט החדש
                solving.ExploreTree(s1);
                solving.ExploreTree(s2);
                solving.ExploreTree(s2);
            }
            #endregion
        }

        /// <summary>
        /// פונקצית עזר ליצירת תיכון
        /// </summary>
        /// <param name="line"></param>
        public void Create_middle(LineInShape line)
        {
            line.DescriptionLine = DescriptionLine.plumb | DescriptionLine.bisectsAngle | DescriptionLine.middle;
            string nameLine1 = line.RibDest.NameLine[0] + line.CutPoint.ToString();
            string nameLine2 = line.RibDest.NameLine[1] + line.CutPoint.ToString();
            Triangle littleTriangle1 = GlobalVariable.shapes.Where(x => x is Triangle).Select(a => a as Triangle).FirstOrDefault(x => x.Ribs[0].NameLine == nameLine1 || x.Ribs[1].NameLine == nameLine1 || x.Ribs[2].NameLine == nameLine1)!;
            Triangle littleTriangle2 = GlobalVariable.shapes.Where(x => x is Triangle).Select(a => a as Triangle).FirstOrDefault(x => x != littleTriangle1 && (x.Ribs[0].NameLine == line.NameLine || x.Ribs[1].NameLine == line.NameLine || x.Ribs[2].NameLine == line.NameLine))!;
            Rib rib1 = littleTriangle1.Ribs.FirstOrDefault(x => x.NameLine == nameLine1)!;
            Rib rib2 = littleTriangle2.Ribs.FirstOrDefault(x => x.NameLine == nameLine2)!;


            if (!Is_equal<Rib>(rib1, rib2))
            {
                Relation relation1 = new Relation() { obj1 = rib1, obj2 = rib2, relation = 1 };
                GlobalVariable.ListAllRelations.Add(relation1);
                rib1.LenLine = rib1.LenLine == 0 ? rib2.LenLine : rib1.LenLine;
                rib2.LenLine = rib2.LenLine == 0 ? rib1.LenLine : rib2.LenLine;
            }
            Shape shape = GlobalVariable.shapes.FirstOrDefault(x => x.Name == line.NameShape)!;

            #region הוספת המשפטים הרלוונטים לעץ
            if (!GlobalVariable.stopAddingSentences)
            {
                Sentence s1 = Create_sentence_if_middle_and_plumb_is_converge(shape);
                Sentence s2 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(shape);
                Sentence s3 = Create_sentence_if_middle_and_bisectsAngle_is_converge(shape);

                // הוספת המשפטים החדשים לתת חוליה של מי שזימן אותו
                if (GlobalVariable.callStack.Count > 0)
                {
                    Sentence parentSentence = GlobalVariable.callStack.Peek(); // קבלת המשפט המזמן
                    Sentence ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s1); // הוספת המשפט החדש לרשימת תתי המשפטים שלו
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s2);
                    ss = parentSentence.myChildren.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                    if (ss == null)
                        parentSentence.myChildren.Add(s3);
                }
                // המשך הסריקה עבור המשפט החדש
                solving.ExploreTree(s1);
                solving.ExploreTree(s2);
                solving.ExploreTree(s2);
            }
            #endregion
        }

        /// <summary>
        /// פונקצית עזר ליצירת משולש שווה צלעות
        /// </summary>
        /// <param name="t"></param>
        public void Create_Equilateral_Triangle(Triangle t)
        {
            EquilateralTriangle et = new EquilateralTriangle();
            et.Angles = t.Angles;
            et.Ribs = t.Ribs;
            et.Name = t.Name;
            et.MoreAngles = t.MoreAngles;
            et.MoreAngles = t.MoreAngles;
            et.Set_attributes_of_the_Equilateral_Triangle();
            GlobalVariable.shapes.Remove(t);
            GlobalVariable.shapes.Add(et);
        }
    }
}