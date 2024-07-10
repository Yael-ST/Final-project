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
using static IronPython.Modules._ast;


namespace MyProject.Text_Of_Exercise
{
    delegate void create_element(string sentence, int index);

    internal class TextAnalysis : Sentence
    {
        public string textOfExercise;
        public string data;
        public string proofs;
        public Dictionary<string, create_element> key_words_dictionary;
        public TextAnalysis()
        {
            key_words_dictionary = new Dictionary<string, create_element>();
        }
        /// <summary>
        /// העלאת קובץ אקסל של מילות המפתח
        /// </summary>
        /// <param name="textOfExercise"></param>
        /// <returns></returns>
        public WorkSheet Uploading_an_Excel_file()
        {
            WorkBook wb = WorkBook.Load("C:\\Users\\win 10\\Desktop\\my-project\\C#\\Text Of Exercise\\key_words.xlsx");
            WorkSheet ws = wb.GetWorkSheet("sheet1");
            return ws;
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
            Console.WriteLine("the key_words dictionary is built");

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
            if (matches1 != null)
            {
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
            Console.WriteLine("the text divided");


        }
        create_element ce;
        /// <summary>
        ///יצירת אוביקט לפי מילת מפתח
        /// </summary>
        public void Scan_the_data()
        {
            build_dictionary();
            divides_the_text_into_facts_and_proofs();
            string[] sentences = string_scan();
            
            for (int i = 0; i < sentences.Length; i++)
            {
                Console.WriteLine(sentences[i]);
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
        public string GetNameOfShape(string sentence, Shape shape)
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
        /// מחזירה שם צורה שהישר/הצלע שייכים אליו
        /// </summary>
        /// <param name="nameLine"></param>
        /// <returns></returns>
        public string GetNameShapeOfRibOrLine(string nameLine)
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
        public char GetVertexSource(string nameLine, string nameShape)
        {
            if (nameShape.Contains(nameLine[0]))
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
        /// מחזירה צלע יעד של ישר 
        /// </summary>
        /// <param name="vertexSource"></param>
        /// <param name="nameShape"></param>
        /// <returns></returns>
        public Rib GetRibDest(char vertexSource, Shape shape, string sentence)
        {
            if (shape is Triangle)
            {
                string nameRib = "";
                if (shape.Name[0] == vertexSource)
                    nameRib = shape.Name[0].ToString() + shape.Name[0].ToString();
                if (shape.Name[0] == vertexSource)
                    nameRib = shape.Name[0].ToString() + shape.Name[0].ToString();
                if (shape.Name[0] == vertexSource)
                    nameRib = shape.Name[0].ToString() + shape.Name[0].ToString();
                return shape.Ribs.FirstOrDefault(x => x.NameLine == nameRib)!;
            }
            else
            {
                string nameLine = GetNameOfRibOrLine(sentence);
                Rib? r = GlobalVariable.lines.OfType<Rib>().FirstOrDefault(x => x.NameLine == nameLine);
                return r!;
            }
        }

        /// <summary>
        /// מחזירה את זווית המקור
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameAngleSource(Shape ShapeOfLine, char vertexSource)
        {
            Angle? a = ShapeOfLine.Angles.FirstOrDefault(x => x.NameAngle[1] == vertexSource);
            return a!.NameAngle;
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
                shape.Angles[j].sortNameAngle();
            }
        }

        /// <summary>
        /// פרטי צלעות של הצורה
        /// </summary>
        /// <param name="shape"></param>
        public void SetRibsOfShape(Shape shape)
        {
            foreach (Rib rib in shape.Ribs)
            {
                rib.NameShape = shape.Name;
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
            string nameNichav1 = t1.RightAngle.NameAngle[0].ToString() + t1.RightAngle.NameAngle[1].ToString();
            string nameNichav2 = t1.RightAngle.NameAngle[1].ToString() + t1.RightAngle.NameAngle[2].ToString();
            string nameYeter = t1.RightAngle.NameAngle[2].ToString() + t1.RightAngle.NameAngle[0].ToString();
            t1.Ribs[0].NameLine = nameNichav1;
            t1.Ribs[0].DescriptionRib = DescriptionRib.nichav;
            t1.Ribs[0].NameShape = t1.Name;

            t1.Ribs[1].NameLine = nameNichav2;
            t1.Ribs[1].DescriptionRib = DescriptionRib.nichav;
            t1.Ribs[1].NameShape = t1.Name;

            t1.Ribs[2].NameLine = nameYeter;
            t1.Ribs[2].DescriptionRib = DescriptionRib.yeter;
            t1.Ribs[2].NameShape = t1.Name;

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
            string nameTriangle = GetNameOfShape(sentence, t1);
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                SetRibsOfShape(t1);
                GlobalVariable.shapes.Add(t1);

                #region הוספת המשפטים הרלוונטים לרשימת המשפטים
                #endregion
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
            string nameTriangle = GetNameOfShape(sentence, t1);
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                SetRibsOfShape(t1);
                t1.Set_attributes_of_the_isosceles_triangle();
                GlobalVariable.shapes.Add(t1);
                Console.WriteLine("the IsoscelesTriangle is built");

                #region הוספת משפטים רלוונטים לעץ
                Sentence s1 = Create_sentence_Lines_in_triangle_is_converge(triangle!);
                GlobalVariable.relevant_sentences.Add(s1);               
                #endregion
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
                t1.Set_attributes_of_the_Equilateral_Triangle();
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
            string nameTriangle = GetNameOfShape(sentence, t1);
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1);
                string subSentence = sentence.Substring(index);
                t1.RightAngle.NameAngle = GetNameOfAngle(subSentence);
                SetRibsOfRightTri(t1);
                t1.Set_attributes_of_the_Right_triangle();
                GlobalVariable.shapes.Add(t1);

                #region הוספת משפטים רלונטיים למילון
                Sentence s1 = Create_sentence_Pythagorean_Theorem(triangle!);
                GlobalVariable.relevant_sentences.Add(s1);
                Sentence s2 = Create_sentence_The_middle_for_yeter(triangle!);
                GlobalVariable.relevant_sentences.Add(s2);
                Sentence s3 = Create_sentence_A_nice_triangle(triangle!);
                GlobalVariable.relevant_sentences.Add(s3);
                #endregion

                Console.WriteLine("RightTriangle");

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
            //בדיקה אם הזווית לא שייכת למשולש קיים
            var sortedAngle = new string(nameAngle.Where(char.IsLetter).OrderBy(char.ToLower).ToArray());
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == sortedAngle);
            if (triangle == null)
            {
                Angle a1 = new Angle();
                a1.NameAngle = nameAngle;
                SetRibsOfAngle(a1);
                a1.sortNameAngle();
                GlobalVariable.angles.Add(a1);
            }
        }

        /// <summary>
        /// יוצרת 2 משולשים קטנים שמרכיבים את המשולש בו עבר הישר
        /// </summary>
        /// <param name="l"></param>
        /// <param name="tri"></param>
        public void Create_2_small_tris(LineInShape l, Triangle tri)
        {
            //בניית שם המשולש החדש על ידי שרשור עם שם הצלע המתאימה 
            Rib? r1 = tri.Ribs.FirstOrDefault(x => x.NameLine.Contains(l.VertexSource));
            string nameSmallTriangle1 = l.CutPoint + r1!.NameLine;
            nameSmallTriangle1 = new string(nameSmallTriangle1.OrderBy(char.ToLower).ToArray());
            var triangle1 = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameSmallTriangle1);

            //בניית שם המשולש החדש על ידי שרשור עם שם הצלע המתאימה 
            Rib r2 = tri.Ribs.Last(x => x.NameLine.Contains(l.VertexSource));
            string nameSmallTriangle2 = l.CutPoint + r2.NameLine;
            nameSmallTriangle2 = new string(nameSmallTriangle2.OrderBy(char.ToLower).ToArray());
            var triangle2 = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameSmallTriangle2);

            //אם הישר הוא גובה צריך ליצור שתי משולשים ישרי זויית
            if (l.DescriptionLine == DescriptionLine.plumb)
            {
                //1
                RightTriangle smallRightTriangle1 = new RightTriangle();
                smallRightTriangle1.Name = nameSmallTriangle1;
                smallRightTriangle1.sortNameShape();
                SetRibsOfRightTri(smallRightTriangle1);
                SetAnglesOfShape(smallRightTriangle1);
                smallRightTriangle1.RightAngle.NameAngle = r1.NameLine[0] + l.CutPoint + r1.NameLine[1].ToString();
                smallRightTriangle1.RightAngle.sortNameAngle();
                smallRightTriangle1.RightAngle.ValueAngle = 90;
                GlobalVariable.shapes.Add(smallRightTriangle1);

                //2
                RightTriangle smallRightTriangle2 = new RightTriangle();
                smallRightTriangle2.Name = nameSmallTriangle2;
                smallRightTriangle2.sortNameShape();
                SetRibsOfRightTri(smallRightTriangle2);
                SetAnglesOfShape(smallRightTriangle2);
                smallRightTriangle2.RightAngle.NameAngle = r2.NameLine[0] + l.CutPoint + r2.NameLine[1].ToString();
                smallRightTriangle2.RightAngle.sortNameAngle();
                smallRightTriangle2.RightAngle.ValueAngle = 90;
                GlobalVariable.shapes.Add(smallRightTriangle2);
            }
            //אם לא- יוצר 2 משולשים רגילים
            else
            {
                if (triangle1 == null)
                {
                    Triangle smallTriangle1 = new Triangle();
                    smallTriangle1.Name = nameSmallTriangle1;
                    smallTriangle1.sortNameShape();
                    SetAnglesOfShape(smallTriangle1);
                    SetRibsOfShape(smallTriangle1);
                    GlobalVariable.shapes.Add(smallTriangle1);
                }

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
            GlobalVariable.lines.Add(l1);
        }

        /// <summary>
        /// יצירת תיכון
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_middle(string sentence, int index)
        {
            Triangle tri=new Triangle();
            if (sentence != "")
            {
                string nameLine = GetNameOfRibOrLine(sentence);
                Line? line = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == nameLine);
                if (line == null)
                {
                    LineInShape l = new LineInShape();
                    l.NameLine = nameLine;
                    l.DescriptionLine = DescriptionLine.middle;
                    l.NameShape = GetNameShapeOfRibOrLine(l.NameLine);
                    l.VertexSource = GetVertexSource(l.NameLine, l.NameShape);
                    l.CutPoint = GetCutPoint(l.NameLine, l.VertexSource);

                    tri = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(t => t.Name == l.NameShape)!;
                    l.RibDest = GetRibDest(l.VertexSource, tri!, sentence);

                    tri!.MoreLines.Add(l);
                    GlobalVariable.lines.Add(l);
                    Create_2_lines(l);
                    Create_2_small_tris(l, tri);
                }
                if (((LineInShape)line!).DescriptionLine == DescriptionLine.plumb)
                {
                    ((LineInShape)line).DescriptionLine = DescriptionLine.plumb | DescriptionLine.middle;
                    tri = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(t => t.Name == line.NameShape)!;
                }
                if (((LineInShape)line!).DescriptionLine == DescriptionLine.bisectsAngle)
                {
                    ((LineInShape)line).DescriptionLine = DescriptionLine.bisectsAngle | DescriptionLine.middle;
                    tri = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(t => t.Name == line.NameShape)!;

                }
                #region הוספת משפטים רלוונטים לעץ
                Sentence s1 = Create_sentence_if_middle_and_bisectsAngle_is_converge(tri);
                GlobalVariable.relevant_sentences.Add(s1);
                Sentence s2 = Create_sentence_if_middle_and_plumb_is_converge(tri);
                GlobalVariable.relevant_sentences.Add(s2);
                Sentence s3 = Create_sentence_Lines_in_triangle_is_converge(tri);
                GlobalVariable.relevant_sentences.Add(s3);
                Sentence s4 = Create_sentence_The_middle_for_yeter(tri);
                GlobalVariable.relevant_sentences.Add(s4);
                #endregion

                Console.WriteLine("middle is built");

            }
        }

        /// <summary>
        /// יצירת 2 קטעים שנוצרו מהישר שעבר על הבסיס
        /// </summary>
        /// <param name="l"></param>
        public void Create_2_lines(LineInShape l)
        {
            int indexOfOtherVertex1 = 0, indexOfOtherVertex2 = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (l.VertexSource == l.NameShape[i])
                    if (indexOfOtherVertex1 != 0)
                        indexOfOtherVertex1 = i;
                    else
                        indexOfOtherVertex2 = i;
            }
            Line l1 = new Line();
            l1.NameLine = l.CutPoint.ToString() + indexOfOtherVertex1;
            l1.NameShape = l.NameShape;
            GlobalVariable.lines.Add(l1);

            Line l2 = new Line();
            l2.NameLine = l.CutPoint.ToString() + indexOfOtherVertex2;
            l2.NameShape = l.NameShape;
            GlobalVariable.lines.Add(l2);
            //אם הישר הוא תיכון אז הקטעים החדשים שנוצרו שווים
            if(l.DescriptionLine==DescriptionLine.middle)
            {
                if (!Is_equal<Line>(l1, l2))
                {
                    Relation relation1 = new Relation() { obj1 = l1, obj2 = l2, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                    if (l.RibDest.LenLine != 0)
                    {
                        l1.LenLine = l.LenLine * 0.5;
                        l2.LenLine= l.LenLine * 0.5;
                    }
   
                }
            }
            
        }

        /// <summary>
        /// יצירת גובה
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_plumb(string sentence, int index)
        {
            LineInShape l1 = new LineInShape();
            l1.NameLine = GetNameOfRibOrLine(sentence);
            l1.DescriptionLine = DescriptionLine.plumb;
            l1.NameShape = GetNameShapeOfRibOrLine(l1.NameLine);
            l1.VertexSource = GetVertexSource(l1.NameLine, l1.NameShape);
            l1.CutPoint = GetCutPoint(l1.NameLine, l1.VertexSource);

            Shape? shape = GlobalVariable.shapes.FirstOrDefault(t => t.Name == l1.NameShape);
            sentence = sentence.Substring(index);
            l1.RibDest = GetRibDest(l1.VertexSource, shape!, sentence);

            shape!.MoreLines.Add(l1);
            GlobalVariable.lines.Add(l1);
            if (shape is Triangle)
            {
                Create_2_small_tris(l1, (Triangle)shape);
            }
            Create_2_lines(l1);
            if (((LineInShape)l1!).DescriptionLine == DescriptionLine.middle)
            {
                ((LineInShape)l1).DescriptionLine = DescriptionLine.middle | DescriptionLine.middle;
            }
            if (((LineInShape)l1!).DescriptionLine == DescriptionLine.bisectsAngle)
            {
                ((LineInShape)l1).DescriptionLine = DescriptionLine.bisectsAngle | DescriptionLine.middle;
            }

            Sentence s1 = Create_sentence_if_middle_and_plumb_is_converge(shape);
            GlobalVariable.relevant_sentences.Add(s1);
            Sentence s2 = Create_sentence_Lines_in_triangle_is_converge(shape);
            GlobalVariable.relevant_sentences.Add(s2);
            Sentence s3 = Create_sentence_if_bisectsAngle_and_plumb_is_converge(shape);
            GlobalVariable.relevant_sentences.Add(s3);
            Console.WriteLine("plumb is built");

        }

        /// <summary>
        /// יצירת חוצה זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_bisects_angle(string sentence, int index)
        {
            LineInShape l1 = new LineInShape();
            l1.NameLine = GetNameOfRibOrLine(sentence);
            l1.sortNameLine();
            l1.DescriptionLine = DescriptionLine.bisectsAngle;
            l1.NameShape = GetNameShapeOfRibOrLine(l1.NameLine);
            l1.VertexSource = GetVertexSource(l1.NameLine, l1.NameShape);
            l1.CutPoint = GetCutPoint(l1.NameLine, l1.VertexSource);

            Shape? shape = GlobalVariable.shapes.FirstOrDefault(t => t.Name == l1.NameShape);
            l1.RibDest = GetRibDest(l1.VertexSource, shape!, sentence);
            l1.NameAngleSource = GetNameAngleSource(shape!, l1.VertexSource);
            GlobalVariable.lines.Add(l1);
            shape!.MoreLines.Add(l1);

            //הוספת 2 הזויות החדשות שנוצרו
            Angle a1 = new Angle();
            a1.NameAngle = l1.NameAngleSource[0].ToString() + l1.NameAngleSource[1].ToString() + l1.CutPoint;
            SetRibsOfAngle(a1);
            Angle a2 = new Angle();
            a2.NameAngle = l1.NameAngleSource[2].ToString() + l1.NameAngleSource[1].ToString() + l1.CutPoint;
            SetRibsOfAngle(a2);
            shape.MoreAngles.Add(a1);
            shape.MoreAngles.Add(a2);
            GlobalVariable.angles.Add(a1);
            GlobalVariable.angles.Add(a2);

            if (!Is_equal<Angle>(a1, a2))
            {
                Relation relation1 = new Relation() { obj1 = a1, obj2 = a2, relation = 1 };
                this.ListAllRelations.Add(relation1);
                Angle angleSource = shape.Angles.FirstOrDefault(x => x.NameAngle == l1.NameAngleSource)!;
                if (angleSource.ValueAngle != 0)
                {
                    a1.ValueAngle = angleSource.ValueAngle*0.5;
                    a2.ValueAngle = angleSource.ValueAngle*0.5;
                }

            }
            if (shape is Triangle)
            {
                Create_2_small_tris(l1, (Triangle)shape);
            }
            Create_2_lines(l1);
            #region הוספת המשפטים הרלוונטים למילון
            Sentence s1 = Create_sentence_if_middle_and_bisectsAngle_is_converge(shape);
            GlobalVariable.relevant_sentences.Add(s1);
            Sentence s2 = Create_sentence_Lines_in_triangle_is_converge(shape);
            GlobalVariable.relevant_sentences.Add(s2);
            Sentence s3 = Create_sentence_if_bisectsAngle_and_plumb_is_converge(shape);
            GlobalVariable.relevant_sentences.Add(s3);
            #endregion
            Console.WriteLine("bisectsAngle is built");

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
            r1.NameShape = GetNameShapeOfRibOrLine(r1.NameLine);
            GlobalVariable.lines.Add(r1);
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
            r1.NameShape = GetNameShapeOfRibOrLine(r1.NameLine);
            GlobalVariable.lines.Add(r1);
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
            r1.NameShape = GetNameShapeOfRibOrLine(r1.NameLine);
            GlobalVariable.lines.Add(r1);
        }

       


        public void Create_relation(string sentence, int index)
        {
            string FirstElement = GetFirstElement(sentence);
            string secondElement = GetSecondElement(sentence, index);
            //במקרה שזה צלע או ישר
            if (FirstElement.Length == 2)
            {
                Line? l1 = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == FirstElement)!;
                Line? l2 = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == secondElement)!;
                if (!Is_equal<Line>(l1!, l2!))
                {
                    Relation relation1 = new Relation() { obj1 = l1!, obj2 = l2!, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                    if (l1.LenLine == 0)
                        l1.LenLine = l2.LenLine;
                    if (l2.LenLine == 0)
                        l2.LenLine = l1.LenLine;
                }
            }

            //במקרה שזה זווית
            if (FirstElement.Length == 3)
            {
                Angle? a1 = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == FirstElement)!;
                Angle? a2 = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == secondElement)!;
                if (!Is_equal<Angle>(a1!, a2!))
                {
                    Relation relation1 = new Relation() { obj1 = a1!, obj2 = a2!, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                    if (a1.ValueAngle == 0)
                        a1.ValueAngle = a2.ValueAngle;
                    if (a2.ValueAngle == 0)
                        a2.ValueAngle = a1.ValueAngle;
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