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

    internal class textAnalysis :Element
    {
        public string textOfExercise;
        public string data;
        public string proofs;
        public Dictionary<string, create_element> key_words_dictionary = new Dictionary<string, create_element>();

        public textAnalysis()
        {

        }
        /// <summary>
        /// אולי נשתמש בזה 
        /// </summary>
        /// <param name="textOfExercise"></param>
        /// <returns></returns>
        public MatchCollection find_key_words(string textOfExercise)
        {
            //העלאת קובץ אקסל
            WorkBook wb = WorkBook.Load("C:\\Users\\win 10\\Desktop\\my-project\\MyProject\\Text Of Exercise\\key_words.xlsx");
            WorkSheet ws = wb.GetWorkSheet("sheet1");

            // בניית ביטוי רגולרי שיכלול את כל מילות המפתח
            string pattern = @"(" + string.Join("|", ws["A1:A12"]) + @")";

            // יצירת אובייקט Regex
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // חיפוש ההתאמות בטקסט
            MatchCollection matches = regex.Matches(textOfExercise);
            return matches;
        }

        /// <summary>
        /// מחלקת את הטקסט למשפטים
        /// </summary>
        /// <returns></returns>
        public string[] string_scan(string text)
        {
            string[] sentences = text.Split('.');
            return sentences;
        }

        /// <summary>
        /// מחלקת את הטקסט לנתונים והוכחות
        /// </summary>
        public void divides_the_text_into_facts_and_proofs()
        {
            int index = this.textOfExercise.IndexOf("הוכח");
            this.data =this.textOfExercise.Substring(0, index);
            this.proofs =this.textOfExercise.Substring(index); 
        }

        /// <summary>
    ///יוצר אוביקט לפי מילת מפתח
    /// </summary>
        create_element ce;
        public void Create_elements()
        { 
            divides_the_text_into_facts_and_proofs();
            string[] sentences =string_scan(this.data);
            for(int i = 0;i<sentences.Length;i++)
            {
                int index = 0;
                foreach (var word in this.key_words_dictionary)
                {
                    string s = word.Key;
                    ce += key_words_dictionary[s];
                    index = sentences[i].IndexOf(s,index);
                    if (index != -1)
                    {
                        this.key_words_dictionary[s].Invoke(sentences[i], index);
                    }
                    else
                        index = 0;
                }
            }
        }

        /// <summary>
        ///  מחזירה שם משולש או זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameOfTriOrAngle(string sentence)
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
            foreach (Triangle triangle in GlobalVariable.Triangles)
            {
                if (triangle.Name.Contains(nameLine[0])|| triangle.Name.Contains(nameLine[1]))
                    return triangle.Name;
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
        public char GetCutPoint(string nameLine,char vertexSource)
        {
            if (nameLine[0]==vertexSource)
                return nameLine[1];
            return nameLine[0];
        }
        
        /// <summary>
        /// מחזירה צלע יעד של ישר במשולש
        /// </summary>
        /// <param name="vertexSource"></param>
        /// <param name="nameTriangle"></param>
        /// <returns></returns>
        public Rib GetRibDest(char vertexSource, string nameTriangle)
        {
            string nameRib="";
            if (nameTriangle[0] == vertexSource)
                nameRib= nameTriangle[1].ToString() + nameTriangle[2].ToString();
            if (nameTriangle[1] == vertexSource)
                nameRib= nameTriangle[2].ToString()+ nameTriangle[0].ToString();
            if (nameTriangle[2] == vertexSource)
                nameRib = nameTriangle[1].ToString() + nameTriangle[0].ToString();
            Triangle t = GlobalVariable.Triangles.FirstOrDefault(x => x.Name == nameTriangle);
            if(t != null)
                return t.Ribs.FirstOrDefault(x => x.NameLine == nameRib);
            return null;
        }

        /// <summary>
        /// מחזירה את זווית המקור
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameAngleSource(string sentence)
        {
            Regex rg = new Regex(@"[A-Z]{2}");
            Match match = rg.Match(sentence);
            return match.Value;
        }

        /// <summary>
        /// פרטי זווית של המשולש
        /// </summary>
        /// <param name="t1"></param>
        public void SetAnglesOfTri(Triangle t1)
        {
            t1.Angles[0].NameAngle = (t1.Name[0] + t1.Name[1] + t1.Name[2]).ToString();
            t1.Angles[0].Rib1.NameLine = (t1.Name[0] + t1.Name[1]).ToString();
            t1.Angles[0].Rib2.NameLine = (t1.Name[1] + t1.Name[2]).ToString();
            t1.Angles[1].NameAngle = (t1.Name[1] + t1.Name[2] + t1.Name[0]).ToString();
            t1.Angles[1].Rib1.NameLine = (t1.Name[1] + t1.Name[2]).ToString();
            t1.Angles[1].Rib2.NameLine = (t1.Name[2] + t1.Name[0]).ToString();
            t1.Angles[2].NameAngle = (t1.Name[2] + t1.Name[0] + t1.Name[1]).ToString();
            t1.Angles[2].Rib1.NameLine = (t1.Name[2] + t1.Name[0]).ToString();
            t1.Angles[2].Rib2.NameLine = (t1.Name[0] + t1.Name[1]).ToString();
        }

        /// <summary>
        /// פרטי צלעות של המשולש
        /// </summary>
        /// <param name="t1"></param>
        public void SetRibsOfTri(Triangle t1)
        {
            foreach (Rib rib in t1.Ribs)
            {
                rib.NameTriangle = t1.Name;
                rib.DescriptionRib = DescriptionRib.simple;
            }
            t1.Ribs[0].NameLine = (t1.Name[0] + t1.Name[1]).ToString();
            t1.Ribs[1].NameLine = (t1.Name[1] + t1.Name[2]).ToString();
            t1.Ribs[2].NameLine = (t1.Name[2] + t1.Name[1]).ToString();
        }


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
        /// יוצרת אוביקט משולש
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_triangle(string sentence, int index)
        {
            string nameTriangle = GetNameOfTriOrAngle(sentence);
            var triangle = GlobalVariable.Triangles.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                Triangle t1 = new Triangle();
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfTri(t1);
                SetRibsOfTri(t1);
                GlobalVariable.Triangles.Add(t1);
            } 
        }

       /// <summary>
       /// יוצרת אוביקט משולש שו"ש
       /// </summary>
       /// <param name="sentence"></param>
       /// <param name="index"></param>
        public void Create_isosceles_triangle(string sentence, int index)
        {
            string nameTriangle = GetNameOfTriOrAngle(sentence);
            var triangle = GlobalVariable.Triangles.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                IsoscelesTriangle t1 = new IsoscelesTriangle();
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfTri(t1);
                
                GlobalVariable.Triangles.Add(t1);
            }
            
        }

        /// <summary>
        /// משולש שו"צ
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_equilateral_triangle(string sentence, int index)
        {
            string nameTriangle = GetNameOfTriOrAngle(sentence);
            var triangle = GlobalVariable.Triangles.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                EquilateralTriangle t1 = new EquilateralTriangle();
                t1.Name =nameTriangle;
                t1.sortNameShape(); 
                SetAnglesOfTri(t1);
                SetRibsOfTri(t1);
                GlobalVariable.Triangles.Add(t1);
            }
        }

        /// <summary>
        /// משולש ישר זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void create_right_triangle(string sentence, int index)
        {
            string nameTriangle = GetNameOfTriOrAngle(sentence);
            var triangle = GlobalVariable.Triangles.FirstOrDefault(x => x.Name == nameTriangle);
            if (triangle == null)
            {
                RightTriangle t1 = new RightTriangle();
                t1.Name = nameTriangle;
                t1.sortNameShape();
                SetAnglesOfTri(t1);
                SetRibsOfTri(t1);
                string subSentence = sentence.Substring(index);
                t1.NameRightAngle = GetNameOfTriOrAngle(subSentence);
                SetRibsOfRightTri(t1);
                GlobalVariable.Triangles.Add(t1);
            }
        }

        /// <summary>
        /// יוצרת זווית
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_angle(string sentence, int index)
        {
            string nameAngle= GetNameOfTriOrAngle(sentence);
            var sortedAngle = new string(nameAngle.Where(char.IsLetter).OrderBy(char.ToLower).ToArray());
            var triangle= GlobalVariable.Triangles.FirstOrDefault(x => x.Name == sortedAngle);
            if (triangle == null)
            {
                Angle a1 = new Angle();
                a1.NameAngle = nameAngle;
                SetRibsOfAngle(a1);
            }
        }

        /// <summary>
        /// יוצרת ישר
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_line(string sentence, int index)
        {            
            Line l1 = new Line();
            l1.NameLine = GetNameOfRibOrLine(sentence);
        }

        /// <summary>
        /// יוצרת תיכון
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="index"></param>
        public void Create_middle(string sentence, int index)
        {
            LineInTriangle l1 = new LineInTriangle();
            l1.NameLine = GetNameOfRibOrLine(sentence);
            l1.DescriptionLine = DescriptionLine.middle;
            l1.NameTriangle = GetNameTriangleOfRibOrLine(l1.NameLine);
            l1.VertexSource = GetVertexSource(l1.NameLine,l1.NameTriangle);
            l1.CutPoint = GetCutPoint(l1.NameLine,l1.VertexSource);
            l1.RibDest = GetRibDest(l1.VertexSource, l1.NameTriangle);

            Triangle tri=GlobalVariable.Triangles.First(t => t.Name == l1.NameTriangle);
            tri.MoreLines.Add(l1);
        }

        /// <summary>
        /// יוצרת גובה
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

            Triangle tri = GlobalVariable.Triangles.First(t => t.Name == l1.NameTriangle);
            tri.MoreLines.Add(l1);
        }

        /// <summary>
        /// יוצרת חוצה זווית
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
            l1.NameAngleSource = GetNameAngleSource(sentence);

            Triangle tri = GlobalVariable.Triangles.First(t => t.Name == l1.NameTriangle);
            tri.MoreLines.Add(l1);
        }

        /// <summary>
        /// יוצר צלע
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
        /// יוצר ניצב
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
        /// יוצר יתר
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
            key_words_dictionary.Add("משולש", Create_triangle);
            key_words_dictionary.Add("משולש שוווה שוקיים", Create_isosceles_triangle);
            key_words_dictionary.Add("משולש שוווה צלעות", Create_equilateral_triangle);
            key_words_dictionary.Add("משולש ישר זווית", create_right_triangle);
            key_words_dictionary.Add("זווית", Create_angle);
            key_words_dictionary.Add("<", Create_angle);
            key_words_dictionary.Add("ישר", Create_line);
            key_words_dictionary.Add("קטע", Create_line);
            key_words_dictionary.Add("אמצע צלע", Create_line);
            key_words_dictionary.Add("תיכון", Create_middle);
            key_words_dictionary.Add("גובה", Create_plumb);
            key_words_dictionary.Add("חוצה זווית", Create_bisects_angle);
            key_words_dictionary.Add("חוצה את הזווית", Create_bisects_angle);
            key_words_dictionary.Add("צלע", Create_rib);
            key_words_dictionary.Add("ניצב", Create_nichav);
            key_words_dictionary.Add("יתר", Create_yeter);
            key_words_dictionary.Add("=", Create_relation);
            key_words_dictionary.Add("שווה", Create_relation);
        }

        public void Create_relation(string sentence, int index)
        {
            string firstElement = GetFirstElement(sentence);
            string secondElement = GetSecondElement(sentence, index);
            //במקרה שזה צלע או ישר
            if (firstElement.Length == 2)
            {
                Line l1=GlobalVariable.lines.First(x=>x.NameLine == firstElement);
                Line l2=GlobalVariable.lines.First(x=>x.NameLine == secondElement);
                var thisRelation = (l1, 1);
                if (!l2.GetMyRelations().Contains(thisRelation))
                {
                    Relation relation1 = new Relation() { obj1 = l1, obj2 = l2, relation = 1 };
                    this.ListAllRelations.Add(relation1);
                }
            }

            //במקרה שזה זווית
            if(firstElement.Length == 3)
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