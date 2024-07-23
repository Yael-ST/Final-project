using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IronXL;
using System.Collections;
using MyProject.Classes;

namespace MyProject.Text_Of_Exercise
{
    delegate void create_element(string sentence, int index);

    internal class Text_of_Data:TextAnalysis
    {
        public Dictionary<string, create_element> key_words_of_data_dictionary;
        public Text_of_Data()
        {
            key_words_of_data_dictionary = new Dictionary<string, create_element>();

        }      
        public void SetTextOfExercise(string text)
        {
            textOfExercise = text;
        }
        /// <summary>
        /// בונה את המילון של מילות המפתח
        /// </summary>
        public void Build_dictionary_key_words_of_data()
        {
            WorkSheet key_words = Uploading_an_Excel_file();
            key_words_of_data_dictionary.Add(key_words["A1"].ToString(), create_right_triangle);
            key_words_of_data_dictionary.Add(key_words["A2"].ToString(), Create_isosceles_triangle);
            key_words_of_data_dictionary.Add(key_words["A3"].ToString(), Create_equilateral_triangle);
            key_words_of_data_dictionary.Add(key_words["A4"].ToString(), Create_triangle);
            key_words_of_data_dictionary.Add(key_words["A5"].ToString(), Create_angle);
            key_words_of_data_dictionary.Add(key_words["A6"].ToString(), Create_angle);
            key_words_of_data_dictionary.Add(key_words["A7"].ToString(), Create_line);
            key_words_of_data_dictionary.Add(key_words["A8"].ToString(), Create_line);
            key_words_of_data_dictionary.Add(key_words["A9"].ToString(), Create_middle);
            key_words_of_data_dictionary.Add(key_words["A10"].ToString(), Create_middle);
            key_words_of_data_dictionary.Add(key_words["A11"].ToString(), Create_plumb);
            key_words_of_data_dictionary.Add(key_words["A12"].ToString(), Create_bisects_angle);
            key_words_of_data_dictionary.Add(key_words["A13"].ToString(), Create_bisects_angle);
            key_words_of_data_dictionary.Add(key_words["A14"].ToString(), Create_rib);
            key_words_of_data_dictionary.Add(key_words["A15"].ToString(), Create_nichav);
            key_words_of_data_dictionary.Add(key_words["A16"].ToString(), Create_yeter);
            key_words_of_data_dictionary.Add(key_words["A17"].ToString(), Create_relation);
            key_words_of_data_dictionary.Add(key_words["A18"].ToString(), Create_relation);
            Console.WriteLine("the key_words_data dictionary is built");
        }

        /// <summary>
        ///יצירת אוביקט לפי מילת מפתח
        /// </summary>
        public void Scan_the_data()
        {
            Divides_the_text_into_facts_and_proofs();
            Build_dictionary_key_words_of_data();
            string[] sentences = Dividing_the_text_into_sentences(data);

            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i] != " ")
                {
                    Console.WriteLine(sentences[i]);
                    //מעבר על כל מילות המפתח
                    foreach (var word in this.key_words_of_data_dictionary)
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
        }

        /// <summary>
        ///  מחזירה שם צורה
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameOfShape(string sentence, Shape shape)
        {
            int length = shape is Triangle ? 3 : 4;
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
            return ((char)nameLine[1]);
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
                    nameRib = string.Concat(shape.Name[1].ToString(), shape.Name[2]);
                if (shape.Name[1] == vertexSource)
                    nameRib = string.Concat(shape.Name[0].ToString(), shape.Name[2]);
                if (shape.Name[2] == vertexSource)
                    nameRib = string.Concat(shape.Name[0].ToString(), shape.Name[1]);
                nameRib = new string(nameRib.OrderBy(char.ToLower).ToArray());
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
        public void SetAnglesOfShape(Shape shape, bool is_to_small_triangle)
        {
            int length = shape.Name.Length;
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < 3; i++)
                    shape.Angles[j].NameAngle += shape.Name[(i + j) % length];
                shape.Angles[j].sortNameAngle();
                if (is_to_small_triangle)
                {
                    Angle aa = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == shape.Angles[j].NameAngle)!;
                    if (aa != null)
                    {
                        shape.Angles[j].ValueAngle = aa.ValueAngle;
                        if (!Is_equal<Angle>(aa, shape.Angles[j]))
                        {
                            Relation relation1 = new Relation() { obj1 = aa, obj2 = shape.Ribs[j], relation = 1 };
                            GlobalVariable.ListAllRelations.Add(relation1);
                        }
                    }
                }
                GlobalVariable.angles.Add(shape.Angles[j]);
            }
        }

        /// <summary>
        /// פרטי צלעות של הצורה
        /// </summary>
        /// <param name="shape"></param>
        public void SetRibsOfShape(Shape shape, bool is_to_small_triangle)
        {
            int length = shape.Name.Length;
            for (int j = 0; j < length; j++)
            {
                for (int i = 1; i <= 2; i++)
                    shape.Ribs[j].NameLine += shape.Name[(i + j) % length];
                shape.Ribs[j].sortNameLine();
                shape.Ribs[j].NameShape = shape.Name;
                shape.Ribs[j].DescriptionRib = DescriptionRib.simple;
                if (is_to_small_triangle)
                {
                    Line rr = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == shape.Ribs[j].NameLine)!;
                    if (rr != null)
                    {
                        shape.Ribs[j].LenLine = rr.LenLine;
                        if (!Is_equal<Line>(rr, shape.Ribs[j]))
                        {
                            Relation relation1 = new Relation() { obj1 = rr, obj2 = shape.Ribs[j], relation = 1 };
                            GlobalVariable.ListAllRelations.Add(relation1);
                        }
                        if (rr is not LineInShape && rr is not Rib)
                        {
                            GlobalVariable.lines.Remove(rr);
                            GlobalVariable.lines.Add(shape.Ribs[j]);
                        }
                        else
                            GlobalVariable.lines.Add(shape.Ribs[j]);
                    }
                }
                else
                    GlobalVariable.lines.Add(shape.Ribs[j]);
            }
        }

        /// <summary>
        /// פרטי שוקיים של משו"ש
        /// </summary>
        /// <param name="t"></param>
        /// <param name="sentence"></param>
        public void SetShoksOfIsoscelesTriangle(IsoscelesTriangle t, string sentence)
        {
            Match matchShok1 = GetFirstElement(sentence);
            string nameShok2 = GetSecondElement(sentence.Substring(matchShok1.Index + 2));
            string nameShok1 = matchShok1.Value;
            Rib shok1 = t.Ribs.FirstOrDefault(x => x.NameLine == nameShok1)!;
            Rib shok2 = t.Ribs.FirstOrDefault(x => x.NameLine == nameShok2)!;
            shok1.DescriptionRib = DescriptionRib.shok1;
            shok2.DescriptionRib = DescriptionRib.shok2;
            shok1.Add_relation<Line>(shok1, shok2, 1);
            List<(IRelated, double)> myRelations = shok1.GetMyRelations();
            foreach ((IRelated, double) item in myRelations)
            {
                Console.WriteLine("we have a relation " + shok1.NameLine + " & " + ((Line)item.Item1).NameLine);

            }
        }

        /// <summary>
        /// פרטי  צלעות של משולש ישר זווית
        /// </summary>
        /// <param name="t1"></param>
        public void SetRibsOfRightTri(RightTriangle t1, bool is_to_small_triangle)
        {
            string nameNichav1 = string.Concat(t1.RightAngle.NameAngle[0], t1.RightAngle.NameAngle[1]);
            string nameNichav2 = string.Concat(t1.RightAngle.NameAngle[1], t1.RightAngle.NameAngle[2]);
            string nameYeter = string.Concat(t1.RightAngle.NameAngle[2], t1.RightAngle.NameAngle[0]);
            t1.Ribs[0].NameLine = nameNichav1;
            t1.Ribs[1].NameLine = nameNichav2;
            t1.Ribs[2].NameLine = nameYeter;

            t1.Ribs[0].DescriptionRib = DescriptionRib.nichav;
            t1.Ribs[1].DescriptionRib = DescriptionRib.nichav;
            t1.Ribs[2].DescriptionRib = DescriptionRib.yeter;
            foreach (Rib rib in t1.Ribs)
            {
                rib.NameShape = t1.Name;
                rib.sortNameLine();
            }
            //אם הצלעות הם עבור משולש ישר זווית קטן

            foreach (Rib rib in t1.Ribs)
            {
                if (is_to_small_triangle)
                {
                    Line rr = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == rib.NameLine)!;
                    if (rr != null)
                    {
                        rib.LenLine = rr.LenLine;
                        Add_relation<Line>(rib, rr, 1);
                        if (rr is not LineInShape && rr is not Rib)
                        {
                            GlobalVariable.lines.Remove(rr);
                        }
                        GlobalVariable.lines.Add(rib);
                    }
                }
                else
                    GlobalVariable.lines.Add(rib);
            }


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
                SetAnglesOfShape(t1, false);
                SetRibsOfShape(t1, false);
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
            string nameTriangle = GetNameOfShape(sentence, t1);
            var triangle = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfShape(t1, false);
                SetRibsOfShape(t1, false);
                SetShoksOfIsoscelesTriangle(t1, sentence.Substring(index + 22));
                t1.Set_attributes_of_the_isosceles_triangle();
                GlobalVariable.shapes.Add(t1);
                Console.WriteLine("the IsoscelesTriangle is built");

                #region הוספת משפטים רלוונטים לעץ
                Sentence s1 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(t1!);
                Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                if (ss == null)
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
                SetAnglesOfShape(t1, false);
                SetRibsOfShape(t1, false);
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
                SetAnglesOfShape(t1, false);
                string subSentence = sentence.Substring(index);
                t1.RightAngle.NameAngle = GetNameOfAngle(subSentence);
                SetRibsOfRightTri(t1, false);
                t1.Set_attributes_of_the_Right_triangle();
                GlobalVariable.shapes.Add(t1);
                Console.WriteLine("RightTriangle");

                #region הוספת משפטים רלונטיים למילון
                //1
                Sentence s1 = Create_sentence_Pythagorean_Theorem(triangle!);
                Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s1); 
                //2
                Sentence s2 = Create_sentence_The_middle_for_yeter(triangle!);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s2);
                //3
                Sentence s3 = Create_sentence_A_nice_triangle(triangle!);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s3);
                #endregion
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
                SetRibsOfRightTri(smallRightTriangle1, true);
                SetAnglesOfShape(smallRightTriangle1, true);
                smallRightTriangle1.RightAngle.NameAngle = r1.NameLine[0] + l.CutPoint + r1.NameLine[1].ToString();
                smallRightTriangle1.RightAngle.sortNameAngle();
                smallRightTriangle1.RightAngle.ValueAngle = 90;
                GlobalVariable.shapes.Add(smallRightTriangle1);

                //2
                RightTriangle smallRightTriangle2 = new RightTriangle();
                smallRightTriangle2.Name = nameSmallTriangle2;
                smallRightTriangle2.sortNameShape();
                SetRibsOfRightTri(smallRightTriangle2, true);
                SetAnglesOfShape(smallRightTriangle2, true);
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
                    SetAnglesOfShape(smallTriangle1, true);
                    SetRibsOfShape(smallTriangle1, true);
                    GlobalVariable.shapes.Add(smallTriangle1);
                }

                if (triangle2 == null)
                {
                    Triangle smallTriangle2 = new Triangle();
                    smallTriangle2.Name = nameSmallTriangle2;
                    smallTriangle2.sortNameShape();
                    SetAnglesOfShape(smallTriangle2, true);
                    SetRibsOfShape(smallTriangle2, true);
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
            Triangle tri = new Triangle();
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
                l.NameAngleSource = GetNameAngleSource(tri, l.VertexSource);

                tri!.MoreLines.Add(l);
                GlobalVariable.lines.Add(l);
                Create_2_lines(l);
                Create_2_small_tris(l, tri);
            }
            else
            {
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
            }

            #region הוספת משפטים רלוונטים לעץ
            Sentence s1 = Create_sentence_if_middle_and_bisectsAngle_is_converge(tri);
            Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s1);
            Sentence s2 = Create_sentence_if_middle_and_plumb_is_converge(tri);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s2);
            Sentence s3 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(tri);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s3);
            Sentence s4 = Create_sentence_The_middle_for_yeter(tri);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s4.Id && x.GeometricElement == s4.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s4);
            #endregion

            Console.WriteLine("middle is built");
        }

        /// <summary>
        /// יצירת 2 קטעים שנוצרו מהישר שעבר על הבסיס
        /// </summary>
        /// <param name="l"></param>
        public void Create_2_lines(LineInShape l)
        {
            char vertex1 = l.NameShape.FirstOrDefault(x => x != l.VertexSource)!;
            char vertex2 = l.NameShape.LastOrDefault(x => x != l.VertexSource)!;
            Line l1 = new Line();
            l1.NameLine = string.Concat(l.CutPoint, vertex1);
            l1.sortNameLine();
            l1.NameShape = l.NameShape;
            GlobalVariable.lines.Add(l1);

            Line l2 = new Line();
            l2.NameLine = string.Concat(l.CutPoint, vertex2);
            l2.sortNameLine();
            l2.NameShape = l.NameShape;
            GlobalVariable.lines.Add(l2);
            //אם הישר הוא תיכון אז הקטעים החדשים שנוצרו שווים
            if (l.DescriptionLine == DescriptionLine.middle)
            {
                if (!Is_equal<Line>(l1, l2))
                {
                    Relation relation1 = new Relation() { obj1 = l1, obj2 = l2, relation = 1 };
                    GlobalVariable.ListAllRelations.Add(relation1);
                    //l1.LenLine = l.LenLine * 0.5;
                    //l2.LenLine = l.LenLine * 0.5;
                    List<(IRelated, double)> myRelations = l2.GetMyRelations();
                    foreach ((IRelated, double) item in myRelations)
                    {
                        Console.WriteLine("we have a relation " + l2.NameLine + " & " + ((Line)item.Item1).NameLine);

                    }
                }
                l1.Add_relation<Line>(l1, l.RibDest, 0.5);
                l2.Add_relation<Line>(l2, l.RibDest, 0.5);
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

            #region הוספת משפטים רלוונטים לעץ
            Sentence s1 = Create_sentence_if_middle_and_plumb_is_converge(shape);
            Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s1);
            Sentence s2 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(shape);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s2);
            Sentence s3 = Create_sentence_if_bisectsAngle_and_plumb_is_converge(shape);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s3);
            Console.WriteLine("plumb is built");
            #endregion
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
                GlobalVariable.ListAllRelations.Add(relation1);
                Angle angleSource = shape.Angles.FirstOrDefault(x => x.NameAngle == l1.NameAngleSource)!;
                if (angleSource.ValueAngle != 0)
                {
                    a1.ValueAngle = angleSource.ValueAngle * 0.5;
                    a2.ValueAngle = angleSource.ValueAngle * 0.5;
                }
            }
            if (shape is Triangle)
            {
                Create_2_small_tris(l1, (Triangle)shape);
            }
            Create_2_lines(l1);
            Console.WriteLine("bisectsAngle is built");

            #region הוספת המשפטים הרלוונטים למילון
            Sentence s1 = Create_sentence_if_middle_and_bisectsAngle_is_converge(shape);
            Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s1);
            Sentence s2 = Create_sentence_Lines_in_IsoscelesTriangle_is_converge(shape);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s2);
            Sentence s3 = Create_sentence_if_bisectsAngle_and_plumb_is_converge(shape);
            ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
            if (ss == null)
                GlobalVariable.relevant_sentences.Add(s3);
            
            #endregion

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
            if (!Create_relation_with_one_element(sentence))
            {
                Create_relation_with_two_element(sentence, index);
            }
        }

        public bool Create_relation_with_one_element(string sentence)
        {
            int num = GetNumber(sentence);
            if (num == 0) return false;
            Match element = GetFirstElement(sentence);
            if (element.Value.Length == 2)
            {
                Line line = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == element.Value)!;
                line.LenLine = num;
                line.CompleteAllMyRelations();

                #region הוספת משפטים רלונטים לעץ
                string nameShape = line.NameShape;
                Triangle t1 = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(x => x.Name == nameShape)!;
                //1
                Sentence s1 = Create_sentence_find_Perimeter(t1);
                Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s1);
                //2
                Sentence s2 = Create_sentence_A_nice_triangle__reverse_sentence(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s2);
                #endregion
            }
            if (element.Value.Length == 3)
            {
                Angle angle = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == element.Value)!;
                angle.ValueAngle = num;
                angle.CompleteAllMyRelations();
                #region הוספת משפטים רלוונטים לעץ
                string nameShape = new string(angle.NameAngle.OrderBy(char.ToLower).ToArray());
                Triangle t1 = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(x => x.Name == nameShape)!;
                //1
                Sentence s1 = Create_sentence_If_triangle_is_IsoscelesTriangle_and_oneOfThEAngles_equal_to_60(t1);
                Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s1);
                //2
                Sentence s2 = Create_sentence_Completing_Angles(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s2);
                //3
                Sentence s3 = Create_sentence_If_2_of_angles_equal_to60(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s3);
                //4
                Sentence s4 = Create_sentence_If_2_of_angles_equal_to60(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s4.Id && x.GeometricElement == s4.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s4);

                #endregion
            }
            return true;
        }

        public void Create_relation_with_two_element(string sentence, int index)
        {
            #region ss
            int indexSograim = sentence.IndexOf("(");
            sentence = indexSograim != -1 ? sentence.Substring(indexSograim) : sentence;
            #endregion
            Match FirstElement = GetFirstElement(sentence);
            string secondElement = GetSecondElement(sentence.Substring(FirstElement.Index + 2));
            //במקרה שזה צלע או ישר
            if (FirstElement.Length == 2)
            {
                Line l1 = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == FirstElement.Value)!;
                Line l2 = GlobalVariable.lines.FirstOrDefault(x => x.NameLine == secondElement)!;
                if (!Is_equal<Line>(l1, l2))
                {
                    Relation relation1 = new Relation() { obj1 = l1, obj2 = l2, relation = 1 };
                    GlobalVariable.ListAllRelations.Add(relation1);
                    l1.LenLine = l1.LenLine == 0 ? l2.LenLine : l1.LenLine;
                    l2.LenLine = l2.LenLine == 0 ? l1.LenLine : l2.LenLine;
                }

                #region הוספת משפטים רלוונטים לעץ
                //משפטים עבור הישר הראשון
                Triangle t1 = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(x => x.Name == l1.NameShape)!;
                //1
                Sentence s1 = Create_sentence_find_Perimeter(t1);
                Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s1);
                //2
                Sentence s2 = Create_sentence_Shoks_is_equal(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s2);
                //3
                Sentence s3 = Create_sentence_Pythagorean_Theorem(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s3);

                //משפטים עבור הישר השני
                Triangle t2 = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(x => x.Name == l2.NameShape)!;
                //1
                Sentence s11 = Create_sentence_find_Perimeter(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s11.Id && x.GeometricElement == s11.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s11);
                //2
                Sentence s22 = Create_sentence_Shoks_is_equal(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s22.Id && x.GeometricElement == s22.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s22);
                //3
                Sentence s33 = Create_sentence_Pythagorean_Theorem(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s33.Id && x.GeometricElement == s33.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s33);
                #endregion
            }

            //במקרה שזה זווית
            if (FirstElement.Length == 3)
            {
                Angle? a1 = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == FirstElement.Value)!;
                Angle? a2 = GlobalVariable.angles.FirstOrDefault(x => x.NameAngle == secondElement)!;
                if (!Is_equal<Angle>(a1!, a2!))
                {
                    Relation relation1 = new Relation() { obj1 = a1!, obj2 = a2!, relation = 1 };
                    GlobalVariable.ListAllRelations.Add(relation1);
                    a1.ValueAngle = a1.ValueAngle == 0 ? a2.ValueAngle : a1.ValueAngle;
                    a2.ValueAngle = a2.ValueAngle == 0 ? a1.ValueAngle : a2.ValueAngle;
                }

                #region הוספת משפטים רלוונטים לעץ
                //משפטים עבור הזווית הראשונה
                string nameShape = new string(a1.NameAngle.OrderBy(char.ToLower).ToArray());
                Triangle t1 = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(x => x.Name == nameShape)!;
                //1
                Sentence s1 = Create_sentence_Completing_Angles(t1);
                Sentence ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s1.Id && x.GeometricElement == s1.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s1);
                //2
                Sentence s2 = Create_sentence_Basic_angles_is_equal(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s2.Id && x.GeometricElement == s2.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s2);
                //3
                Sentence s3 = Create_sentence_If_2_of_angles_equal_to60(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s3.Id && x.GeometricElement == s3.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s3);
                //4
                Sentence s4 = Create_sentence_If_triangle_is_IsoscelesTriangle_and_oneOfThEAngles_equal_to_60(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s4.Id && x.GeometricElement == s4.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s4);

                //משפטים עבור הזווית השנייה
                string nameShape2 = new string(a2.NameAngle.OrderBy(char.ToLower).ToArray());
                Triangle t2 = GlobalVariable.shapes.OfType<Triangle>().FirstOrDefault(x => x.Name == nameShape2)!;
                //1
                Sentence s11 = Create_sentence_Completing_Angles(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s11.Id && x.GeometricElement == s11.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s11);
                //2
                Sentence s22 = Create_sentence_Basic_angles_is_equal(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s22.Id && x.GeometricElement == s22.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s22);
                //3
                Sentence s33 = Create_sentence_If_2_of_angles_equal_to60(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s33.Id && x.GeometricElement == s33.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s33);
                //4
                Sentence s44 = Create_sentence_If_triangle_is_IsoscelesTriangle_and_oneOfThEAngles_equal_to_60(t1);
                ss = GlobalVariable.relevant_sentences.FirstOrDefault(x => x.Id == s44.Id && x.GeometricElement == s44.GeometricElement)!;
                if (ss == null)
                    GlobalVariable.relevant_sentences.Add(s44);
                #endregion
            }
        }

        public string GetSecondElement(string sentence)
        {
            Regex rg = new Regex(@"[A-Z]{2,3}");
            Match match = rg.Match(sentence);
            return match.Value;
        }

        public Match GetFirstElement(string sentence)
        {
            Regex rg = new Regex(@"[A-Z]{2,3}");
            Match match = rg.Match(sentence);
            return match;
        }

        public int GetNumber(string sentence)
        {
            string pattern = @"\d+";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(sentence);
            return match.Success ? Convert.ToInt32(match.Value) : 0;
        }
    }

}
    

