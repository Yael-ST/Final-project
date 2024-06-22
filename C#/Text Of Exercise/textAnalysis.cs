using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IronXL;
using System.Collections;
using MyProject.Classes;
using SixLabors.ImageSharp.Formats.Tga;
using System.Reflection;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.ColorSpaces;


namespace MyProject.Text_Of_Exercise
{
    delegate void create_element(string sentence, int index);
    delegate void solving();

    internal class textAnalysis : Element
    {
        public string textOfExercise;
        public string data;
        public string proofs;
        public Dictionary<string, create_element> key_words_dictionary =
            new Dictionary<string, create_element>();
        solving solving;
        public textAnalysis()
        {

        }
        /// <summary>
        /// העלאת קובץ אקסל של מילות המפתח
        /// </summary>
        /// <param name="textOfExercise"></param>
        /// <returns></returns>
        public WorkSheet Uploading_an_Excel_file()
        {
            WorkBook wb = WorkBook.Load("C:\\Users\\win 10\\Desktop\\my-project\\MyProject\\Text Of Exercise\\key_words.xlsx");
            WorkSheet ws = wb.GetWorkSheet("sheet1");
            return ws;
        }

        /// <summary>
        /// מחלקת את הטקסט למשפטים
        /// </summary>
        /// <returns></returns>
        public string[] string_scan()
        {
            string[] sentences = this.data.Split('.');
            return sentences;
        }

        /// <summary>
        /// מחלקת את הטקסט לנתונים והוכחות
        /// </summary>
        public void divides_the_text_into_facts_and_proofs()
        {
            #region a
            string pattern1 = @"\bא\.\b";
            #endregion
            Regex regex1 = new Regex(pattern1);
            MatchCollection matches1 = regex1.Matches(this.textOfExercise);
            int index = -1;
            if (matches1.Count > 0)
                index = matches1[0].Index;
            else
            {
                string pattern = @"\b(הוכח|מצא|חשב)\b";
                Regex regex = new Regex(pattern);
                MatchCollection matches2 = regex.Matches(this.textOfExercise);
                if (matches2.Count > 0)
                    index = matches2[0].Index;
            }
            if (index != -1)
            {
                this.data = this.textOfExercise.Substring(0, index);
                this.proofs = this.textOfExercise.Substring(index);
            }
        }
        create_element ce;
        /// <summary>
        ///יצירת אוביקט לפי מילת מפתח
        /// </summary>
        public void Create_elements()
        {
            divides_the_text_into_facts_and_proofs();
            string[] sentences = string_scan();
            for (int i = 0; i < sentences.Length; i++)
            {
                //מעבר על כל מילות המפתח
                foreach (var word in this.key_words_dictionary)
                {
                    string pattern = word.Key;
                    create_element action = word.Value;
                    Match match = Regex.Match(sentences[i], pattern);
                    if (match.Success)
                    {
                        // אם נמצאה התאמה, הפעלת הפונקציה המתאימה
                        action(sentences[i], match.Index);
                    }
                }
            }
        }

        /// <summary>
        ///  מחזירה שם צורה
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameOfShape(string sentence,Shape shape)
        {
            int length = shape.Name.Length;
            Regex rg = new Regex($@"\b[A-Z]{{{length}}}\b");
            Match match = rg.Match(sentence);
            return match.Value;
        }
        /// <summary>
        ///  מחזירה שם זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameOfAngle(string sentence)
        {
            Regex rg = new Regex(@"[A-Z]{3}");
            Match match = rg.Match(sentence);
            return match.Value;
        }

        /// <summary>
        /// מחזירה שם המשולש שהישר/הצלע שייכים אליו
        /// </summary>
        /// <param name="nameLine"></param>
        /// <returns></returns>
        public string GetNameTriangleOfRibOrLine(string nameLine)
        {
            foreach (Shape shape in GlobalVariable.shapes)
            {
                if (shape.Name.Contains(nameLine[0]) || shape.Name.Contains(nameLine[1]))
                    return shape.Name;
            }
            return "";
        }

        /// <summary>
        /// מחזירה שם צלע/ישר
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="indexTri"></param>
        /// <returns></returns>
        public string GetNameOfRibOrLine(string sentence)
        {
            Regex rg = new Regex(@"[A-Z]{2}");
            Match match = rg.Match(sentence);
            return match.Value;
        }

        /// <summary>
        /// מחזירה את הקודקוד ממנו יוצא הישר
        /// </summary>
        /// <param name="nameLine"></param>
        /// <returns></returns>
        public char GetVertexSource(string nameLine, string nameTriangle)
        {
            if (nameTriangle.Contains(nameLine[0]))
                return nameLine[0];
            return nameLine[1];
        }

        /// <summary>
        /// מחזירה נקודת חיתוך של הישר
        /// </summary>
        /// <param name="nameLine"></param>
        /// <param name="vertexSource"></param>
        /// <returns></returns>
        public char GetCutPoint(string nameLine, char vertexSource)
        {
            if (nameLine[0] == vertexSource)
                return nameLine[1];
            return nameLine[0];
        }

        /// <summary>
        /// מחזירה צלע יעד של ישר במשולש
        /// </summary>
        /// <param name="vertexSource"></param>
        /// <param name="nameShape"></param>
        /// <returns></returns>
        public Rib GetRibDest(char vertexSource, string nameShape)
        {
            string nameRib = "";
            if (nameShape[0] == vertexSource)
                nameRib = nameShape[1].ToString() + nameShape[2].ToString();
            if (nameShape[1] == vertexSource)
                nameRib = nameShape[2].ToString() + nameShape[0].ToString();
            if (nameShape[2] == vertexSource)
                nameRib = nameShape[1].ToString() + nameShape[0].ToString();
            Shape t = GlobalVariable.shapes.First(x => x.Name == nameShape);
            if (t != null)
                return t.Ribs.First(x => x.NameLine == nameRib);
            return null;
        }

        /// <summary>
        /// מחזירה את זווית המקור
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameAngleSource(string nameTriangleOfLine, char vertexSource)
        {
            Shape t = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangleOfLine);
            Angle a = t.Angles.First(x => x.NameAngle[1] == vertexSource);
            return a.NameAngle;
        }

        /// <summary>
        /// פרטי זווית של הצורה
        /// </summary>
        /// <param name="shape"></param>
        public void SetAnglesOfShape(Shape shape)
        {
            int length = shape.Name.Length;
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i <= 2; i++)
                    shape.Ribs[j].NameLine += shape.Name[(i + length - 1) % length];
                for (int i = 0; i <= 3; i++)
                    shape.Angles[j].NameAngle += shape.Name[(i + length - 1) % length];
            }
        }

        /// <summary>
        /// פרטי צלעות של המשולש
        /// </summary>
        /// <param name="shape"></param>
        public void SetRibsOfShape(Shape shape)
        {
            foreach (Rib rib in shape.Ribs)
            {
                rib.NameTriangle = shape.Name;
                rib.DescriptionRib = DescriptionRib.simple;
            }

            int length = shape.Name.Length;
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i <= 2; i++)
                    shape.Ribs[j].NameLine += shape.Name[(i + length - 1) % length];
            }
        }

        /// <summary>
        /// פרטי  צלעות של משולש ישר זווית
        /// </summary>
        /// <param name="t1"></param>
        public void SetRibsOfRightTri(RightTriangle t1)
        {
            string nameNichav1 = t1.NameRightAngle[0].ToString() + t1.NameRightAngle[1].ToString();
            string nameNichav2 = t1.NameRightAngle[1].ToString() + t1.NameRightAngle[2].ToString();
            string nameYeter = t1.NameRightAngle[2].ToString() + t1.NameRightAngle[0].ToString();
            t1.Ribs[0].NameLine = nameNichav1;
            t1.Ribs[0].DescriptionRib = DescriptionRib.nichav;
            t1.Ribs[0].NameTriangle = t1.Name;

            t1.Ribs[1].NameLine = nameNichav2;
            t1.Ribs[1].DescriptionRib = DescriptionRib.nichav;
            t1.Ribs[1].NameTriangle = t1.Name;

            t1.Ribs[2].NameLine = nameYeter;
            t1.Ribs[2].DescriptionRib = DescriptionRib.yeter;
            t1.Ribs[2].NameTriangle = t1.Name;

        }

        /// <summary>
        /// פרטי צלעות של זווית
        /// </summary>
        /// <param name="a1"></param>
        public void SetRibsOfAngle(Angle a1)
        {
            a1.Rib1.NameLine = (a1.NameAngle[0] + a1.NameAngle[1]).ToString();
            a1.Rib2.NameLine = (a1.NameAngle[1] + a1.NameAngle[2]).ToString();
        }

        /// <summary>
        /// יצירת משולש
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_triangle(string sentence, int index)
        {
            Triangle t1 = new Triangle();
            string nameTriangle = GetNameOfShape(sentence,t1);
            var triangle = GlobalVariable.shapes.First(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                SetRibsOfShape(t1);
                GlobalVariable.shapes.Add(t1);
            }
        }

        /// <summary>
        /// יצירת  משולש שו"ש
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_isosceles_triangle(string sentence, int index)
        {
            IsoscelesTriangle t1 = new IsoscelesTriangle();
            string nameTriangle = GetNameOfShape(sentence,t1);
            var triangle = GlobalVariable.shapes.First(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                SetRibsOfShape(t1);
                GlobalVariable.shapes.Add(t1);
            }
        }

        /// <summary>
        /// משולש שו"צ
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_equilateral_triangle(string sentence, int index)
        {
            EquilateralTriangle t1 = new EquilateralTriangle();
            string nameTriangle = GetNameOfShape(sentence, t1);
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                SetRibsOfShape(t1);
                GlobalVariable.shapes.Add(t1);
            }
        }

        /// <summary>
        /// משולש ישר זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void create_right_triangle(string sentence, int index)
        {
            RightTriangle t1 = new RightTriangle();
            string nameTriangle = GetNameOfShape(sentence,t1);
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                string subSentence = sentence.Substring(index);
                t1.NameRightAngle = GetNameOfAngle(subSentence);
                SetRibsOfRightTri(t1);
                GlobalVariable.shapes.Add(t1);
            }
        }

        /// <summary>
        /// יצירת זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_angle(string sentence, int index)
        {
            string nameAngle = GetNameOfAngle(sentence);
            var sortedAngle = new string(nameAngle.Where(char.IsLetter).OrderBy(char.ToLower).ToArray());
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == sortedAngle);
            if (triangle == null)
            {
                Angle a1 = new Angle();
                a1.NameAngle = nameAngle;
                SetRibsOfAngle(a1);
            }
        }
        /// <summary>
        /// יוצרת 2 משולשים קטנים שמרכיבים את המשולש בו עבר הישר
        /// </summary>
        /// <param name="l"></param>
        /// <param name="tri"></param>
        public void Create_2_small_tris(LineInTriangle l, Triangle tri)
        {
            Rib r1 = tri.Ribs.First(x => x.NameLine.Contains(l.VertexSource));
            string nameSmallTriangle1 = l.CutPoint + r1.NameLine;
            nameSmallTriangle1 = new string(nameSmallTriangle1.OrderBy(char.ToLower).ToArray());
            var triangle1 = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameSmallTriangle1);
            if (triangle1 == null)
            {
                Triangle smallTriangle1 = new Triangle();
                smallTriangle1.Name = nameSmallTriangle1;
                smallTriangle1.sortNameShape();
                SetAnglesOfShape(smallTriangle1);
                SetRibsOfShape(smallTriangle1);
                GlobalVariable.shapes.Add(smallTriangle1);
            }

            Rib r2 = tri.Ribs.Last(x => x.NameLine.Contains(l.VertexSource));
            string nameSmallTriangle2 = l.CutPoint + r2.NameLine;
            nameSmallTriangle2 = new string(nameSmallTriangle2.OrderBy(char.ToLower).ToArray());
            var triangle2 = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameSmallTriangle2);
            if (triangle2 == null)
            {
                Triangle smallTriangle2 = new Triangle();
                smallTriangle2.Name = nameSmallTriangle1;
                smallTriangle2.sortNameShape();
                SetAnglesOfShape(smallTriangle2);
                SetRibsOfShape(smallTriangle2);
                GlobalVariable.shapes.Add(smallTriangle2);
            }
        }

        /// <summary>
        /// יצירת ישר
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_line(string sentence, int index)
        {
            Line l1 = new Line();
            l1.NameLine = GetNameOfRibOrLine(sentence);
        }

        /// <summary>
        /// יצירת תיכון
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_middle(string sentence, int index)
        {
            string nameLine = GetNameOfRibOrLine(sentence);
            Line line = GlobalVariable.lines.First(x => x.NameLine == nameLine);
            if (line == null)
            {
                LineInTriangle l = new LineInTriangle();
                l.NameLine = nameLine;
                l.DescriptionLine = DescriptionLine.middle;
                l.NameTriangle = GetNameTriangleOfRibOrLine(l.NameLine);
                l.VertexSource = GetVertexSource(l.NameLine, l.NameTriangle);
                l.CutPoint = GetCutPoint(l.NameLine, l.VertexSource);
                l.RibDest = GetRibDest(l.VertexSource, l.NameTriangle);

                Triangle tri = GlobalVariable.shapes.OfType<Triangle>().First(t => t.Name ==l.NameTriangle);

                tri.MoreLines.Add(l);
                GlobalVariable.lines.Add(l);

                Create_2_lines(l);
                Create_2_small_tris(l, tri);
            }
        }

        public void Create_2_lines(LineInTriangle l)
        {
            int indexOfOtherVertex1 = 0, indexOfOtherVertex2 = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (l.VertexSource == l.NameTriangle[i])
                    if (indexOfOtherVertex1 != 0)
                        indexOfOtherVertex1 = i;
                    else
                        indexOfOtherVertex2 = i;
            }
            Line l1 = new Line();
            l1.NameLine = l.CutPoint.ToString() + indexOfOtherVertex1;
            l1.NameTriangle = l.NameTriangle;
            GlobalVariable.lines.Add(l1);

            Line l2 = new Line();
            l2.NameLine = l.CutPoint.ToString() + indexOfOtherVertex2;
            l2.NameTriangle = l.NameTriangle;
            GlobalVariable.lines.Add(l2);

            var thisRelation = (l1, 1);
            if (!l2.GetMyRelations().Contains(thisRelation))
            {
                Relation relation1 = new Relation() { obj1 = l1, obj2 = l2, relation = 1 };
                this.ListAllRelations.Add(relation1);
            }
        }

        /// <summary>
        /// יצירת גובה
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_plumb(string sentence, int index)
        {
            LineInTriangle l1 = new LineInTriangle();
            l1.NameLine = GetNameOfRibOrLine(sentence);
            l1.DescriptionLine = DescriptionLine.plumb;
            l1.NameTriangle = GetNameTriangleOfRibOrLine(l1.NameLine);
            l1.VertexSource = GetVertexSource(l1.NameLine, l1.NameTriangle);
            l1.CutPoint = GetCutPoint(l1.NameLine, l1.VertexSource);
            l1.RibDest = GetRibDest(l1.VertexSource, l1.NameTriangle);

            Triangle tri = GlobalVariable.shapes.OfType<Triangle>().First(t => t.Name == l1.NameTriangle);
            tri.MoreLines.Add(l1);
        }

        /// <summary>
        /// יצירת חוצה זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_bisects_angle(string sentence, int index)
        {
            LineInTriangle l1 = new LineInTriangle();
            l1.NameLine = GetNameOfRibOrLine(sentence);
            l1.DescriptionLine = DescriptionLine.bisectsAngle;
            l1.NameTriangle = GetNameTriangleOfRibOrLine(l1.NameLine);
            l1.VertexSource = GetVertexSource(l1.NameLine, l1.NameTriangle);
            l1.CutPoint = GetCutPoint(l1.NameLine, l1.VertexSource);
            l1.RibDest = GetRibDest(l1.VertexSource, l1.NameTriangle);
            l1.NameAngleSource = GetNameAngleSource(l1.NameTriangle, l1.VertexSource);

            Triangle tri = GlobalVariable.shapes.OfType<Triangle>().First(t => t.Name == l1.NameTriangle);
            tri.MoreLines.Add(l1);

            //הוספת 2 הזויות החדשות שנוצרו
            Angle a1 = new Angle();
            a1.NameAngle = l1.NameAngleSource[0].ToString() + l1.NameAngleSource[1].ToString() + l1.CutPoint;
            SetRibsOfAngle(a1);
            Angle a2 = new Angle();
            a2.NameAngle = l1.NameAngleSource[2].ToString() + l1.NameAngleSource[1].ToString() + l1.CutPoint;
            SetRibsOfAngle(a2);
            tri.MoreAngles.Add(a1);
            tri.MoreAngles.Add(a2);

            var thisRelation = (a1, 1);
            if (!a2.GetMyRelations().Contains(thisRelation))
            {
                Relation relation1 = new Relation() { obj1 = a1, obj2 = a2, relation = 1 };
                this.ListAllRelations.Add(relation1);
            }
        }

        /// <summary>
        /// יצירת צלע
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_rib(string sentence, int index)
        {
            Rib r1 = new Rib();
            r1.NameLine = GetNameOfRibOrLine(sentence);
            r1.DescriptionRib = DescriptionRib.simple;
            r1.NameTriangle = GetNameTriangleOfRibOrLine(r1.NameLine);

        }

        /// <summary>
        /// יצירת ניצב
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_nichav(string sentence, int index)
        {
            Rib r1 = new Rib();
            r1.NameLine = GetNameOfRibOrLine(sentence);
            r1.DescriptionRib = DescriptionRib.nichav;
            r1.NameTriangle = GetNameTriangleOfRibOrLine(r1.NameLine);
        }

        /// <summary>
        /// יצירת יתר
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_yeter(string sentence, int index)
        {
            Rib r1 = new Rib();
            r1.NameLine = GetNameOfRibOrLine(sentence);
            r1.DescriptionRib = DescriptionRib.yeter;
            r1.NameTriangle = GetNameTriangleOfRibOrLine(r1.NameLine);
        }

        /// <summary>
        /// בונה את המילון של מילות המפתח
        /// </summary>
        public void build_dictionary()
        {
            WorkSheet key_words = Uploading_an_Excel_file();
            key_words_dictionary.Add(key_words["A1"].ToString(), create_right_triangle);
            key_words_dictionary.Add(key_words["A2"].ToString(), Create_isosceles_triangle);
            key_words_dictionary.Add(key_words["A3"].ToString(), Create_equilateral_triangle);
            key_words_dictionary.Add(key_words["A4"].ToString(), Create_triangle);
            key_words_dictionary.Add(key_words["A5"].ToString(), Create_angle);
            key_words_dictionary.Add(key_words["A6"].ToString(), Create_angle);
            key_words_dictionary.Add(key_words["A7"].ToString(), Create_line);
            key_words_dictionary.Add(key_words["A8"].ToString(), Create_line);
            key_words_dictionary.Add(key_words["A9"].ToString(), Create_middle);
            key_words_dictionary.Add(key_words["A10"].ToString(), Create_middle);
            key_words_dictionary.Add(key_words["A11"].ToString(), Create_plumb);
            key_words_dictionary.Add(key_words["A12"].ToString(), Create_bisects_angle);
            key_words_dictionary.Add(key_words["A13"].ToString(), Create_bisects_angle);
            key_words_dictionary.Add(key_words["A14"].ToString(), Create_rib);
            key_words_dictionary.Add(key_words["A15"].ToString(), Create_nichav);
            key_words_dictionary.Add(key_words["A16"].ToString(), Create_yeter);
            key_words_dictionary.Add(key_words["A17"].ToString(), Create_relation);
            key_words_dictionary.Add(key_words["A18"].ToString(), Create_relation);
        }

        public void Create_relation(string sentence, int index)
        {
            string firstElement = GetFirstElement(sentence);
            string secondElement = GetSecondElement(sentence, index);
            //במקרה שזה צלע או ישר
            if (firstElement.Length == 2)
            {
                Line l1 = GlobalVariable.lines.First(x => x.NameLine == firstElement);
                Line l2 = GlobalVariable.lines.First(x => x.NameLine == secondElement);
                var thisRelation = (l1, 1);
                if (!l2.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = l1, obj2 = l2, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                }
            }

            //במקרה שזה זווית
            if (firstElement.Length == 3)
            {
                Angle a1 = GlobalVariable.angles.First(x => x.NameAngle == firstElement);
                Angle a2 = GlobalVariable.angles.First(x => x.NameAngle == secondElement);
                var thisRelation = (a1, 1);
                if (!a2.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = a1, obj2 = a2, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                }
            }
        }

        public string GetSecondElement(string sentence, int index)
        {
            Regex rg = new Regex(@"[A-Z]{2,3}");
            string substring = sentence.Substring(index);
            Match match = rg.Match(substring);
            return match.Value;
        }

        public string GetFirstElement(string sentence)
        {
            Regex rg = new Regex(@"[A-Z]{2,3}");
            Match match = rg.Match(sentence);
            return match.Value;
        }
    }
}